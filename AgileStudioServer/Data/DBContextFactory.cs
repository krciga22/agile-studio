using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace AgileStudioServer.Data
{
    public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public static DBContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var options = ConfigureDefaultOptions(ref optionsBuilder);
            return new DBContext(options);
        }

        public DBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var options = ConfigureDefaultOptions(ref optionsBuilder);
            return new DBContext(options);
        }

        public static DbContextOptions ConfigureDefaultOptions(ref DbContextOptionsBuilder optionsBuilder)
        {
            return optionsBuilder.UseMySql(
                    GetConnectionString(),
                    ServerVersion.Create(
                        new Version("8.0"),
                        Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql
                    )
                )
                .UseSnakeCaseNamingConvention()
                .Options;
        }

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            var dbHost = configuration.GetValue<string>("DB_HOST");
            var dbPort = configuration.GetValue<string>("DB_PORT");
            var dbName = configuration.GetValue<string>("DB_NAME");
            var dbUser = configuration.GetValue<string>("DB_USER");
            var dbPass = configuration.GetValue<string>("DB_PASS");
            return string.Format(
                "server={0};port={1};database={2};user={3};password={4};",
                dbHost, dbPort, dbName, dbUser, dbPass
            );
        }
    }
}
