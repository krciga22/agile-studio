using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Releases.Releases
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ReleaseController : ControllerBase
    {
        private readonly ReleaseService _ReleaseService;
        private readonly ProjectService _ProjectService;
        private readonly Hydrator _Hydrator;

        public ReleaseController(
            ReleaseService releaseService,
            ProjectService projectService,
            Hydrator hydrator)
        {
            _ReleaseService = releaseService;
            _ProjectService = projectService;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}", Name = "GetRelease")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReleaseDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _ReleaseService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = HydrateReleaseDto(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateRelease")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(ReleasePostDto releasePostDto)
        {
            ReleaseModel model = HydrateReleaseModel(releasePostDto);
            model = _ReleaseService.Create(model);

            var releaseUrl = "";
            if (Url != null)
            {
                releaseUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? releaseUrl;
            }

            var dto = HydrateReleaseDto(model);

            return Created(releaseUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateRelease")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, ReleasePatchDto releasePatchDto)
        {
            if (id != releasePatchDto.ID)
            {
                return BadRequest();
            }

            ReleaseDto dto;
            try
            {
                ReleaseModel model = HydrateReleaseModel(releasePatchDto);
                model = _ReleaseService.Update(model);
                dto = HydrateReleaseDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(ReleaseModel)))
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

        [HttpDelete("{id}", Name = "DeleteRelease")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            ReleaseModel? model = _ReleaseService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _ReleaseService.Delete(model);

            return new OkResult();
        }

        private ReleaseDto HydrateReleaseDto(ReleaseModel release, int depth = 1)
        {
            return (ReleaseDto)_Hydrator.Hydrate(
                release, typeof(ReleaseDto), depth
            );
        }

        private ReleaseModel HydrateReleaseModel(ReleasePostDto releasePostDto, int depth = 3)
        {
            return (ReleaseModel)_Hydrator.Hydrate(
                releasePostDto, typeof(ReleaseModel), depth
            );
        }

        private ReleaseModel HydrateReleaseModel(ReleasePatchDto releasePatchDto, int depth = 3)
        {
            return (ReleaseModel)_Hydrator.Hydrate(
                releasePatchDto, typeof(ReleaseModel), depth
            );
        }
    }
}