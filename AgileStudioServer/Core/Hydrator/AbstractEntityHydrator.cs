using AgileStudioServer.Data;

namespace AgileStudioServer.Core.Hydrator
{
    public abstract class AbstractEntityHydrator : AbstractHydrator
    {
        protected DBContext _DBContext;

        public AbstractEntityHydrator(DBContext dBContext)
        {
            _DBContext = dBContext;
        }
    }
}
