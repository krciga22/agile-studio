using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.AccountTypes;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.AccountTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaEntries;
using AgileStudioServerTest.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.WorkflowStates;
using AgileStudioServerTest.Features.Auth.RoleGrants;
using AgileStudioServerTest.Features.Auth.Roles;
using AgileStudioServerTest.Features.Projects.BacklogItems;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Projects.Releases;
using AgileStudioServerTest.Features.Projects.Sprints;
using AgileStudioServerTest.Features.Users.Users;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaNodes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaEdges;

namespace AgileStudioCLI.FixtureSets
{
    public class BaseFixtureSet : IFixtureSet
    {
        private readonly AccountFixture _AccountFixture;
        private readonly AccountTypeFixture _AccountTypeFixture;
        private readonly BacklogItemLinkTypeFixture _BacklogItemLinkTypeFixture;
        private readonly BacklogItemLinkTypeSchemaEntryFixture _BacklogItemLinkTypeSchemaEntryFixture;
        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;
        private readonly BacklogItemFixture _BacklogItemFixture;
        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;
        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;
        private readonly BacklogItemTypeSchemaEntryFixture _BacklogItemTypeSchemaEntryFixture;
        private readonly BacklogItemTypeSchemaNodeFixture _BacklogItemTypeSchemaNodeFixture;
        private readonly BacklogItemTypeSchemaEdgeFixture _BacklogItemTypeSchemaEdgeFixture;
        private readonly ProjectFixture _ProjectFixture;
        private readonly ReleaseFixture _ReleaseFixture;
        private readonly RoleFixture _RoleFixture;
        private readonly RoleGrantFixture _RoleGrantFixture;
        private readonly SprintFixture _SprintFixture;
        private readonly UserFixture _UserFixture;
        private readonly WorkflowFixture _WorkflowFixture;
        private readonly WorkflowStateFixture _WorkflowStateFixture;

        public BaseFixtureSet(
            AccountFixture accountFixture,
            AccountTypeFixture accountTypeFixture,
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture,
            BacklogItemLinkTypeSchemaEntryFixture backlogItemLinkTypeSchemaEntryFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            BacklogItemFixture backlogItemFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeSchemaEntryFixture backlogItemTypeSchemaEntryFixture,
            BacklogItemTypeSchemaNodeFixture backlogItemTypeSchemaNodeFixture,
            BacklogItemTypeSchemaEdgeFixture backlogItemTypeSchemaEdgeFixture,
            ProjectFixture projectFixture,
            ReleaseFixture releaseFixture,
            RoleFixture roleFixture,
            RoleGrantFixture roleGrantFixture,
            SprintFixture sprintFixture,
            UserFixture userFixture,
            WorkflowFixture workflowFixture,
            WorkflowStateFixture workflowStateFixture)
        {
            _AccountFixture = accountFixture;
            _AccountTypeFixture = accountTypeFixture;
            _BacklogItemLinkTypeFixture = backlogItemLinkTypeFixture;
            _BacklogItemLinkTypeSchemaEntryFixture = backlogItemLinkTypeSchemaEntryFixture;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _BacklogItemFixture = backlogItemFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _BacklogItemTypeSchemaEntryFixture = backlogItemTypeSchemaEntryFixture;
            _BacklogItemTypeSchemaNodeFixture = backlogItemTypeSchemaNodeFixture;
            _BacklogItemTypeSchemaEdgeFixture = backlogItemTypeSchemaEdgeFixture;
            _ProjectFixture = projectFixture;
            _ReleaseFixture = releaseFixture;
            _RoleFixture = roleFixture;
            _RoleGrantFixture = roleGrantFixture;
            _SprintFixture = sprintFixture;
            _UserFixture = userFixture;
            _WorkflowFixture = workflowFixture;
            _WorkflowStateFixture = workflowStateFixture;
        }

        public void LoadFixtures(DBContext dbContext)
        {
            LoadAgileStudioProject();
        }

