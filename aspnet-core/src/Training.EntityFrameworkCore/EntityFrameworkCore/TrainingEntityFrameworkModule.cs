using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Training.EntityFrameworkCore.Seed;

namespace Training.EntityFrameworkCore
{
    [DependsOn(
        typeof(TrainingCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class TrainingEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<TrainingDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        TrainingDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        TrainingDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TrainingEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
