using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.CoreFeatures.Auth.RoleGrants;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypes;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Accounts.ChildBacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.Auth.RoleGrants;
using AgileStudioServerTest.CoreFeatures.Auth.Roles;
using AgileStudioServerTest.CoreFeatures.Projects.BacklogItems;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Projects.Releases;
using AgileStudioServerTest.CoreFeatures.Projects.Sprints;
using AgileStudioServerTest.CoreFeatures.Users.Users;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;
using AgileStudioServerTest.CoreFeatures.Workflows.WorkflowStates;

namespace AgileStudioCLI.FixtureSets
{
    public class BaseFixtureSet : IFixtureSet
    {
        private readonly BacklogItemLinkTypeFixture _BacklogItemLinkTypeFixture;
        private readonly BacklogItemLinkTypeSchemaEntryFixture _BacklogItemLinkTypeSchemaEntryFixture;
        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;
        private readonly BacklogItemFixture _BacklogItemFixture;
        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;
        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;
        private readonly ChildBacklogItemTypeFixture _ChildBacklogItemTypeFixture;
        private readonly ProjectFixture _ProjectFixture;
        private readonly ReleaseFixture _ReleaseFixture;
        private readonly RoleFixture _RoleFixture;
        private readonly RoleGrantFixture _RoleGrantFixture;
        private readonly SprintFixture _SprintFixture;
        private readonly UserFixture _UserFixture;
        private readonly WorkflowFixture _WorkflowFixture;
        private readonly WorkflowStateFixture _WorkflowStateFixture;

        public BaseFixtureSet(
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture, 
            BacklogItemLinkTypeSchemaEntryFixture backlogItemLinkTypeSchemaEntryFixture, 
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture, 
            BacklogItemFixture backlogItemFixture, 
            BacklogItemTypeFixture backlogItemTypeFixture, 
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture, 
            ChildBacklogItemTypeFixture childBacklogItemTypeFixture, 
            ProjectFixture projectFixture, 
            ReleaseFixture releaseFixture, 
            RoleFixture roleFixture,
            RoleGrantFixture roleGrantFixture,
            SprintFixture sprintFixture, 
            UserFixture userFixture, 
            WorkflowFixture workflowFixture, 
            WorkflowStateFixture workflowStateFixture)
        {
            _BacklogItemLinkTypeFixture = backlogItemLinkTypeFixture;
            _BacklogItemLinkTypeSchemaEntryFixture = backlogItemLinkTypeSchemaEntryFixture;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _BacklogItemFixture = backlogItemFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _ChildBacklogItemTypeFixture = childBacklogItemTypeFixture;
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

            var workflow = _WorkflowFixture.Create(
                title: "Story & Defect Workflow",
                createdBy: user);

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
                createdBy: user);

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
                createdBy: user);

            var backlogItemTypeStory = _BacklogItemTypeFixture.Create(
                title: "Story",
                createdBy: user,
                backlogItemTypeSchema: backlogItemTypeSchema,
                workflow: workflow);

            var backlogItemTypeDefect = _BacklogItemTypeFixture.Create(
                title: "Defect",
                createdBy: user,
                backlogItemTypeSchema: backlogItemTypeSchema,
                workflow: workflow);

            var backlogItemTypeTask = _BacklogItemTypeFixture.Create(
                title: "Task",
                createdBy: user,
                backlogItemTypeSchema: backlogItemTypeSchema,
                workflow: workflow);

            var backlogItemTypeTest = _BacklogItemTypeFixture.Create(
                title: "Test",
                createdBy: user,
                backlogItemTypeSchema: backlogItemTypeSchema,
                workflow: workflow);

            _ChildBacklogItemTypeFixture.Create(
                parentType: backlogItemTypeStory,
                childType: backlogItemTypeTask,
                schema: backlogItemTypeSchema
            );

            _ChildBacklogItemTypeFixture.Create(
                parentType: backlogItemTypeStory,
                childType: backlogItemTypeTest,
                schema: backlogItemTypeSchema
            );

            _ChildBacklogItemTypeFixture.Create(
                parentType: backlogItemTypeDefect,
                childType: backlogItemTypeTask,
                schema: backlogItemTypeSchema
            );

            _ChildBacklogItemTypeFixture.Create(
                parentType: backlogItemTypeDefect,
                childType: backlogItemTypeTest,
                schema: backlogItemTypeSchema
            );

            var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create("Agile Studio Backlog Item Link Type Schema");

            var blocksLinkType = _BacklogItemLinkTypeFixture.Create("blocks", "is blocked by");
            var relatesToLinkType = _BacklogItemLinkTypeFixture.Create("relates to", "relates to");
            var splitFromLinkType = _BacklogItemLinkTypeFixture.Create("split from", "split to");
            var clonedFromLinkType = _BacklogItemLinkTypeFixture.Create("cloned from", "cloned to");
            var duplicatesLinkType = _BacklogItemLinkTypeFixture.Create("duplicates", "is duplicated by");
            var causesLinkType = _BacklogItemLinkTypeFixture.Create("causes", "is caused by");

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: blocksLinkType);

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: relatesToLinkType);

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: splitFromLinkType);

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: clonedFromLinkType);

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: duplicatesLinkType);

            _BacklogItemLinkTypeSchemaEntryFixture.Create(
                backlogItemLinkTypeSchema: backlogItemLinkTypeSchema,
                backlogItemLinkType: causesLinkType);

            var project = _ProjectFixture.Create(
                title: "Agile Studio", 
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
                scope: PermissionScopes.PROJECTS,
                scopeID: project.ID.ToString(),
                createdBy: user);
        }
    }
}
