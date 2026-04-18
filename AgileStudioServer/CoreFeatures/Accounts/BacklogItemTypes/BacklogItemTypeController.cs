using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Accounts.ChildBacklogItemTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypes
{
    [ApiController]
    [Route("Accounts/[controller]")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [Authorize]
    public class BacklogItemTypeController : ControllerBase
    {
        private readonly BacklogItemTypeService _BacklogItemTypeService;

        private readonly ChildBacklogItemTypeService _ChildBacklogItemTypeService;

        private readonly BacklogItemTypeSchemaService _BacklogItemTypeSchemaService;

        private readonly Hydrator _Hydrator;

        public BacklogItemTypeController(
            BacklogItemTypeService dataProvider,
            Hydrator hydrator,
            ChildBacklogItemTypeService childBacklogItemTypeService,
            BacklogItemTypeSchemaService backlogItemTypeSchemaService)
        {
            _BacklogItemTypeService = dataProvider;
            _Hydrator = hydrator;
            _ChildBacklogItemTypeService = childBacklogItemTypeService;
            _BacklogItemTypeSchemaService = backlogItemTypeSchemaService;
        }

        [HttpGet("{id}", Name = "GetBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _BacklogItemTypeService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = _Hydrator.Hydrate<BacklogItemTypeDto>(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateBacklogItemType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemTypePostDto backlogItemTypePostDto)
        {
            BacklogItemTypeModel model = _Hydrator.Hydrate<BacklogItemTypeModel>(backlogItemTypePostDto);
            model = _BacklogItemTypeService.Create(model);

            string backlogItemTypeUrl = "";
            if (Url != null)
            {
                backlogItemTypeUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemTypeUrl;
            }

            var dto = _Hydrator.Hydrate<BacklogItemTypeDto>(model);

            return Created(backlogItemTypeUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateBacklogItemType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, BacklogItemTypePatchDto backlogItemTypePatchDto)
        {
            if (id != backlogItemTypePatchDto.ID)
            {
                return BadRequest();
            }

            BacklogItemTypeDto dto;
            try
            {
                BacklogItemTypeModel model = _Hydrator.Hydrate<BacklogItemTypeModel>(backlogItemTypePatchDto);
                model = _BacklogItemTypeService.Update(model);
                dto = _Hydrator.Hydrate<BacklogItemTypeDto>(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemTypeModel)))
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

        [HttpDelete("{id}", Name = "DeleteBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            BacklogItemTypeModel? model = _BacklogItemTypeService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemTypeService.Delete(model);

            return new OkResult();
        }

        [HttpGet("{id}/ChildTypes", Name = "GetChildTypesForBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemTypeDto>), StatusCodes.Status200OK)]
        public IActionResult GetChildTypes(int id)
        {
            var backlogItemType = _BacklogItemTypeService.Get(id);
            if (backlogItemType == null)
            {
                return NotFound();
            }

            List<BacklogItemTypeModel> models = new();

            var childBacklogItemTypes = _ChildBacklogItemTypeService.GetByParentTypeId(id);
            childBacklogItemTypes.ForEach(childBacklogItemType =>
            {
                var childType = _BacklogItemTypeService.Get(childBacklogItemType.ChildTypeID);
                if (childType == null)
                {
                    throw new ModelNotFoundException(
                        nameof(BacklogItemTypeModel),
                        childBacklogItemType.ChildTypeID.ToString()
                    );
                }
                models.Add(childType);
            });

            var dtos = _Hydrator.HydrateList<BacklogItemTypeDto>(models);
            return Ok(dtos);
        }

        [HttpPut("{id}/ChildTypes/{childId}", Name = "PutChildTypeForBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult PutChildType(int id, int childId)
        {
            var parentType = _BacklogItemTypeService.Get(id);
            var childType = _BacklogItemTypeService.Get(childId);
            if (parentType == null || childType == null)
            {
                return NotFound();
            }

            if (parentType.BacklogItemTypeSchemaID != childType.BacklogItemTypeSchemaID)
            {
                var problem = new ProblemDetails();
                problem.Title = "Child belongs to a different schema";
                problem.Status = 400;
                return BadRequest(problem);
            }

            var created = false;
            var childBacklogItemType = _ChildBacklogItemTypeService.Get(id, childId);
            if (childBacklogItemType == null)
            {
                var schema = _BacklogItemTypeSchemaService.Get(
                    parentType.BacklogItemTypeSchemaID
                );
                if (schema == null)
                {
                    throw new ModelNotFoundException(
                        nameof(BacklogItemTypeSchemaModel),
                        parentType.BacklogItemTypeSchemaID.ToString()
                    );
                }

                childBacklogItemType = new ChildBacklogItemTypeModel(
                    childType.ID, parentType.ID, schema.ID);
                childBacklogItemType = _ChildBacklogItemTypeService.Create(childBacklogItemType);
                created = true;
            }

            var dto = _Hydrator.Hydrate<BacklogItemTypeDto>(childType);
            return created ? Created("", dto) : Ok(dto);
        }

        [HttpDelete("{id}/ChildTypes/{childId}", Name = "DeleteChildTypeForBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult DeleteChildType(int id, int childId)
        {
            var childBacklogItemType = _ChildBacklogItemTypeService.Get(id, childId);
            if (childBacklogItemType == null)
            {
                return NotFound();
            }

            _ChildBacklogItemTypeService.Delete(childBacklogItemType);

            return new NoContentResult();
        }
    }
}