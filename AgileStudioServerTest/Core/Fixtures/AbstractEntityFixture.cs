namespace AgileStudioServerTest.Core.Fixtures
{
    public abstract class AbstractEntityFixture<TRepository>
        where TRepository : class
    {
        protected readonly TRepository _Repository;

        protected AbstractEntityFixture(TRepository repository)
        {
            _Repository = repository;
        }
    }
}
