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
using Training.AppService.Provinces.Dto;
using Training.Entity.PageResults;
using Training.Entity.Provinces;
using Training.FluentValidation.Provinces;

namespace Training.AppService.Provinces
{
    [AbpAuthorize]
    public class ProvinceAppService : TrainingAppServiceBase
    {
        private readonly IRepository<Province, Guid> _provinceRepository;

        public ProvinceAppService(IRepository<Province, Guid> provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }

        //Get province by Id
        [HttpGet]
        public async Task<Province> GetProvinceById(Guid id)
        {
            return await _provinceRepository.GetAsync(id);
        }

        //Get all province
        [HttpGet]
        public async Task<List<GetProvinceDto>> GetAllProvince()
        {

            var values = await _provinceRepository
                .GetAll()
                .Select(c => new GetProvinceDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
            return values;
        }

        //Get province by page
        [HttpGet]
        public async Task<PageResult<GetProvinceDto>> GetProvinceByPage(ProvinceFilterDto input)
        {
            var count = 0;
            var results = _provinceRepository
                    .GetAll()
                    .WhereIf(!String.IsNullOrEmpty(input.ProvinceName), x => x.Name.Contains(input.ProvinceName))
                    .Select(c => new GetProvinceDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                    });

            count = results.Count();

            var result = new PageResult<GetProvinceDto>
            {
                Count = count,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

        //Add provice
        [HttpPost]
        public async Task AddProvince(ProvinceDto input)
        {
            List<string> errorList = new List<string>();

            var province = new Province
            {
                Name = input.Name,
            };

            ProvinceValidator categoryValidator = new ProvinceValidator();
            ValidationResult validationResult = categoryValidator.Validate(province);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _provinceRepository.InsertAsync(province);
        }

        //Update province
        [HttpPut]
        public async Task UpdateProvince(ProvinceDto input)
        {
            List<string> errorList = new List<string>();

            var data = await GetProvinceById(input.Id);
            data.Name = input.Name;

            ProvinceValidator categoryValidator = new ProvinceValidator();
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
            await _provinceRepository.UpdateAsync(data);
        }

        //Delete province
        [HttpDelete]
        public async Task DeleteProvince(DeleteProvinceDto input)
        {
            var data = await GetProvinceById(input.Id);

            await _provinceRepository.DeleteAsync(data);
        }
    }
}
