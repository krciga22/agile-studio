using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Features.Resources.Resource.Commands
{
    /// <summary>
    /// Command to delete a resource/model of the specified type and ID.
    /// </summary>
    public class ResourceDeletingCommand(string type, object[] id, ServiceContext serviceContext) :
        AbstractCommand(serviceContext)
    {
        public string Type { get; set; } = type;

        public object[] Id { get; set; } = id;
    }
}