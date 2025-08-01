using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServerTest.IntegrationTests
{
    public class DBTest : IDisposable
    {
        protected readonly DBContext _DBContext;

        public DBTest(DBContext dbContext)
        {
            _DBContext = dbContext;
            _DBContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _DBContext.Database.RollbackTransaction();
            GC.SuppressFinalize(this);
        }
    }
}
