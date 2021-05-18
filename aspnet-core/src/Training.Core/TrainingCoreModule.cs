using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Training.Authorization.Roles;
using Training.Authorization.Users;
using Training.Configuration;
using Training.Localization;
using Training.MultiTenancy;
using Training.Timing;

namespace Training
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class TrainingCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            TrainingLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = TrainingConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TrainingCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
