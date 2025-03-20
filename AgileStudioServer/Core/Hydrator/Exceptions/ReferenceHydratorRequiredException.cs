using AgileStudioServer.Core.Exceptions;
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.Core.Hydrators.Exceptions
{
    public class ReferenceHydratorRequiredException : AbstractException
    {
        public ReferenceHydratorRequiredException(IHydrator hydrator) : base($"The hydrator {nameof(hydrator)} requires a reference hydrator")
        {

        }
    }
}
