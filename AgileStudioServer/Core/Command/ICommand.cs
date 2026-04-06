using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Core.Command
{
    /// <summary>
    /// General Command interface that all commands should implement.
    /// </summary>
    public interface ICommand
    {
        ServiceContext ServiceContext { get; set; }
    }
}
