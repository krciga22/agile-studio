
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaFixture : AbstractEntityFixture
    {
        private readonly UserFixture _userFixture;

        public BacklogItemTypeSchemaFixture(DBContext dbContext, UserFixture userFixture) : base(dbContext)
        {
            _userFixture = userFixture;
        }

        public BacklogItemTypeSchema Create(
            string? title = null,
            User? createdBy = null)
        {
            title ??= "Test BacklogItemTypeSchema";
            createdBy ??= _userFixture.Create();

            var backlogItemTypeSchema = new BacklogItemTypeSchema(title)
            {
                CreatedBy = createdBy
            };
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }
    }
}
