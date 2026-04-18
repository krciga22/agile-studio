using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Accounts.WorkflowStates
{
    [ApiController]
    [Route("Accounts/[controller]")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [Authorize]
    public class WorkflowStateController : ControllerBase
    {
        private readonly WorkflowStateService _WorkflowStateService;
        private readonly Hydrator _Hydrator;

        public WorkflowStateController(WorkflowStateService workflowStateService, Hydrator hydrator)
        {
            _WorkflowStateService = workflowStateService;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}", Name = "GetWorkflowState")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(WorkflowStateDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _WorkflowStateService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = _Hydrator.Hydrate<WorkflowStateDto>(model);

            return Ok(dto);
        }

        [HttpPost(Name = "CreateWorkflowState")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowStateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(WorkflowStatePostDto workflowStatePostDto)
        {
            var model = _Hydrator.Hydrate<WorkflowStateModel>(workflowStatePostDto);
            model = _WorkflowStateService.Create(model);

            string workflowStateUrl = "";
            if (Url != null)
            {
                workflowStateUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? workflowStateUrl;
            }

            var dto = _Hydrator.Hydrate<WorkflowStateDto>(model);

            return Created(workflowStateUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateWorkflowState")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowStateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, WorkflowStatePatchDto workflowStatePatchDto)
        {
            if (id != workflowStatePatchDto.ID)
            {
                return BadRequest();
            }

            WorkflowStateDto dto;
            try
            {
                WorkflowStateModel model = _Hydrator.Hydrate<WorkflowStateModel>(workflowStatePatchDto);
                model = _WorkflowStateService.Update(model);
                dto = _Hydrator.Hydrate<WorkflowStateDto>(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(WorkflowStateModel)))
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

        [HttpDelete("{id}", Name = "DeleteWorkflowState")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var model = _WorkflowStateService.Get(id);
            if (model is null)
            {
                return NotFound();
            }

            _WorkflowStateService.Delete(model);

            return new OkResult();
        }
    }
}