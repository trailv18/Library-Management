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
using Training.AppService.Publishers.Dto;
using Training.Entity.PageResults;
using Training.Entity.Publishers;
using Training.FluentValidation.Publishers;

namespace Training.AppService.Publishers
{
    [AbpAuthorize]
    public class PublisherAppService : TrainingAppServiceBase
    {
        private readonly IRepository<Publisher, Guid> _publisherRepository;

        public PublisherAppService(IRepository<Publisher, Guid> publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        //Get publisher by id
        [HttpGet]
        public async Task<Publisher> GetPublisherById(Guid id)
        {
            return await _publisherRepository.GetAsync(id);
        }

        //Get all publisher
        [HttpGet]
        public async Task<List<GetPublisherDto>> GetAllPublisher()
        {

            var values = await _publisherRepository
                .GetAll()
                .Select(c => new GetPublisherDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Address = c.Address,
                    Phone = c.Phone
                })
                .ToListAsync();
            return values;
        }

        //Get publisher by page
        [HttpGet]
        public async Task<PageResult<GetPublisherDto>> GetPublisherByPage(PublisherFilterDto input)
        {
            var count = 0;
            var results = _publisherRepository
                   .GetAll()
                   .WhereIf(!String.IsNullOrEmpty(input.PublisherName), x => x.Name.Contains(input.PublisherName))
                   .Select(p => new GetPublisherDto
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Email = p.Email,
                       Address = p.Address,
                       Phone = p.Phone
                   });

            count = results.Count();

            var result = new PageResult<GetPublisherDto>
            {
                Count = count,
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                Items = await results.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync()
            };

            return result;
        }

        //Add publisher
        [HttpPost]
        public async Task AddPublisher(PublisherDto input)
        {
            List<string> errorList = new List<string>();

            var publisher = new Publisher
            {
                Name = input.Name,
                Email = input.Email,
                Address = input.Address,
                Phone = input.Phone
            };

            PublisherValidator validator = new PublisherValidator();
            ValidationResult validationResult = validator.Validate(publisher);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _publisherRepository.InsertAsync(publisher);
        }

        //Update publisher
        [HttpPut]
        public async Task UpdatePublisher(PublisherDto input)
        {
            List<string> errorList = new List<string>();

            var data = await GetPublisherById(input.Id);
            data.Name = input.Name;
            data.Email = input.Email;
            data.Address = input.Address;
            data.Phone = input.Phone;

            PublisherValidator validator = new PublisherValidator();
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
            await _publisherRepository.UpdateAsync(data);

        }

        //Delete publisher
        [HttpDelete]
        public async Task DeletePublisher(DeletePublisherDto input)
        {
            var data = await GetPublisherById(input.Id);

            await _publisherRepository.DeleteAsync(data);
        }

    }
}
