
using AgileStudioServerTest.IntegrationTests;

[assembly: AssemblyFixture(typeof(AgileStudioServerTest.Core.Fixtures.AssemblyFixture))]

namespace AgileStudioServerTest.Core.Fixtures
{
    public sealed class AssemblyFixture : IDisposable
    {
        public void Dispose()
        {
            DBTestContainer dbTestContainer = DBTestContainer.GetInstance();
            if (dbTestContainer.IsStarted())
            {
                dbTestContainer.Stop();
            }
        }
    }
}
