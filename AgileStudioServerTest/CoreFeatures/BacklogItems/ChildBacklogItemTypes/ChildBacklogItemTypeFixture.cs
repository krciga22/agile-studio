
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.ChildBacklogItemTypes
{
    public class ChildBacklogItemTypeFixture : AbstractEntityFixture
    {
        private readonly UserFixture _userFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;

        public ChildBacklogItemTypeFixture(
            DBContext dbContext, 
            UserFixture userFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture) : base(dbContext)
        {
            _userFixture = userFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
        }

        public ChildBacklogItemType Create(
            BacklogItemType? parentType = null,
            BacklogItemType? childType = null,
            BacklogItemTypeSchema? schema = null,
            User? createdBy = null)
        {
            schema ??= _backlogItemTypeSchemaFixture.Create();
            parentType ??= _backlogItemTypeFixture.Create("Story", backlogItemTypeSchema: schema);
            childType ??= _backlogItemTypeFixture.Create("Task", backlogItemTypeSchema: schema);
            createdBy ??= _userFixture.Create();

            var childBacklogItemType = new ChildBacklogItemType(childType.ID, parentType.ID, schema.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.ChildBacklogItemType.Add(childBacklogItemType);
            _DBContext.SaveChanges();
            return childBacklogItemType;
        }
    }
}
