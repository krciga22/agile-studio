using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Projects.Projects
{
    [ApiController]
    [Route("Projects/Projects")]
    [ApiExplorerSettings(GroupName = "projects")]
    [MapResourceGetCollection(ResourceTypes.ProjectsProject)]
    [MapResourceGet(ResourceTypes.ProjectsProject)]
    [MapResourcePost(ResourceTypes.ProjectsProject)]
    [MapResourcePatch(ResourceTypes.ProjectsProject)]
    [MapResourceDelete(ResourceTypes.ProjectsProject)]
    [MapSubResourceGetCollection(
        ResourceTypes.ProjectsProject,
        ResourceTypes.ReleasesRelease,
        "Releases")]
    [MapSubResourcePost(
        ResourceTypes.ProjectsProject,
        ResourceTypes.ReleasesRelease,
        "Releases")]
    [MapSubResourceGetCollection(
        ResourceTypes.ProjectsProject,
        ResourceTypes.SprintsSprint,
        "Sprints")]
    [MapSubResourcePost(
        ResourceTypes.ProjectsProject,
        ResourceTypes.SprintsSprint,
        "Sprints")]
    [MapSubResourceGetCollection(
        ResourceTypes.ProjectsProject,
        ResourceTypes.BacklogItemsBacklogItem,
        "BacklogItems")]
    [MapSubResourcePost(
        ResourceTypes.ProjectsProject,
        ResourceTypes.BacklogItemsBacklogItem,
        "BacklogItems")]
    [Authorize]
    public class ProjectController : ControllerBase
    {

    }
}