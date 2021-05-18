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
using Training.AppService.Categories.Dto;
using Training.Entity.Categories;
using Training.Entity.PageResults;
using Training.FluentValidation.Categories;

namespace Training.AppService.Categories
{
    [AbpAuthorize]
    public class CategoryAppService : TrainingAppServiceBase
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryAppService(IRepository<Category, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //Get category by id
        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _categoryRepository.GetAsync(id);
        }

        //Get all category
        [HttpGet]
        public async Task<List<GetCategoryDto>> GetAllCategory()
        {
            var values = await _categoryRepository
                .GetAll()
                .Select(value => new GetCategoryDto
                {
                    Id = value.Id,
                    Name = value.Name
                }).ToListAsync();

            return values;
        }

        //Get category by page
        [HttpGet]
        public async Task<PageResult<GetCategoryDto>> GetCategoryByPage(CategoryFilterDto input)
        {
            var counts = 0;

            var results = _categoryRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(input.CategoryName), x => x.Name.Contains(input.CategoryName))
                .Select(value => new GetCategoryDto
                {
                    Id = value.Id,
                    Name = value.Name
                });
            counts = results.Count();

            var result = new PageResult<GetCategoryDto>
            {
                Count = counts,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

        //Add category
        [HttpPost]
        public async Task AddCategory(CategoryDto input)
        {
            List<string> errorList = new List<string>();

            var data = new Category
            {
                Name = input.Name
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult validationResult = categoryValidator.Validate(data);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _categoryRepository.InsertAsync(data);
        }

        //Update category
        [HttpPut]
        public async Task UpdateCategory(CategoryDto input)
        {

            List<string> errorList = new List<string>();

            var ct = await GetCategoryById(input.Id);
            ct.Name = input.Name;

            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult validationResult = categoryValidator.Validate(ct);


            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }

            await _categoryRepository.UpdateAsync(ct);
        }

        //Delete category
        [HttpDelete]
        public async Task DeleteCategory(DeleteCategoryDto input)
        {
            var ct = await GetCategoryById(input.Id);

            await _categoryRepository.DeleteAsync(ct);
        }
    }
}
