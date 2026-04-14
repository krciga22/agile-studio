using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Resources.Resource.Commands
{
    public class ResourceDeletedCommand(string type, object[] id, ServiceContext serviceContext) :
        AbstractCommand(serviceContext)
    {
        public string Type { get; set; } = type;

        public object[] Id { get; set; } = id;
    }
}