
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaFixture : AbstractEntityFixture
    {
        private readonly UserFixture _userFixture;

        public BacklogItemLinkTypeSchemaFixture(DBContext dbContext, UserFixture userFixture) : base(dbContext)
        {
            _userFixture = userFixture;
        }

        public BacklogItemLinkTypeSchema Create(
            string? title = null,
            User? createdBy = null)
        {
            title ??= "Test BacklogItemLinkTypeSchema";
            createdBy ??= _userFixture.Create();

            var backlogItemLinkTypeSchema = new BacklogItemLinkTypeSchema(title)
            {
                CreatedBy = createdBy,
            };
            _DBContext.BacklogItemLinkTypeSchema.Add(backlogItemLinkTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemLinkTypeSchema;
        }
    }
}
