namespace AgileStudioServer.Core.Command
{
    public class CommandDispatcher(IEnumerable<ICommandListener> serviceCommandListeners)
    {
        private readonly IEnumerable<ICommandListener> _ServiceCommandListeners = serviceCommandListeners;

        public void Dispatch(ICommand serviceEvent)
        {
            List<ICommandListener> listeners = _ServiceCommandListeners.Where(
                    l => l.GetEvents().Contains(serviceEvent.GetType())).ToList();

            listeners = listeners.OrderBy(l => l.GetPriority()).ToList();

            listeners.ForEach(listener => {
                try
                {
                    listener.Handle(serviceEvent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling service event {nameof(serviceEvent)} with listener {nameof(listener)}: {ex.Message}");
                }
            });
        }
    }
}
