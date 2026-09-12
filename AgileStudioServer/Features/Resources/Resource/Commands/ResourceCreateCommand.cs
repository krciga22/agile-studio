using AgileStudioServer.Core.Command;

namespace AgileStudioServer.Features.Resources.Resource.Commands
{
    public class ResourceCreateCommand(string type, object model) : AbstractCommand
    {
        public string Type { get; set; } = type;

        public object Model { get; set; } = model;
    }
}
