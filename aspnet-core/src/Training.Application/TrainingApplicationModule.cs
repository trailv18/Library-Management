using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Training.Authorization;

namespace Training
{
    [DependsOn(
        typeof(TrainingCoreModule), 
        typeof(AbpAutoMapperModule))]

    public class TrainingApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TrainingAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TrainingApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
