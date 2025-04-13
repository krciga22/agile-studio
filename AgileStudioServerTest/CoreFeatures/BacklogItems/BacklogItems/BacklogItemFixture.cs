
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Users.Users;
using AgileStudioServerTest.CoreFeatures.Workflows.WorkflowStates;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItems
{
    public class BacklogItemFixture : AbstractEntityFixture
    {
        private readonly UserFixture _userFixture;

        private readonly ProjectFixture _projectFixture;

        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;

        private readonly WorkflowStateFixture _workflowStateFixture;

        public BacklogItemFixture(
            DBContext dbContext, 
            UserFixture userFixture,
            ProjectFixture projectFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            WorkflowStateFixture workflowStateFixture) : base(dbContext)
        {
            _userFixture = userFixture;
            _projectFixture = projectFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
            _workflowStateFixture = workflowStateFixture;
        }

        public BacklogItem Create(
            string? title = null,
            User? createdBy = null,
            Project? project = null,
            BacklogItemType? backlogItemType = null,
            WorkflowState? workflowState = null,
            Sprint? sprint = null,
            Release? release = null,
            BacklogItem? parentBacklogItem = null)
        {
            title ??= "Test BacklogItem";
            project ??= _projectFixture.Create();
            backlogItemType ??= _backlogItemTypeFixture.Create(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            workflowState ??= _workflowStateFixture.Create();

            var backlogItem = new BacklogItem(
                title, project.ID, backlogItemType.ID, workflowState.ID);

            if (createdBy != null)
            {
                backlogItem.CreatedBy = createdBy;
            }

            if (sprint != null)
            {
                backlogItem.Sprint = sprint;
            }

            if (release != null)
            {
                backlogItem.Release = release;
            }

            if (parentBacklogItem != null)
            {
                backlogItem.ParentBacklogItem = parentBacklogItem;
            }

            _DBContext.BacklogItem.Add(backlogItem);
            _DBContext.SaveChanges();
            return backlogItem;
        }
    }
}
