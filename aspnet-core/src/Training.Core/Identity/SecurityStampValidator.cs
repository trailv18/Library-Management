using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Training.Authorization.Roles;
using Training.Authorization.Users;
using Training.MultiTenancy;
using Microsoft.Extensions.Logging;

namespace Training.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            SignInManager signInManager,
            ISystemClock systemClock,
            ILoggerFactory loggerFactory) 
            : base(options, signInManager, systemClock, loggerFactory)
        {
        }
    }
}
