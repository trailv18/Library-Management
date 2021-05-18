using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Training.Configuration;
using Training.Web;

namespace Training.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class TrainingDbContextFactory : IDesignTimeDbContextFactory<TrainingDbContext>
    {
        public TrainingDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TrainingDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            TrainingDbContextConfigurer.Configure(builder, configuration.GetConnectionString(TrainingConsts.ConnectionStringName));

            return new TrainingDbContext(builder.Options);
        }
    }
}
