using AgileStudioServer.Core.Events;

namespace AgileStudioServer.Features.Resources.Resource.Events
{
    public class ResourceCreatedEvent(string type, object model) : IEvent
    {
        public string Type { get; set; } = type;

        public object Model { get; set; } = model;
    }
}
