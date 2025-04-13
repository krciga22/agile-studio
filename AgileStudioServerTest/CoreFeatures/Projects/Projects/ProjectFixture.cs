
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Projects.Projects
{
    public class ProjectFixture : AbstractEntityFixture
    {
        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _backlogItemLinkTypeSchemaFixture;

        private readonly UserFixture _userFixture;

        public ProjectFixture(
            DBContext dbContext,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            UserFixture userFixture) : base(dbContext)
        {
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _userFixture = userFixture;
        }

        public Project Create(
            string? title = null,
            BacklogItemTypeSchema? backlogItemTypeSchema = null,
            BacklogItemLinkTypeSchema? backlogItemLinkTypeSchema = null,
            User? createdBy = null)
        {
            title ??= "Test Project";
            backlogItemTypeSchema ??= _backlogItemTypeSchemaFixture.Create();
            backlogItemLinkTypeSchema ??= _backlogItemLinkTypeSchemaFixture.Create();
            createdBy ??= _userFixture.Create();

            var project = new Project(title, backlogItemTypeSchema.ID, backlogItemLinkTypeSchema.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
        }
    }
}
