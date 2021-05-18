using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Training.AppService.Statistics.Dto;
using Training.Authorization;
using Training.Entity.BookLibraries;
using Training.Entity.Books;
using Training.Entity.BorrowBookDetails;
using Training.Entity.BorrowBooks;
using Training.Entity.Categories;
using Training.Entity.Districts;
using Training.Entity.Libraries;
using Training.Entity.PageResults;
using Training.Entity.Provinces;

namespace Training.AppService.Statistics
{
    [AbpAuthorize(PermissionNames.Pages_Librarians)]
    public class StatisticAppService : TrainingAppServiceBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IRepository<BorrowBookDetail, Guid> _borrowBookDetailRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<BookLibrary, Guid> _bookLibraryRepository;
        private readonly IRepository<Library, Guid> _libraryRepository;
        private readonly IRepository<District, Guid> _districtRepository;
        private readonly IRepository<Province, Guid> _provinceRepository;
        private readonly IRepository<BorrowBook, Guid> _borrowBookRepository;


        public StatisticAppService(IRepository<Book, Guid> bookRepository,
                                     IRepository<BorrowBookDetail, Guid> borrowBookDetailRepository,
                                     IRepository<Category, Guid> categoryRepository,
                                     IRepository<BookLibrary, Guid> bookLibraryRepository,
                                     IRepository<Library, Guid> libraryRepository,
                                     IRepository<District, Guid> districtRepository,
                                     IRepository<Province, Guid> provinceRepository,
                                     IRepository<BorrowBook, Guid> borrowBookRepository)
        {
            _bookRepository = bookRepository;
            _borrowBookDetailRepository = borrowBookDetailRepository;
            _categoryRepository = categoryRepository;
            _bookLibraryRepository = bookLibraryRepository;
            _libraryRepository = libraryRepository;
            _districtRepository = districtRepository;
            _provinceRepository = provinceRepository;
            _borrowBookRepository = borrowBookRepository;
        }

        [HttpGet]
        public async Task<PageResult<GetStatisticDto>> GetStatisticByCriteria(StatisticReportFilterDto input)
        {
            var counts = 0;

            var data = (from borrowBook in _borrowBookDetailRepository.GetAll()
                        join book in _bookRepository.GetAll() on borrowBook.BookId equals book.Id
                        select new
                        {
                            LibraryId = borrowBook.LibraryId,
                            BookId = borrowBook.BookId,
                            BookName = book.Name,
                            CategoryName = book.Category.Name,
                            AuthorName = book.Author.Name,
                            DateBorrow = borrowBook.BorrowBook.DateBorrow,
                            Qty = borrowBook.Qty
                        })
                       .ToList()
                       .Where(x => x.LibraryId == input.LibraryId)
                       .WhereIf(input.Month != null, x => x.DateBorrow.Month == input.Month)
                       .GroupBy(x => new { x.BookId, x.BookName, x.CategoryName, x.AuthorName })
                       .Select(x => new GetStatisticDto
                       {
                           BookId = x.Key.BookId,
                           BookName = x.Key.BookName,
                           CategoryName = x.Key.CategoryName,
                           AuthorName = x.Key.AuthorName,
                           Quantity = x.Sum(x => x.Qty)
                       }).OrderByDescending(x => x.Quantity);

            counts = data.Count();


            var result = new PageResult<GetStatisticDto>
            {
                Count = counts,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await Task.FromResult(data.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList())
            };

            return result;
        }

        [HttpGet]
        public async Task<PageResult<GetAllStatisticByDto>> GetAllStatisticBy(StatisticReportFilterByDto input)
        {
            var counts = 0;

            var results = (from category in _categoryRepository.GetAll()

                           join book in _bookRepository.GetAll() on category.Id equals book.CategoryId

                           join brDetail in _borrowBookDetailRepository.GetAll() on book.Id equals brDetail.BookId into TempBrDetail
                           from brDetailTB in TempBrDetail.DefaultIfEmpty()

                           join borrow in _borrowBookRepository.GetAll() on brDetailTB.BorrowBookId equals borrow.Id into TempBr
                           from borrowTB in TempBr.DefaultIfEmpty()

                           join library in _libraryRepository.GetAll() on brDetailTB.LibraryId equals library.Id into TempLib
                           from libraryTB in TempLib.DefaultIfEmpty()

                           join district in _districtRepository.GetAll() on libraryTB.DistrictId equals district.Id into TempDistrict
                           from districtTB in TempDistrict.DefaultIfEmpty()

                           join province in _provinceRepository.GetAll() on districtTB.ProvinceId equals province.Id into TempProvince
                           from provinceTB in TempProvince.DefaultIfEmpty()

                           select new
                           {
                               CategoryId = category.Id,
                               CategoryName = category.Name,
                               LibraryId = libraryTB != null ? libraryTB.Id : Guid.Empty,
                               LibraryName = libraryTB != null ? libraryTB.Name : null,
                               DistrictId = districtTB != null ? districtTB.Id : Guid.Empty,
                               DistrictName = districtTB != null ? districtTB.Name : null,
                               ProvinceId = provinceTB != null ? provinceTB.Id : Guid.Empty,
                               ProvinceName = provinceTB != null ? provinceTB.Name : null,
                               Qty = brDetailTB != null ? brDetailTB.Qty : 0,
                               DateBorrow = borrowTB.DateBorrow
                           })
                           .ToList()
                           .WhereIf(
                                input.LibraryId != Guid.Empty || input.ProvinceId != Guid.Empty ||
                                input.DistrictId != Guid.Empty || input.FromDate.HasValue && input.ToDate.HasValue ||
                                input.Month != null || input.Quarter != null
                                ,
                                x => x.LibraryId == input.LibraryId || x.ProvinceId == input.ProvinceId ||
                                x.DistrictId == input.DistrictId || x.DateBorrow.Date >= input.FromDate && x.DateBorrow.Date <= input.ToDate ||
                                x.DateBorrow.Month == input.Month ||
                                ((1 <= x.DateBorrow.Month && x.DateBorrow.Month <= 3) ? 1 : ((4 <= x.DateBorrow.Month && x.DateBorrow.Month <= 6) ? 2 : ((7 <= x.DateBorrow.Month && x.DateBorrow.Month <= 9) ? 3 : ((10 <= x.DateBorrow.Month && x.DateBorrow.Month <= 12) ? 4 : 0)))) == input.Quarter
                            )
                           .GroupBy(x => x.CategoryId)
                           .Select(data => new GetAllStatisticByDto
                           {
                               CategoryId = data.Key,
                               CategoryName = data.Select(x => x.CategoryName).FirstOrDefault(),
                               LibraryId = data.Select(x => x.LibraryId).FirstOrDefault(),
                               LibraryName = data.Select(x => x.LibraryName).FirstOrDefault(),
                               DistrictId = data.Select(x => x.DistrictId).FirstOrDefault(),
                               DistrictName = data.Select(x => x.DistrictName).FirstOrDefault(),
                               ProvinceId = data.Select(x => x.ProvinceId).FirstOrDefault(),
                               ProvinceName = data.Select(x => x.ProvinceName).FirstOrDefault(),
                               Quantity = data.Sum(x => x == null ? 0 : x.Qty),
                           }).OrderByDescending(x => x.Quantity);

            counts = results.Count();
            var result = new PageResult<GetAllStatisticByDto>
            {
                Count = counts,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await Task.FromResult(results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList())
            };

            return result;
        }
    }
}
