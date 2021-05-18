using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Training.Configuration.Dto;

namespace Training.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TrainingAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
