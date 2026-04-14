using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Resources.Resource.Commands
{
    public class ResourceUpdatingCommand(string type, object[] id, object model, ServiceContext serviceContext) :
        AbstractCommand(serviceContext)
    {
        public string Type { get; set; } = type;

        public object[] Id { get; set; } = id;

        public object Model { get; set; } = model;
    }
}