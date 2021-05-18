using System.Threading.Tasks;
using Training.Configuration.Dto;

namespace Training.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
