using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Projects.Sprints
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SprintController : ControllerBase
    {
        private SprintService _SprintService;
        private readonly Hydrator _Hydrator;

        public SprintController(
            SprintService sprintService,
            Hydrator hydrator)
        {
            _SprintService = sprintService;
            _Hydrator = hydrator;
        }


        [HttpGet("{id}", Name = "GetSprint")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SprintDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _SprintService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = _Hydrator.Hydrate<SprintDto>(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(SprintPostDto sprintPostDto)
        {
            SprintModel model = _Hydrator.Hydrate<SprintModel>(sprintPostDto);
            model = _SprintService.Create(model);

            var sprintUrl = "";
            if (Url != null)
            {
                sprintUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? sprintUrl;
            }

            var dto = _Hydrator.Hydrate<SprintDto>(model);

            return Created(sprintUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, SprintPatchDto sprintPatchDto)
        {
            if (id != sprintPatchDto.ID)
            {
                return BadRequest();
            }

            SprintDto dto;
            try
            {
                SprintModel model = _Hydrator.Hydrate<SprintModel>(sprintPatchDto);
                model = _SprintService.Update(model);
                dto = _Hydrator.Hydrate<SprintDto>(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(SprintModel)))
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

        [HttpDelete("{id}", Name = "DeleteSprint")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            SprintModel? model = _SprintService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _SprintService.Delete(model);

            return new OkResult();
        }
    }
}