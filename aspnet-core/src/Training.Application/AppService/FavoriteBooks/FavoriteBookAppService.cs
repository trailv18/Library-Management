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
using System.Threading.Tasks;
using Training.AppService.FavoriteBooks.Dto;
using Training.Entity.Books;
using Training.Entity.FavoriteBooks;
using Training.Entity.Libraries;
using Training.Entity.PageResults;
using Training.FluentValidation.FavoriteBooks;

namespace Training.AppService.FavoriteBooks
{
    [AbpAuthorize]
    public class FavoriteAppService : TrainingAppServiceBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IRepository<Library, Guid> _libraryRepository;
        private readonly IRepository<FavoriteBook, Guid> _favoriteBookRepository;

        public FavoriteAppService(IRepository<Book, Guid> bookRepository,
                                     IRepository<Library, Guid> libraryRepository,
                                     IRepository<FavoriteBook, Guid> favoriteBookRepository)
        {
            _bookRepository = bookRepository;
            _libraryRepository = libraryRepository;
            _favoriteBookRepository = favoriteBookRepository;
        }

        //Get book by id
        [HttpGet]
        public async Task<FavoriteBook> GetFavoriteBookById(Guid id)
        {
            return await _favoriteBookRepository.GetAsync(id);
        }

        //user get favorite book
        [HttpGet]
        public async Task<PageResult<GetFavoriteBookDto>> GetAllFavorite(FavoriteBookFilterDto input)
        {
            long userID = AbpSession.UserId.Value;
            var counts = 0;
            var results = _favoriteBookRepository.GetAll()
                .Where(user => user.UserId == userID)
                .Select(c => new GetFavoriteBookDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    BookLibraryId = c.BookLibraryId,
                    BookId = c.BookId,
                    LibraryId = c.LibraryId,
                    BookName = c.Book.Name,
                    LibraryName = c.Library.Name
                });

            counts = results.Count();
            var result = new PageResult<GetFavoriteBookDto>
            {
                Count = counts,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync()
            };

            return result;
        }


        //Add favorite
        [HttpPost]
        public async Task AddFavoriteBook(FavoriteBookDto input)
        {
            List<string> errorList = new List<string>();

            long userID = AbpSession.UserId.Value;

            var getData = _favoriteBookRepository.GetAll()
                .Where(x => x.LibraryId == input.LibraryId && x.BookId == input.BookId && x.UserId == userID)
                .FirstOrDefault();

            if (getData == null)
            {
                var favoriteBook = new FavoriteBook
                {
                    UserId = userID,
                    BookLibraryId = input.BookLibraryId,
                    BookId = input.BookId,
                    LibraryId = input.LibraryId
                };

                FavoriteBookValidator validator = new FavoriteBookValidator();
                ValidationResult validationResult = validator.Validate(favoriteBook);

                if (!validationResult.IsValid)
                {
                    foreach (var failure in validationResult.Errors)
                    {
                        errorList.Add(string.Format("{0}", failure.ErrorMessage));
                    }
                    string errorString = string.Join(" ", errorList.ToArray());
                    throw new UserFriendlyException(errorString);
                }
                await _favoriteBookRepository.InsertAsync(favoriteBook);
            }
            else
            {
                throw new UserFriendlyException(string.Format("The book is already in the favorites list!"));
            }
        }

        [HttpDelete]
        public async Task DeleteFavoriteBook(DeleteFavoriteBookDto input)
        {
            var data = await GetFavoriteBookById(input.Id);

            await _favoriteBookRepository.DeleteAsync(data);
        }
    }
}
