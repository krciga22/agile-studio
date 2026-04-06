using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Core.Command
{
    /// <summary>
    /// Base class for all commands.
    /// </summary>
    public abstract class AbstractCommand(ServiceContext serviceContext) : ICommand
    {
        public ServiceContext ServiceContext { get; set; } = serviceContext;
    }
}
