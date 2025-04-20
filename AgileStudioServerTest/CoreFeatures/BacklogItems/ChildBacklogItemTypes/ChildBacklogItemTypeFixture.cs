
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.ChildBacklogItemTypes
{
    public class ChildBacklogItemTypeFixture : AbstractEntityFixture<ChildBacklogItemTypeRepository>
    {
        private readonly UserFixture _userFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;

        public ChildBacklogItemTypeFixture(
            ChildBacklogItemTypeRepository childBacklogItemTypeRepository, 
            UserFixture userFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture) : base(childBacklogItemTypeRepository)
        {
            _userFixture = userFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
        }

        public ChildBacklogItemTypeModel Create(
            BacklogItemTypeModel? parentType = null,
            BacklogItemTypeModel? childType = null,
            BacklogItemTypeSchemaModel? schema = null,
            UserModel? createdBy = null)
        {
            schema ??= _backlogItemTypeSchemaFixture.Create();
            parentType ??= _backlogItemTypeFixture.Create("Story", backlogItemTypeSchema: schema);
            childType ??= _backlogItemTypeFixture.Create("Task", backlogItemTypeSchema: schema);
            createdBy ??= _userFixture.Create();

            var childBacklogItemType = new ChildBacklogItemTypeModel(childType.ID, parentType.ID, schema.ID)
            {
                CreatedByID = createdBy.ID
            };
            _Repository.Create(childBacklogItemType);
            return childBacklogItemType;
        }
    }
}
