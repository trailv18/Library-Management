using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Training.Configuration;
using Training.EntityFrameworkCore;
using Training.Migrator.DependencyInjection;

namespace Training.Migrator
{
    [DependsOn(typeof(TrainingEntityFrameworkModule))]
    public class TrainingMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public TrainingMigratorModule(TrainingEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(TrainingMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                TrainingConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TrainingMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
