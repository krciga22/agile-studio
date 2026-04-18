using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.CoreFeatures.Accounts.WorkflowStates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Accounts.Workflows
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet(Name = "GetWorkflows")]
        [ProducesResponseType(typeof(List<WorkflowDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var models = _WorkflowService.GetAll();
            var dtos = _Hydrator.HydrateList<WorkflowDto>(models);
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetWorkflow")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(WorkflowDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _WorkflowService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = _Hydrator.Hydrate<WorkflowDto>(model);
            return Ok(dto);
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

        [HttpPost(Name = "CreateWorkflow")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(WorkflowPostDto workflowPostDto)
        {
            WorkflowModel model = _Hydrator.Hydrate<WorkflowModel>(workflowPostDto);
            model = _WorkflowService.Create(model);

            string workflowUrl = "";
            if (Url != null)
            {
                workflowUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? workflowUrl;
            }

            var dto = _Hydrator.Hydrate<WorkflowDto>(model);

            return Created(workflowUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateWorkflow")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, WorkflowPatchDto workflowPatchDto)
        {
            if (id != workflowPatchDto.ID)
            {
                return BadRequest();
            }

            WorkflowDto dto;
            try
            {
                WorkflowModel model = _Hydrator.Hydrate<WorkflowModel>(workflowPatchDto);
                model = _WorkflowService.Update(model);
                dto = _Hydrator.Hydrate<WorkflowDto>(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(WorkflowModel)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult(dto);
        }

        [HttpDelete("{id}", Name = "DeleteWorkflow")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            WorkflowModel? model = _WorkflowService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _WorkflowService.Delete(model);

            return new OkResult();
        }
    }
}