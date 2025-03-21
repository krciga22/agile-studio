using AgileStudioServer.Data;

namespace AgileStudioServer.Core.Hydrator
{
    public abstract class AbstractModelHydrator : AbstractHydrator
    {
        protected DBContext _DBContext;

        public AbstractModelHydrator(DBContext dbContext)
        {
            _DBContext = dbContext;
        }
    }
}
