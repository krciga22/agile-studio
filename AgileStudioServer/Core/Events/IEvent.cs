using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Core.Events
{
    /// <summary>
    /// General Event interface that all events should implement.
    /// </summary>
    public interface IEvent
    {

    }

    public interface IEvent<T> : IEvent
    {
        T Data { get; }
    }
}
