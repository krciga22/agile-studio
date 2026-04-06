namespace AgileStudioServer.Core.Command
{
    public interface ICommandListener
    {
        /// <summary>
        /// Gets the events that this listener is interested in.
        /// </summary>
        /// <returns>
        /// An array of service event types.
        /// </returns>
        Type[] GetEvents();

        /// <summary>
        /// Gets the priority of the listener.
        /// </summary>
        /// <returns>
        /// An integer representing the priority of the listener. Default is 0. 
        /// Listeners with lower values will be executed before those with higher values.
        /// </returns>
        int GetPriority();

        void Handle(ICommand serviceEvent);
    }
}
