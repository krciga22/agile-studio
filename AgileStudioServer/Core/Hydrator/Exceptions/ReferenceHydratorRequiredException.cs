using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Core.Hydrators.Exceptions
{
    public class ReferenceHydratorRequiredException : AbstractException
    {
        public ReferenceHydratorRequiredException(IHydrator hydrator) : base($"The hydrator {nameof(hydrator)} requires a reference hydrator")
        {

        }
    }
}