        private void LoadAgileStudioProject()
        {
            var user = _UserFixture.Create();

            var accountType = _AccountTypeFixture.Get(AccountTypes.INDIVIDUAL);

            var account = _AccountFixture.Create(accountType, createdBy: user);
            _AccountFixture.GrantAccess(account.ID, user.ID, RoleKeys.ACCOUNTS_ACCOUNT_OWNER);

            var workflow = _WorkflowFixture.Create(
                title: "Story & Defect Workflow",
                createdBy: user,
                account: account);

            var workflowStateInBacklog = _WorkflowStateFixture.Create(
                title: "In Backlog", 
                workflow: workflow,
                createdBy: user);
            _WorkflowStateFixture.Create(
                title: "In Planning",
                workflow: workflow,
                createdBy: user);
            _WorkflowStateFixture.Create(
                title: "In Development",
                workflow: workflow,
                createdBy: user);
            _WorkflowStateFixture.Create(
                title: "In Testing",
                workflow: workflow,
                createdBy: user);
            _WorkflowStateFixture.Create(
                title: "In Release",
                workflow: workflow,
                createdBy: user);
            _WorkflowStateFixture.Create(
                title: "Cancelled",
                workflow: workflow,
                createdBy: user);

            var taskWorkflow = _WorkflowFixture.Create(
                title: "Task Workflow",
                createdBy: user,
                account: account);

            var taskWorkflowStateNotStarted = _WorkflowStateFixture.Create(
                title: "Not Started",
                workflow: taskWorkflow,
                createdBy: user);
            _WorkflowStateFixture.Create(
                title: "In Progress",
                workflow: taskWorkflow,
                createdBy: user);
            _WorkflowStateFixture.Create(
                title: "Complete",
                workflow: taskWorkflow,
                createdBy: user);

            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create(
                title: "Agile Studio Backlog Item Type Schema",
                createdBy: user,
                account: account);

            var backlogItemTypeStory = _BacklogItemTypeFixture.Create(
                title: "Story",
                createdBy: user,
                account: account,
                workflow: workflow);

            var backlogItemTypeDefect = _BacklogItemTypeFixture.Create(
                title: "Defect",
                createdBy: user,
                account: account,
                workflow: workflow);

            var backlogItemTypeTask = _BacklogItemTypeFixture.Create(
                title: "Task",
                createdBy: user,
                account: account,
                workflow: workflow);

            var backlogItemTypeTest = _BacklogItemTypeFixture.Create(
                title: "Test",
                createdBy: user,
                account: account,
                workflow: workflow);

            _BacklogItemTypeSchemaEntryFixture.Create(
                parentType: backlogItemTypeStory,
                childType: backlogItemTypeTask,
                schema: backlogItemTypeSchema,
                createdBy: user
            );

            _BacklogItemTypeSchemaEntryFixture.Create(
                parentType: backlogItemTypeStory,
                childType: backlogItemTypeTest,
                schema: backlogItemTypeSchema,
                createdBy: user
            );

            _BacklogItemTypeSchemaEntryFixture.Create(
                parentType: backlogItemTypeDefect,
                childType: backlogItemTypeTask,
                schema: backlogItemTypeSchema,
                createdBy: user
            );

            _BacklogItemTypeSchemaEntryFixture.Create(
                parentType: backlogItemTypeDefect,
                childType: backlogItemTypeTest,
                schema: backlogItemTypeSchema,
                createdBy: user
            );

            _BacklogItemTypeSchemaNodeFixture.Create(
                backlogItemType: backlogItemTypeStory,
                schema: backlogItemTypeSchema,
                createdBy: user
            );

            _BacklogItemTypeSchemaNodeFixture.Create(
                backlogItemType: backlogItemTypeDefect,
                schema: backlogItemTypeSchema,
                createdBy: user
            );

            _BacklogItemTypeSchemaNodeFixture.Create(
                backlogItemType: backlogItemTypeTask,
                schema: backlogItemTypeSchema,
                createdBy: user,
                fromEdges: [backlogItemTypeStory, backlogItemTypeDefect]
            );

            _BacklogItemTypeSchemaNodeFixture.Create(
                backlogItemType: backlogItemTypeTest,
                schema: backlogItemTypeSchema,
                createdBy: user,
                fromEdges: [backlogItemTypeStory, backlogItemTypeDefect]
            );

            var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create(
                "Agile Studio Backlog Item Link Type Schema",
                createdBy: user,
                account: account
            );

            var blocksLinkType = _BacklogItemLinkTypeFixture.Create(
                "blocks", "is blocked by", createdBy: user, account: account);

            var relatesToLinkType = _BacklogItemLinkTypeFixture.Create(
                "relates to", "relates to", createdBy: user, account: account);

            var splitFromLinkType = _BacklogItemLinkTypeFixture.Create(
                "split from", "split to", createdBy: user, account: account);

            var clonedFromLinkType = _BacklogItemLinkTypeFixture.Create(
                "cloned from", "cloned to", createdBy: user, account: account);

            var duplicatesLinkType = _BacklogItemLinkTypeFixture.Create(
                "duplicates", "is duplicated by", createdBy: user, account: account);

            var causesLinkType = _BacklogItemLinkTypeFixture.Create(
                "causes", "is caused by", createdBy: user, account: account);

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: blocksLinkType,
                createdBy: user
            );

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: relatesToLinkType,
                createdBy: user
            );

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: splitFromLinkType,
                createdBy: user
            );

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: clonedFromLinkType,
                createdBy: user
            );

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: duplicatesLinkType,
                createdBy: user
            );

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: causesLinkType,
                createdBy: user
            );

            var project = _ProjectFixture.Create(
                title: "Agile Studio", 
                account: account,
                backlogItemTypeSchema: backlogItemTypeSchema,
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                createdBy: user);

            var projectAdminRole = _RoleFixture.Get(RoleKeys.PROJECTS_PROJECT_ADMIN);
            if(projectAdminRole == null){
                throw new Exception($"Project admin role not found.");
            }
            AssignUserToProject(user, project, projectAdminRole);

            var sprint1 = _SprintFixture.Create(
                sprintNumber: 1, 
                project: project,
                createdBy: user);

            var release1_0_0 = _ReleaseFixture.Create(
                title: "1.0.0",
                project: project,
                createdBy: user);

            var testStory = _BacklogItemFixture.Create(
                title: "Test Story", 
                project: project,
                backlogItemType: backlogItemTypeStory,
                workflowState: workflowStateInBacklog, 
                sprint: sprint1, 
                release: release1_0_0,
                createdBy: user);

            for( var i = 0; i < 5; i++ )
            {
                _BacklogItemFixture.Create(
                    title: $"Child Task {i}",
                    project: project,
                    backlogItemType: backlogItemTypeTask,
                    workflowState: taskWorkflowStateNotStarted,
                    parentBacklogItem: testStory,
                    createdBy: user);
            }
        }

        private void AssignUserToProject(UserModel user, ProjectModel project, RoleModel role)
        {
            _RoleGrantFixture.Create(
                roleKey: role.RoleKey,
                subjectType: RoleSubjectTypes.USER,
                subjectID: user.ID.ToString(),
                scope: Scopes.PROJECT,
                scopeID: project.ID.ToString(),
                createdBy: user);
        }
    }
}
