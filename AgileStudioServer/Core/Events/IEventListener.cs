namespace AgileStudioServer.Core.Events
{
    public interface IEventListener
    {
        /// <summary>
        /// Gets the events handled by this listener.
        /// </summary>
        Type[] GetEvents();

        /// <summary>
        /// Gets the priority of the listener.
        /// </summary>
        /// <returns>
        /// An integer representing the priority of this 
        /// event listener. Default is 0. Listeners with 
        /// lower values will be executed before those 
        /// with higher values.
        /// </returns>
        int GetPriority();

        void Handle(IEvent e);
    }
}
