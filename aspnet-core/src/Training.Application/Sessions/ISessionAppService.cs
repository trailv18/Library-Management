using System.Threading.Tasks;
using Abp.Application.Services;
using Training.Sessions.Dto;

namespace Training.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
