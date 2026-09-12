namespace AgileStudioServer.Core.Events
{
    public class EventDispatcher(IEnumerable<IEventListener> eventListeners)
    {
        private readonly IEnumerable<IEventListener> _EventListeners = eventListeners;

        public void Dispatch(IEvent e)
        {
            List<IEventListener> listeners = _EventListeners.Where(
                    l => l.GetEvents().Contains(e.GetType())).ToList();

            if(listeners.Count == 0){
                return;
            }

            listeners = listeners.OrderBy(l => l.GetPriority()).ToList();

            listeners.ForEach(listener => {
                try
                {
                    listener.Handle(e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling event {nameof(e)} with listener {nameof(listener)}: {ex.Message}");
                }
            });
        }
    }
}
