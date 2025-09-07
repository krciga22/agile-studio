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

        public static DbContextOptions ConfigureDefaultOptions(ref DbContextOptionsBuilder optionsBuilder, IConfiguration? configuration = null)
        {
            return optionsBuilder.UseMySql(
                    GetConnectionString(configuration),
                    ServerVersion.Create(
                        new Version("8.0"),
                        Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql
                    )
                )
                .UseSnakeCaseNamingConvention()
                .Options;
        }

        private static string GetConnectionString(IConfiguration? configuration = null)
        {
            string? dbHost, dbPort, dbName, dbUser, dbPass;

            if (configuration == null)
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    .AddUserSecrets(Assembly.GetExecutingAssembly())
                    .AddEnvironmentVariables();

                configuration = builder.Build();
            }

            dbHost = configuration.GetValue<string>("DB_HOST");
            dbPort = configuration.GetValue<string>("DB_PORT");
            dbName = configuration.GetValue<string>("DB_NAME");
            dbUser = configuration.GetValue<string>("DB_USER");
            dbPass = configuration.GetValue<string>("DB_PASS");

            return string.Format(
                "server={0};port={1};database={2};user={3};password={4};",
                dbHost, dbPort, dbName, dbUser, dbPass
            );
        }
    }
}
