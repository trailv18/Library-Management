using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Training.Controllers
{
    public abstract class TrainingControllerBase: AbpController
    {
        protected TrainingControllerBase()
        {
            LocalizationSourceName = TrainingConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
