using AgileStudioServer.Data;
using AgileStudioServerTest.IntegrationTests;
using Microsoft.EntityFrameworkCore;

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

            return optionsBuilder.UseNpgsql(
                    dbTestContainer.GetConnectionString()
                )
                .UseSnakeCaseNamingConvention()
                .Options;
        }
    }
}
