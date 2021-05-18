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
using Training.AppService.Libraries.Dto;
using Training.Entity.Libraries;
using Training.Entity.PageResults;
using Training.Validator.Libraries;

namespace Training.AppService.Libraries
{
    [AbpAuthorize]
    public class LibraryAppService : TrainingAppServiceBase
    {
        private readonly IRepository<Library, Guid> _libraryRepository;

        public LibraryAppService(IRepository<Library, Guid> libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        //Get library by id
        [HttpGet]
        public async Task<Library> GetLibraryById(Guid id)
        {
            return await _libraryRepository.GetAsync(id);
        }

        //Get all library
        [HttpGet]
        public async Task<List<GetLibraryDto>> GetAllLibrary()
        {

            var values = await _libraryRepository
                .GetAll()
                .Select(value => new GetLibraryDto
                {
                    Id = value.Id,
                    Name = value.Name,
                    District = value.District.Name,
                    Province = value.Province.Name
                })
                .ToListAsync();
            return values;
        }

        //Get library by page
        [HttpGet]
        public async Task<PageResult<GetLibraryDto>> GetLibraryByPage(LibraryFilterDto input)
        {
            var count = 0;
            var results = _libraryRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(input.LibraryName), x => x.Name.Contains(input.LibraryName))
                .Select(value => new GetLibraryDto
                {
                    Id = value.Id,
                    Name = value.Name,
                    District = value.District.Name,
                    Province = value.Province.Name
                });

            count = results.Count();

            var result = new PageResult<GetLibraryDto>
            {
                Count = count,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

        //Add library
        [HttpPost]
        public async Task AddLibrary(LibraryDto input)
        {
            List<string> errorList = new List<string>();

            var library = new Library
            {
                Name = input.Name,
                DistrictId = input.DistrictId,
                ProvinceId = input.ProvinceId
            };

            LibraryValidator validator = new LibraryValidator();
            ValidationResult validationResult = validator.Validate(library);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _libraryRepository.InsertAsync(library);
        }

        //Update library
        [HttpPut]
        public async Task UpdateLibrary(LibraryDto input)
        {
            List<string> errorList = new List<string>();

            var data = await GetLibraryById(input.Id);
            data.Name = input.Name;
            data.DistrictId = input.DistrictId;
            data.ProvinceId = input.ProvinceId;

            LibraryValidator validator = new LibraryValidator();
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

            await _libraryRepository.UpdateAsync(data);
        }

        //Delete library
        [HttpDelete]
        public async Task DeleteBook(DeleteLibraryDto input)
        {
            var data = await GetLibraryById(input.Id);

            await _libraryRepository.DeleteAsync(data);
        }
    }
}
