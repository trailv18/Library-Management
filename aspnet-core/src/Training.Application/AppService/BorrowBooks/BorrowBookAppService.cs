using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Training.AppService.BorrowBooks.Dto;
using Training.Authorization;
using Training.Entity.BorrowBookDetails;
using Training.Entity.BorrowBooks;
using Training.Entity.PageResults;
using Training.Validator.BorrowBooks;

namespace Training.AppService.BorrowBooks
{
    [AbpAuthorize]
    public class BorrowBookAppService : TrainingAppServiceBase
    {
        private readonly IRepository<BorrowBook, Guid> _borrowBookRepository;
        private readonly IRepository<BorrowBookDetail, Guid> _borrowBookDetailRepository;


        public BorrowBookAppService(IRepository<BorrowBook, Guid> borrowBookRepository,
                                IRepository<BorrowBookDetail, Guid> borrowBookDetailRepository)
        {
            _borrowBookDetailRepository = borrowBookDetailRepository;
            _borrowBookRepository = borrowBookRepository;
        }

        //
        [HttpGet]
        public async Task<PageResult<GetAllBorrowBookDto>> GetPageBorrowBook(BorrowBookFilterDto input)
        {
            var count = 0;
            var results = _borrowBookRepository
                .GetAll()
                .WhereIf(input.FromDate.HasValue && input.ToDate.HasValue || input.Month != null,
                    x => x.DateBorrow.Date >= input.FromDate && x.DateRepay.Date <= input.ToDate || x.DateBorrow.Month == input.Month)
                .Select(value => new GetAllBorrowBookDto
                {
                    Id = value.Id,
                    DateBorrow = value.DateBorrow,
                    DateRepay = value.DateRepay,
                    Status = value.Status,
                    Total = value.Total,
                    User = value.User.FullName
                }).OrderByDescending(x => x.DateBorrow);

            count = results.Count();

            var result = new PageResult<GetAllBorrowBookDto>
            {
                Count = count,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip(((input.PageIndex - 1) * input.PageSize)).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

        //
        [HttpGet]
        public async Task<List<GetBorrowBookDto>> GetBorrowBookDetailById(Guid id)
        {
            var values = await _borrowBookRepository
                .GetAll()
                .Where(d => d.Id == id)
                .Select(value => new GetBorrowBookDto
                {
                    Id = value.Id,
                    BorrowBookDetails = _borrowBookDetailRepository
                                        .GetAll()
                                        .Where(xx => xx.BorrowBookId == value.Id)
                                        .Select(data => new GetBorrowBookDetailDto
                                        {
                                            Id = data.Id,
                                            Book = data.Book.Name,
                                            Category = data.Book.Category.Name,
                                            Author = data.Book.Author.Name,
                                            Publisher = data.Book.Publisher.Name,
                                            PriceBorrow = data.PriceBorrow,
                                            Qty = data.Qty,
                                            Total = data.Total
                                        }).ToList(),
                    Status = value.Status,
                    Total = value.Total
                })
                .ToListAsync();
            return values;
        }

        [HttpGet]
        public async Task<BorrowBook> GetById(Guid id)
        {
            return await _borrowBookRepository.GetAsync(id);
        }

        //Update status
        [AbpAuthorize(PermissionNames.Pages_Librarians)]
        [HttpPut]
        public async Task UpdateStatus(UpdateStatusDto input)
        {
            var data = await GetById(input.Id);
            data.Status = input.Status;
            //data.LibrarianId = input.


            BorrowBookValiadtor validator = new BorrowBookValiadtor();
            ValidationResult validationResult = validator.Validate(data);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    throw new UserFriendlyException(string.Format("{0}", failure.ErrorMessage));
                }
            }
            else
            {
                await _borrowBookRepository.UpdateAsync(data);
            }
        }

        //get borrow book of user
        [HttpGet]
        public async Task<PageResult<GetAllBorrowBookDto>> GetBorrowBookPageByUserId(BorrowBookFilterDto input)
        {
            long userID = AbpSession.UserId.Value;
            var count = 0;
            var results = _borrowBookRepository
                    .GetAll()
                    .Where(user => user.UserId == userID)
                    .WhereIf(input.FromDate.HasValue && input.ToDate.HasValue || input.Month != null,
                        x => x.DateBorrow.Date >= input.FromDate && x.DateRepay.Date <= input.ToDate || x.DateBorrow.Month == input.Month)
                    .Select(value => new GetAllBorrowBookDto
                    {
                        Id = value.Id,
                        DateBorrow = value.DateBorrow,
                        DateRepay = value.DateRepay,
                        Status = value.Status,
                        Total = value.Total,
                        User = value.User.FullName
                    }).OrderByDescending(x => x.DateBorrow);


            count = results.Count();

            var result = new PageResult<GetAllBorrowBookDto>
            {
                Count = count,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip(((input.PageIndex - 1) * input.PageSize)).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

    }
}
