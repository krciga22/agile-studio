using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests
{
    public abstract class AbstractControllerNewTest : DBTest
    {
        public AbstractControllerNewTest(DBContext dbContext) : base(dbContext)
        {

        }
    }
}
