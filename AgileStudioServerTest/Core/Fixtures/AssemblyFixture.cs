
using AgileStudioServer.Data;
using AgileStudioServerTest.IntegrationTests;
using Microsoft.EntityFrameworkCore;

[assembly: AssemblyFixture(typeof(AgileStudioServerTest.Core.Fixtures.AssemblyFixture))]

namespace AgileStudioServerTest.Core.Fixtures
{
    public sealed class AssemblyFixture : IDisposable
    {
        private readonly DBTestContainer _dBTestContainer;

        public AssemblyFixture(DBContext dBContext, DBTestContainer dBTestContainer)
        {
            dBContext.Database.Migrate();
            _dBTestContainer = dBTestContainer;
        }

        public void Dispose()
        {
            if (_dBTestContainer.IsStarted())
            {
                _dBTestContainer.Stop();
            }
        }
    }
}
