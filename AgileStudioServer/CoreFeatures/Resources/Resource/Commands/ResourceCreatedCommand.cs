using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Resources.Resource.Commands
{
    public class ResourceCreatedCommand(string type, object model, ServiceContext serviceContext) :
        AbstractCommand(serviceContext)
    {
        public string Type { get; set; } = type;

        public object Model { get; set; } = model;
    }
}
