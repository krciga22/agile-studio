using AgileStudioServer.Core.Command;
using AgileStudioServer.CoreFeatures.Projects.Projects.Commands;

namespace AgileStudioServer.CoreFeatures.Projects.Projects.CommandListeners
{
    public class ProjectCreateCommandListener(ProjectRepository projectRepository) : ICommandListener
    {
        public Type[] GetEvents()
        {
            return [typeof(ProjectCreateCommand)];
        }

        public int GetPriority()
        {
            return CommandPriority.Normal;
        }

        public void Handle(ICommand serviceEvent)
        {
            if (serviceEvent is ProjectCreateCommand e)
            {
                e.Result = projectRepository.Create(e.ProjectModel);
            }
        }
    }
}
