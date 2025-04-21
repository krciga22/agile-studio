using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests
{
    public abstract class AbstractServiceTest : DBTest
    {
        public AbstractServiceTest(DBContext dbContext) : base(dbContext)
        {

        }
    }
}
