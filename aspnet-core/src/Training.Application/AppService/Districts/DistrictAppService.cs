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
using Training.AppService.Districts.Dto;
using Training.Entity.Districts;
using Training.Entity.PageResults;
using Training.Validator.Districts;

namespace Training.AppService.Districts
{
    [AbpAuthorize]
    public class DistrictAppService : TrainingAppServiceBase
    {
        private readonly IRepository<District, Guid> _districtRepository;

        public DistrictAppService(IRepository<District, Guid> districtRepository)
        {
            _districtRepository = districtRepository;
        }

        //Get district by id
        [HttpGet]
        public async Task<District> GetDistrictById(Guid id)
        {
            return await _districtRepository.GetAsync(id);
        }

        //Get all district
        [HttpGet]
        public async Task<List<GetDistrictDto>> GetAllDistrict()
        {

            var values = await _districtRepository
                .GetAll()
                .Select(value => new GetDistrictDto
                {
                    Id = value.Id,
                    Name = value.Name,
                    ProvinceName = value.Province.Name
                })
                .ToListAsync();
            return values;
        }

        //Get district by province id
        [HttpGet]
        public async Task<List<GetDistrictDto>> GetDistrictByProvinceId(Guid id)
        {

            var values = await _districtRepository
                .GetAll()
                .Where(d => d.ProvinceId == id)
                .Select(value => new GetDistrictDto
                {
                    Id = value.Id,
                    Name = value.Name
                })
                .ToListAsync();
            return values;
        }


        //Get district page by province id 
        [HttpGet]
        public async Task<PageResult<DistrictDto>> GetDistrictPageByProvinceId(DistrictFilterDto input)
        {
            var count = 0;
            var results = _districtRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(input.DistrictName), x => x.Name.Contains(input.DistrictName))
                .Where(d => d.ProvinceId == input.Id)
                .Select(c => new DistrictDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProvinceId = c.ProvinceId
                });

            count = results.Count();

            var result = new PageResult<DistrictDto>
            {
                Count = count,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

        //Add district
        [HttpPost]
        public async Task AddDistrict(DistrictDto input)
        {
            List<string> errorList = new List<string>();

            var district = new District
            {
                Name = input.Name,
                ProvinceId = input.ProvinceId,

            };

            DistrictValidator validator = new DistrictValidator();
            ValidationResult validationResult = validator.Validate(district);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _districtRepository.InsertAsync(district);
        }


        //Update district
        [HttpPut]
        public async Task UpdateDistrict(DistrictDto input)
        {
            List<string> errorList = new List<string>();

            var data = await GetDistrictById(input.Id);
            data.Name = input.Name;
            data.ProvinceId = input.ProvinceId;

            DistrictValidator validator = new DistrictValidator();
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
            await _districtRepository.UpdateAsync(data);
        }

        [HttpDelete]
        public async Task DeleteDistrict(DeleteDistrictDto input)
        {
            var data = await GetDistrictById(input.Id);

            await _districtRepository.DeleteAsync(data);
        }

    }
}
