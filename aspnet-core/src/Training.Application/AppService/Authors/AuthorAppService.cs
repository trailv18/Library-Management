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
using Training.AppService.Authors.Dto;
using Training.Entity.Authors;
using Training.Entity.PageResults;
using Training.FluentValidation.Authors;

namespace Training.AppService.Authors
{
    [AbpAuthorize]
    public class AuthorAppService : TrainingAppServiceBase
    {
        private readonly IRepository<Author, Guid> _authorRepository;

        public AuthorAppService(IRepository<Author, Guid> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        //Get author by id
        [HttpGet]
        public async Task<Author> GetAuthorById(Guid id)
        {
            return await _authorRepository.GetAsync(id);
        }

        //Get all author
        [HttpGet]
        public async Task<List<GetAuthorDto>> GetAllAuthor()
        {

            var values = await _authorRepository
                .GetAll()
                .Select(c => new GetAuthorDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    YearOfBirth = c.YearOfBirth,
                    Phone = c.Phone,
                    Address = c.Address
                })
                .ToListAsync();
            return values;
        }

        //Get author by page
        [HttpGet]
        public async Task<PageResult<GetAuthorDto>> GetAuthorByPage(AuthorFilterDto input)
        {
            var count = 0;
            var results = _authorRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(input.AuthorName), x => x.Name.Contains(input.AuthorName))
                .Select(c => new GetAuthorDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Address = c.Address,
                    YearOfBirth = c.YearOfBirth
                });

            count = results.Count();

            var result = new PageResult<GetAuthorDto>
            {
                Count = count,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

        //Add author
        [HttpPost]
        public async Task AddAuthor(AuthorDto input)
        {
            List<string> errorList = new List<string>();

            var author = new Author
            {
                Name = input.Name,
                YearOfBirth = input.YearOfBirth,
                Address = input.Address,
                Phone = input.Phone
            };

            AuthorValidator validator = new AuthorValidator();
            ValidationResult validationResult = validator.Validate(author);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _authorRepository.InsertAsync(author);
        }

        //Update author
        [HttpPut]
        public async Task UpdateAuthor(AuthorDto input)
        {
            List<string> errorList = new List<string>();

            var data = await GetAuthorById(input.Id);
            data.Name = input.Name;
            data.Address = input.Address;
            data.Phone = input.Phone;

            AuthorValidator validator = new AuthorValidator();
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

            await _authorRepository.UpdateAsync(data);
        }

        //Delete author
        [HttpDelete]
        public async Task DeleteAuthor(DeleteAuthorDto input)
        {
            var data = await GetAuthorById(input.Id);

            await _authorRepository.DeleteAsync(data);
        }
    }
}
