using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Projects.Projects.Commands
{
    public class ProjectCreateCommand(ProjectModel projectModel, ServiceContext serviceContext) : 
        AbstractCommand(serviceContext)
    {
        public ProjectModel ProjectModel { get; set; } = projectModel;

        public ProjectModel Result { get; set; } = null!;
    }
}
