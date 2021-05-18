using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Training.Configuration;

namespace Training.Web.Host.Startup
{
    [DependsOn(
       typeof(TrainingWebCoreModule))]
    public class TrainingWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TrainingWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TrainingWebHostModule).GetAssembly());
        }
    }
}
