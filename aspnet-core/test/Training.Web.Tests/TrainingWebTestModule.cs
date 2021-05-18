using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Training.EntityFrameworkCore;
using Training.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Training.Web.Tests
{
    [DependsOn(
        typeof(TrainingWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class TrainingWebTestModule : AbpModule
    {
        public TrainingWebTestModule(TrainingEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TrainingWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TrainingWebMvcModule).Assembly);
        }
    }
}