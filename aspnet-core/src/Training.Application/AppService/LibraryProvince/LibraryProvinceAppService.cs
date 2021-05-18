using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Training.AppService.LibraryProvince.Dto;
using Training.Authorization;
using Training.Entity.BookLibraries;
using Training.Entity.Books;
using Training.Entity.BorrowBookDetails;
using Training.Entity.BorrowBooks;
using Training.Entity.Districts;
using Training.Entity.Libraries;
using Training.Entity.PageResults;
using Training.Entity.Provinces;

namespace Training.AppService.LibraryProvince
{
    [AbpAuthorize(PermissionNames.Pages_Librarians)]
    public class LibraryProvinceAppService : TrainingAppServiceBase
    {
        private readonly IRepository<BorrowBook, Guid> _borrowBookRepository;
        private readonly IRepository<District, Guid> _districtRepository;
        private readonly IRepository<Library, Guid> _libraryRepository;
        private readonly IRepository<BorrowBookDetail, Guid> _borrowBookDetailRepository;

        public LibraryProvinceAppService(IRepository<BorrowBook, Guid> borrowBookRepository,
                                     IRepository<Library, Guid> libraryRepository,
                                     IRepository<BorrowBookDetail, Guid> borrowBookDetailRepository,
                                     IRepository<District, Guid> districtRepository)
        {
            _borrowBookRepository = borrowBookRepository;
            _libraryRepository = libraryRepository;
            _borrowBookDetailRepository = borrowBookDetailRepository;
            _districtRepository = districtRepository;
        }

        [HttpGet]
        public async Task<PageResult<GetLibraryProvinceDto>> GetLibraryProvinceBy(LibraryProvinceFilterDto input)
        {
            var count = 0;

            var results =
                (from district in _districtRepository.GetAll()
                 join library in _libraryRepository.GetAll() on district.Id equals library.DistrictId into tempL
                 from library in tempL.DefaultIfEmpty()

                 join borrowDetail in _borrowBookDetailRepository.GetAll() on library.Id equals borrowDetail.LibraryId into tempBrD
                 from borrowDetail in tempBrD.DefaultIfEmpty()

                 join borrow in _borrowBookRepository.GetAll() on borrowDetail.BorrowBookId equals borrow.Id into tempBr
                 from borrow in tempBr.DefaultIfEmpty()

                 select new
                 {
                     DistrictName = district.Name,
                     DistrictId = district.Id,
                     ProvinceName = district.Province.Name,
                     ProvinceId = district.ProvinceId,
                     LibraryId = library != null ? library.Id : Guid.Empty,
                     UserId = borrow != null ? borrow.UserId : 0,
                     TotalBorrow = borrowDetail != null ? borrowDetail.Qty : 0
                 })
                 .ToList()
                 .WhereIf(input.ProvinceId != Guid.Empty, x => x.ProvinceId == input.ProvinceId)
                 .GroupBy(x => new { x.DistrictId, x.ProvinceId, x.ProvinceName, x.DistrictName })
                 .Select(x => new GetLibraryProvinceDto
                 {
                     ProvinceId = x.Key.ProvinceId,
                     ProvinceName = x.Key.ProvinceName,
                     DistrictId = x.Key.DistrictId,
                     DistrictName = x.Key.DistrictName,
                     QuantityLibrary = x.Select(a => a.LibraryId).Distinct().Where(a => a != Guid.Empty).Count(),
                     QuantityUser = x.Select(a => a.UserId).Distinct().Where(a => a != 0).Count(),
                     TotalBorrow = x.Sum(y => y.TotalBorrow),
                 });

            count = results.Count();

            var result = new PageResult<GetLibraryProvinceDto>
            {
                Count = count,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await Task.FromResult(results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList())
            };

            return result;
        }

    }
}
