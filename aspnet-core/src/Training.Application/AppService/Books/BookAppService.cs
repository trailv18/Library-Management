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
using System.Threading.Tasks;
using Training.AppService.Books.Dto;
using Training.AppService.Common;
using Training.Entity.Books;
using Training.Entity.PageResults;
using Training.Validator.Books;

namespace Training.AppService.Books
{
    [AbpAuthorize]
    public class BookAppService : TrainingAppServiceBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;


        public BookAppService(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;

        }

        //Get book by id
        [HttpGet]
        public async Task<Book> GetBookById(Guid id)
        {
            return await _bookRepository.GetAsync(id);
        }

        //Get all book
        [HttpGet]
        public async Task<List<GetBookDto>> GetAllBook()
        {

            var values = await _bookRepository
                .GetAll()
                .Select(value => new GetBookDto
                {
                    Id = value.Id,
                    Name = value.Name,
                    PriceBorrow = value.PriceBorrow,
                    Category = value.Category.Name,
                    Author = value.Author.Name,
                    Publisher = value.Publisher.Name,
                    YearPublic = value.YearPublic
                })
                .ToListAsync();
            return values;
        }

        //Get book by page
        [HttpGet]
        public async Task<PageResult<GetBookDto>> GetBookByPage(BookFilterDto input)
        {


            var counts = 0;

            var results = _bookRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(input.BookName), x => x.Name.Contains(input.BookName))
                .Select(c => new GetBookDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    PriceBorrow = c.PriceBorrow,
                    Category = c.Category.Name,
                    Author = c.Author.Name,
                    Publisher = c.Publisher.Name,
                    YearPublic = c.YearPublic
                });

            counts = results.Count();

            var result = new PageResult<GetBookDto>
            {
                Count = counts,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

        //Add book
        [HttpPost]
        public async Task AddBook(BookDto input)
        {
            List<string> errorList = new List<string>();

            var book = new Book
            {
                Name = input.Name.Remove(),
                PriceBorrow = input.PriceBorrow,
                CategoryId = input.CategoryId,
                PublisherId = input.PublisherId,
                AuthorId = input.AuthorId,
                YearPublic = input.YearPublic
            };

            BookValidator validator = new BookValidator();
            ValidationResult validationResult = validator.Validate(book);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _bookRepository.InsertAsync(book);
        }

        //Update book
        [HttpPut]
        public async Task UpdateBook(BookDto input)
        {
            List<string> errorList = new List<string>();

            var data = await GetBookById(input.Id);
            data.Name = input.Name.Remove();
            data.CategoryId = input.CategoryId;
            data.PublisherId = input.PublisherId;
            data.AuthorId = input.AuthorId;
            data.YearPublic = input.YearPublic;

            BookValidator validator = new BookValidator();
            ValidationResult validationResult = validator.Validate(data);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _bookRepository.UpdateAsync(data);
        }

        [HttpDelete]
        public async Task DeleteBook(DeleteBookDto input)
        {
            var data = await GetBookById(input.Id);

            await _bookRepository.DeleteAsync(data);
        }
    }
}
