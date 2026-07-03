using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Auth.RoleGrants;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Projects.Projects
{
    public class ProjectFixture : AbstractEntityFixture<ProjectRepository>
    {
        private readonly AccountFixture _AccountFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _backlogItemLinkTypeSchemaFixture;

        private readonly UserFixture _userFixture;

        private readonly RoleGrantFixture _RoleGrantFixture;

        public ProjectFixture(
            ProjectRepository projectRepository,
            AccountFixture accountFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            UserFixture userFixture,
            RoleGrantFixture roleGrantFixture) : base(projectRepository)
        {
            _AccountFixture = accountFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _userFixture = userFixture;
            _RoleGrantFixture = roleGrantFixture;
        }

        public ProjectModel Create(
            AccountModel? account = null,
            string? title = null,
            BacklogItemTypeSchemaModel? backlogItemTypeSchema = null,
            BacklogItemLinkTypeSchemaModel? backlogItemLinkTypeSchema = null,
            UserModel? createdBy = null)
        {
            account ??= _AccountFixture.Create();
            title ??= "Test Project";
            backlogItemTypeSchema ??= _backlogItemTypeSchemaFixture.Create(
                account: account);
            backlogItemLinkTypeSchema ??= _backlogItemLinkTypeSchemaFixture.Create(
                account: account);
            createdBy ??= _userFixture.Create();

            var project = new ProjectModel(account.ID, title, backlogItemTypeSchema.ID, backlogItemLinkTypeSchema.ID)
            {
                CreatedByID = createdBy.ID
            };
            
            return _Repository.Create(project);
        }

        public ProjectModel? Get(int id)
        {
            return _Repository.Get(id);
        }

        public RoleGrantModel GrantAccess(int id, int userId, string roleKey)
        {
            return _RoleGrantFixture.Create(
                subjectType: RoleSubjectTypes.USER,
                subjectID: userId.ToString(),
                roleKey: roleKey,
                scope: Scopes.PROJECT,
                scopeID: id.ToString()
            );
        }
    }
}
