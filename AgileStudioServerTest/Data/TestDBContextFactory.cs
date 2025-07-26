using AgileStudioServer.Data;
using AgileStudioServerTest.IntegrationTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Testcontainers.MySql;

namespace AgileStudioServerTest.Data
{
    public class TestDBContextFactory
    {
        public static DBContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var options = ConfigureDefaultOptions(ref optionsBuilder);
            return new DBContext(options);
        }

        public static DbContextOptions ConfigureDefaultOptions(ref DbContextOptionsBuilder optionsBuilder)
        {
            DBTestContainer dbTestContainer = DBTestContainer.GetInstance();
            return optionsBuilder.UseMySql(
                    dbTestContainer.GetConnectionString(),
                    ServerVersion.Create(
                        new Version("8.0"),
                        Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql
                    )
                )
                .UseSnakeCaseNamingConvention()
                .Options;
        }
    }
}
