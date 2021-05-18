using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Training.AppService.BorrowBookDetails.Dto;
using Training.Entity.BookLibraries;
using Training.Entity.BorrowBookDetails;
using Training.Entity.BorrowBooks;
using Training.Validator.BorrowBookDetails;

namespace Training.AppService.BorrowBookDetails
{
    [AbpAuthorize]
    public class BorrowBookDetaiAppService : TrainingAppServiceBase
    {
        private readonly IRepository<BorrowBook, Guid> _borrowBookRepository;
        private readonly IRepository<BorrowBookDetail, Guid> _borrowBookDetailRepository;
        private readonly IRepository<BookLibrary, Guid> _bookLibraryRepository;

        public BorrowBookDetaiAppService(IRepository<BorrowBook, Guid> borrowBookRepository,
                                        IRepository<BorrowBookDetail, Guid> borrowBookDetailRepository,
                                        IRepository<BookLibrary, Guid> bookLibraryRepository)
        {
            _borrowBookRepository = borrowBookRepository;
            _borrowBookDetailRepository = borrowBookDetailRepository;
            _bookLibraryRepository = bookLibraryRepository;
        }

        //Get all borrow book detail
        [HttpGet]
        public async Task<List<GetAllBorrowBookDetailDto>> GetAll()
        {
            var values = await _borrowBookDetailRepository
                .GetAll()
                .Select(result => new GetAllBorrowBookDetailDto
                {
                    Id = result.Id,
                    Book = result.Book.Name,
                    Library = result.Library.Name,
                    Qty = result.Qty,
                    PriceBorrow = result.PriceBorrow,
                    Total = result.Total
                })
                .ToListAsync();
            return values;
        }

        //Add borrow book detail
        [HttpPost]
        public async Task AddBorrowBookDetail(List<BorrowBookDetailDto> input)
        {
            List<string> errorList = new List<string>();

            DateTime today = DateTime.Now;
            //Add id BorrowBook
            var borrowBook = new BorrowBook
            {
                DateBorrow = today
            };
            await _borrowBookRepository.InsertAsync(borrowBook);

            int allTotal = 0;

            var items = _bookLibraryRepository.GetAll();

            //Add borrow book detail
            foreach (var borrowBookDetail in input)
            {
                var addBorrowBookDetail = new BorrowBookDetail
                {
                    Qty = borrowBookDetail.Qty,
                    BookId = borrowBookDetail.BookId,
                    LibraryId = borrowBookDetail.LibraryId,
                    PriceBorrow = borrowBookDetail.PriceBorrow,
                    Total = borrowBookDetail.Qty * borrowBookDetail.PriceBorrow,
                    BorrowBookId = borrowBook.Id
                };

                allTotal = allTotal + (borrowBookDetail.Qty * borrowBookDetail.PriceBorrow);

                BorrowBookDetailValiadtor validator = new BorrowBookDetailValiadtor();
                ValidationResult validationResult = validator.Validate(addBorrowBookDetail);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        errorList.Add(string.Format("Error: {0}", error.ErrorMessage));
                    }
                    string errorString = string.Join(" ", errorList.ToArray());
                    throw new UserFriendlyException(errorString);
                }
                
                await _borrowBookDetailRepository.InsertAsync(addBorrowBookDetail);

                var book = items.Where(s => s.BookId == borrowBookDetail.BookId && s.LibraryId == borrowBookDetail.LibraryId)
                                    .FirstOrDefault();


                if (borrowBookDetail.Qty > book.Stock)
                {
                    throw new UserFriendlyException(string.Format("{0} have {1} in stock", book.Book.Name, book.Stock));
                }
                else
                {
                    book.Stock = book.Stock - addBorrowBookDetail.Qty;
                    await _bookLibraryRepository.UpdateAsync(book);
                }
            }

            //Update BorrowBook
            TimeSpan aInterval = new System.TimeSpan(5, 0, 0, 0);

            DateTime newTime = today.Add(aInterval);

            borrowBook.Total = allTotal;
            borrowBook.Status = "Đang xử lý";
            borrowBook.DateRepay = newTime;
            borrowBook.UserId = AbpSession.UserId.Value;
            await _borrowBookRepository.InsertAsync(borrowBook);
        }
    }
}
