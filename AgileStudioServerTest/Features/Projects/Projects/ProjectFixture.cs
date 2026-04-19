using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Projects.Projects
{
    public class ProjectFixture : AbstractEntityFixture<ProjectRepository>
    {
        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _backlogItemLinkTypeSchemaFixture;

        private readonly UserFixture _userFixture;

        public ProjectFixture(
            ProjectRepository projectRepository,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            UserFixture userFixture) : base(projectRepository)
        {
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _userFixture = userFixture;
        }

        public ProjectModel Create(
            string? title = null,
            BacklogItemTypeSchemaModel? backlogItemTypeSchema = null,
            BacklogItemLinkTypeSchemaModel? backlogItemLinkTypeSchema = null,
            UserModel? createdBy = null)
        {
            title ??= "Test Project";
            backlogItemTypeSchema ??= _backlogItemTypeSchemaFixture.Create();
            backlogItemLinkTypeSchema ??= _backlogItemLinkTypeSchemaFixture.Create();
            createdBy ??= _userFixture.Create();

            var project = new ProjectModel(title, backlogItemTypeSchema.ID, backlogItemLinkTypeSchema.ID)
            {
                CreatedByID = createdBy.ID
            };
            
            return _Repository.Create(project);
        }

        public ProjectModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
