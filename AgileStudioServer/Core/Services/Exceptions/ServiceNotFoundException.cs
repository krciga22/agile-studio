using AgileStudioServer.Core.Exceptions;

namespace AgileStudioServer.Core.Services.Exceptions
{
    public class ServiceNotFoundException : AbstractException
    {
        public ServiceNotFoundException(string serviceName) : base($"Required service \"{serviceName}\" not found")
        {

        }
    }
}
