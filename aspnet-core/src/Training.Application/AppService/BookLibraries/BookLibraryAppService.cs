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
using Training.AppService.BookLibraries.Dto;
using Training.Entity.BookLibraries;
using Training.Entity.Books;
using Training.Entity.Libraries;
using Training.Entity.PageResults;
using Training.Validator.BookLibraries;

namespace Training.AppService.BookLibraries
{
    [AbpAuthorize]
    public class BookLibraryAppService : TrainingAppServiceBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IRepository<Library, Guid> _libraryRepository;
        private readonly IRepository<BookLibrary, Guid> _bookLibraryRepository;

        public BookLibraryAppService(IRepository<Book, Guid> bookRepository,
                                     IRepository<Library, Guid> libraryRepository,
                                     IRepository<BookLibrary, Guid> bookLibraryRepository)
        {
            _bookRepository = bookRepository;
            _libraryRepository = libraryRepository;
            _bookLibraryRepository = bookLibraryRepository;
        }

        //Get by id
        [HttpGet]
        public async Task<BookLibrary> GetBookLibraryById(Guid id)
        {
            return await _bookLibraryRepository.GetAsync(id);
        }

        //Get book of library by library id
        [HttpGet]
        public async Task<GetAllBookOfLibraryDto> GetBookOfLibraryById(Guid id)
        {
            var item = await _bookLibraryRepository.
                     GetAll()
                     .Where(d => d.Id == id)
                     .Select(value => new GetAllBookOfLibraryDto
                     {
                         Id = value.Id,
                         BookId = value.BookId,
                         BookName = value.Book.Name,
                         LibraryId = value.LibraryId,
                         PriceBorrow = value.Book.PriceBorrow,
                         Category = value.Book.Category.Name,
                         Stock = value.Stock,
                         Author = value.Book.Author.Name,
                         Publisher = value.Book.Publisher.Name,
                         YearPublic = value.Book.YearPublic
                     }).ToListAsync();
            return item.FirstOrDefault();
        }

        //Get book of library by criteria
        [HttpGet]
        public async Task<PageResult<GetAllBookOfLibraryDto>> GetBookOfLibraryByCriteria(BookLibraryFilterDto input)
        {
            var counts = 0;

            var books = _bookLibraryRepository.GetAll()
                        .Where(x => x.LibraryId == input.LibraryId)
                        .WhereIf(!String.IsNullOrEmpty(input.BookName) || !String.IsNullOrEmpty(input.CategoryName) || !String.IsNullOrEmpty(input.AuthorName) || !String.IsNullOrEmpty(input.PublisherName),
                            c => c.Book.Name.Contains(input.BookName) || c.Book.Category.Name == input.CategoryName || c.Book.Author.Name == input.AuthorName || c.Book.Publisher.Name == input.PublisherName)
                        .Select(book => new GetAllBookOfLibraryDto
                        {

                            Id = book.Id,
                            BookId = book.BookId,
                            BookName = book.Book.Name,
                            PriceBorrow = book.Book.PriceBorrow,
                            Category = book.Book.Category.Name,
                            CategoryId = book.Book.CategoryId,
                            Stock = book.Stock,
                            Author = book.Book.Author.Name,
                            AuthorId = book.Book.AuthorId,
                            Publisher = book.Book.Publisher.Name,
                            PublisherId = book.Book.PublisherId,
                            YearPublic = book.Book.YearPublic
                        });


            counts = books.Count();

            var result = new PageResult<GetAllBookOfLibraryDto>
            {
                Count = counts,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await books.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

        //Add book library
        [HttpPost]
        public async Task AddBookLibrary(BookLibraryDto input)
        {
            List<string> errorList = new List<string>();

            var value = new BookLibrary
            {
                BookId = input.BookId,
                LibraryId = input.LibraryId,
                Stock = input.Stock
            };

            BookLibraryValidator validator = new BookLibraryValidator();
            ValidationResult validationResult = validator.Validate(value);

            var datas = _bookLibraryRepository.GetAll();

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }

            var data = datas.Where(d => d.BookId == input.BookId && d.LibraryId == input.LibraryId).ToList();

            if (data.Count != 0)
            {
                foreach (var ed in data)
                {
                    ed.Stock = ed.Stock + input.Stock;

                    await _bookLibraryRepository.UpdateAsync(ed);
                }
            }
            else
            {
                await _bookLibraryRepository.InsertAsync(value);
            }
        }

        //Update book library
        [HttpPut]
        public async Task UpdateBookLibrary(BookLibraryDto input)
        {
            List<string> errorList = new List<string>();

            var data = await GetBookLibraryById(input.Id);
            data.Stock = input.Stock;

            BookLibraryValidator validator = new BookLibraryValidator();
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
            await _bookLibraryRepository.UpdateAsync(data);
        }

        //Delete book
        [HttpDelete]
        public async Task DeleteBookLbrary(DeleteBookLibraryDto input)
        {
            var sp = await GetBookLibraryById(input.Id);
            await _bookLibraryRepository.DeleteAsync(sp);
        }

    }
}
