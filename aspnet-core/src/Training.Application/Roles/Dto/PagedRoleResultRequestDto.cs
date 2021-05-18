using Abp.Application.Services.Dto;

namespace Training.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

