using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.ChildBacklogItemTypes
{
    public class ChildBacklogItemTypeServiceTest : AbstractServiceTest
    {
        private readonly ChildBacklogItemTypeService _childBacklogItemTypeService;

        private readonly ChildBacklogItemTypeFixture _ChildBacklogItemTypeFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        public ChildBacklogItemTypeServiceTest(
            DBContext dbContext,
            ChildBacklogItemTypeService childBacklogItemTypeService,
            ChildBacklogItemTypeFixture childBacklogItemTypeFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture) : base(dbContext)
        {
            _childBacklogItemTypeService = childBacklogItemTypeService;
            _ChildBacklogItemTypeFixture = childBacklogItemTypeFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
        }

        [Fact]
        public void Create_ReturnsChildBacklogItemType()
        {
            BacklogItemTypeModel backlogItemTypeStory = _BacklogItemTypeFixture.Create("Story");
            BacklogItemTypeModel backlogItemTypeTask = _BacklogItemTypeFixture.Create("Task");
            BacklogItemTypeSchemaModel schema = _BacklogItemTypeSchemaFixture.Create();
            ChildBacklogItemTypeModel childBacklogItemType = new(
                backlogItemTypeTask.ID, backlogItemTypeStory.ID, schema.ID);

            childBacklogItemType = _childBacklogItemTypeService.Create(childBacklogItemType);

            Assert.NotNull(childBacklogItemType);
            Assert.True(childBacklogItemType.ID > 0);
        }

        [Fact]
        public void Get_ReturnsChildBacklogItemType()
        {
            var childBacklogItemType = _ChildBacklogItemTypeFixture.Create();

            var returnedChildBacklogItemType = _childBacklogItemTypeService.Get(childBacklogItemType.ID);

            Assert.NotNull(returnedChildBacklogItemType);
            Assert.Equal(childBacklogItemType.ID, returnedChildBacklogItemType.ID);
        }

        [Fact]
        public void GetByParentTypeId_ReturnsChildBacklogItemTypes()
        {
            var parentBacklogItemType = _BacklogItemTypeFixture.Create("Parent Type");

            var childBacklogItemTypes = new List<ChildBacklogItemTypeModel>
            {
                _ChildBacklogItemTypeFixture.Create(
                    parentType: parentBacklogItemType
                ),
                _ChildBacklogItemTypeFixture.Create(
                    parentType: parentBacklogItemType
                )
            };

            List<ChildBacklogItemTypeModel> returnedChildBacklogItemTypes = _childBacklogItemTypeService
                .GetByParentTypeId(parentBacklogItemType.ID);

            Assert.Equal(childBacklogItemTypes.Count, returnedChildBacklogItemTypes.Count);
        }

        [Fact]
        public void GetByChildTypeId_ReturnsChildBacklogItemTypes()
        {
            var childBacklogItemType = _BacklogItemTypeFixture.Create("Child Type");

            var childBacklogItemTypes = new List<ChildBacklogItemTypeModel>
            {
                _ChildBacklogItemTypeFixture.Create(
                    childType: childBacklogItemType
                ),
                _ChildBacklogItemTypeFixture.Create(
                    childType: childBacklogItemType
                )
            };

            List<ChildBacklogItemTypeModel> returnedChildBacklogItemTypes = _childBacklogItemTypeService
                .GetByChildTypeId(childBacklogItemType.ID);

            Assert.Equal(childBacklogItemTypes.Count, returnedChildBacklogItemTypes.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedChildBacklogItemType()
        {
            var childBacklogItemType = _ChildBacklogItemTypeFixture.Create();
            var backlogItemType = _BacklogItemTypeFixture.Create("Updated Type");

            childBacklogItemType.ChildTypeID = backlogItemType.ID;
            childBacklogItemType = _childBacklogItemTypeService.Update(childBacklogItemType);

            Assert.NotNull(childBacklogItemType);
            Assert.Equal(backlogItemType.ID, childBacklogItemType.ChildTypeID);
        }

        [Fact]
        public void Delete_DeletesChildBacklogItemType()
        {
            var childBacklogItemType = _ChildBacklogItemTypeFixture.Create();

            _childBacklogItemTypeService.Delete(childBacklogItemType);

            childBacklogItemType = _childBacklogItemTypeService.Get(childBacklogItemType.ID);
            Assert.Null(childBacklogItemType);
        }
    }
}
