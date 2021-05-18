using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Training.EntityFrameworkCore
{
    public static class TrainingDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TrainingDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TrainingDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
