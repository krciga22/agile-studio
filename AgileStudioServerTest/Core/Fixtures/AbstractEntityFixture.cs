
using AgileStudioServer.Data;

namespace AgileStudioServerTest.Core.Fixtures
{
    public abstract class AbstractEntityFixture
    {
        protected readonly DBContext _DBContext;

        protected AbstractEntityFixture(DBContext dbContext)
        {
            _DBContext = dbContext;
        }
    }
}
