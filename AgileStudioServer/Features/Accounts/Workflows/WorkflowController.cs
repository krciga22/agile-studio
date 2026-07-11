using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    [ApiController]
    [Route("Accounts/Workflows")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsWorkflow)]
    [MapResourcePatch(ResourceTypes.AccountsWorkflow)]
    [MapResourceDelete(ResourceTypes.AccountsWorkflow)]
    [Authorize]
    public class WorkflowController : ControllerBase
    {
        private readonly WorkflowService _WorkflowService;
        private readonly WorkflowStateService _WorkflowStateService;
        private readonly Hydrator _Hydrator;

        public WorkflowController(WorkflowService workflowService,
            WorkflowStateService workflowStateService, Hydrator hydrator)
        {
            _WorkflowService = workflowService;
            _WorkflowStateService = workflowStateService;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}/WorkflowStates", Name = "GetWorkflowWorkflowStates")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<WorkflowStateDto>), StatusCodes.Status200OK)]
        public IActionResult GetWorkflowStatesForWorkflow(int id)
        {
            var workflow = _WorkflowService.Get(id);
            if (workflow == null)
            {
                return NotFound();
            }

            var models = _WorkflowStateService.GetByWorkflowId(workflow.ID);
            var dtos = _Hydrator.HydrateList<WorkflowStateDto>(models);
            return Ok(dtos);
        }
    }
}