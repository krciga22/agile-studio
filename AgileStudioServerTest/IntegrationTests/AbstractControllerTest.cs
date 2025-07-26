
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests
{
    public abstract class AbstractControllerTest : DBTest
    {
        public AbstractControllerTest(DBContext dbContext) : base(dbContext)
        {

        }
    }
}
