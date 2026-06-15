using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.ChildBacklogItemTypes;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
{
    [ApiController]
    [Route("Accounts/BacklogItemTypes")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsBacklogItemType)]
    [MapResourcePatch(ResourceTypes.AccountsBacklogItemType)]
    [MapResourceDelete(ResourceTypes.AccountsBacklogItemType)]
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

        [HttpGet("{id}/ChildTypes", Name = "GetChildTypesForBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemTypeDto>), StatusCodes.Status200OK)]
        public IActionResult GetChildTypes(int id)
        {
            try
            {
                try
                {
                    var backlogItemType = _BacklogItemTypeService.Get(id);
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
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPut("{id}/ChildTypes/{childId}", Name = "PutChildTypeForBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult PutChildType(int id, int childId)
        {
            try
            {
                var parentType = _BacklogItemTypeService.Get(id);
                var childType = _BacklogItemTypeService.Get(childId);

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
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemTypeModel)))
                {
                    return NotFound();
                }
                else
                {
                    return Problem();
                }
            }
            catch (Exception)
            {
                return Problem();
            }
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