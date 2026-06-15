using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.ChildBacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.ChildBacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.Accounts;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeControllerTest : AbstractControllerTest
    {
        private const int NON_EXISTANT_ID = 1234567;

        private readonly WorkflowFixture _WorkflowFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly ChildBacklogItemTypeFixture _ChildBacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemTypeController _Controller;

        private readonly AccountFixture _AccountFixture;

        public BacklogItemTypeControllerTest(
            DBContext dbContext,
            BacklogItemTypeController controller,
            AccountFixture accountFixture,
            WorkflowFixture workflowFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            ChildBacklogItemTypeFixture childBacklogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture) : base(dbContext)
        {
            _Controller = controller;
            _AccountFixture = accountFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _ChildBacklogItemTypeFixture = childBacklogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _WorkflowFixture = workflowFixture;
        }

        [Fact]
        public void GetChildTypes_WithExistingId_ReturnsDtos()
        {
            var parentType = _BacklogItemTypeFixture.Create();
            var childType1 = _BacklogItemTypeFixture.Create();
            var childType2 = _BacklogItemTypeFixture.Create();

            var childBacklogItemTypes = new List<ChildBacklogItemTypeModel>() {
                _ChildBacklogItemTypeFixture.Create(
                    parentType: parentType,
                    childType: childType1
                ),
                _ChildBacklogItemTypeFixture.Create(
                    parentType: parentType,
                    childType: childType2
                )
            };

            List<BacklogItemTypeDto>? dtos = null;
            IActionResult result = _Controller.GetChildTypes(parentType.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<BacklogItemTypeDto>;
            }

            Assert.IsType<List<BacklogItemTypeDto>>(dtos);
            Assert.Equal(childBacklogItemTypes.Count, dtos.Count);
        }

        [Fact]
        public void GetChildTypes_WithNonExistingId_ReturnsNotFound()
        {
            IActionResult result = _Controller.GetChildTypes(NON_EXISTANT_ID);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PutChildType_WithNewChildType_ReturnsDto()
        {
            var parentType = _BacklogItemTypeFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                parentType.BacklogItemTypeSchemaID);
            var childType = _BacklogItemTypeFixture.Create(
                backlogItemTypeSchema: backlogItemTypeSchema
            );

            BacklogItemTypeDto? dto = null;
            IActionResult result = _Controller.PutChildType(
                parentType.ID,
                childType.ID
            );
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(dto);
            Assert.Equal(childType.ID, dto.ID);
        }

        [Fact]
        public void PutChildType_WithExistingChildType_ReturnsDto()
        {
            var parentType = _BacklogItemTypeFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                parentType.BacklogItemTypeSchemaID);
            var childType = _BacklogItemTypeFixture.Create(
                backlogItemTypeSchema: backlogItemTypeSchema
            );

            _ChildBacklogItemTypeFixture.Create(
                parentType: parentType,
                childType: childType
            );

            BacklogItemTypeDto? dto = null;
            IActionResult result = _Controller.PutChildType(
                parentType.ID,
                childType.ID
            );
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(dto);
            Assert.Equal(childType.ID, dto.ID);
        }

        [Fact]
        public void PutChildType_FromDifferentSchema_ReturnsBadRequest()
        {
            var parentType = _BacklogItemTypeFixture.Create();
            var backlogItemType = _BacklogItemTypeFixture.Create();

            IActionResult result = _Controller.PutChildType(
                parentType.ID,
                backlogItemType.ID
            );

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PutChildType_WithNonExistantParent_ReturnsNotFound()
        {
            var nonExistantBacklogItemTypeId = NON_EXISTANT_ID;
            var childType = _BacklogItemTypeFixture.Create();

            IActionResult result = _Controller.PutChildType(
                nonExistantBacklogItemTypeId,
                childType.ID
            );

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PutChildType_WithNonExistantChild_ReturnsNotFound()
        {
            var parentType = _BacklogItemTypeFixture.Create();
            var nonExistantBacklogItemTypeId = NON_EXISTANT_ID;

            IActionResult result = _Controller.PutChildType(
                parentType.ID,
                nonExistantBacklogItemTypeId
            );

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteChildType_WithExistingChildType_ReturnsOk()
        {
            var parentType = _BacklogItemTypeFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                parentType.BacklogItemTypeSchemaID);
            var childType = _BacklogItemTypeFixture.Create(
                backlogItemTypeSchema: backlogItemTypeSchema
            );

            _ChildBacklogItemTypeFixture.Create(
                parentType: parentType,
                childType: childType
            );

            IActionResult result = _Controller.DeleteChildType(
                parentType.ID,
                childType.ID
            );

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteChildType_WithNonExistingChildType_ReturnsNotFound()
        {
            var parentType = _BacklogItemTypeFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                parentType.BacklogItemTypeSchemaID);
            var childType = _BacklogItemTypeFixture.Create(
                backlogItemTypeSchema: backlogItemTypeSchema
            );

            IActionResult result = _Controller.DeleteChildType(
                parentType.ID,
                childType.ID
            );

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
