using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.WorkflowStates
{
    public class WorkflowStateDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowSummaryDto Workflow { get; set; }

        public string? Description { get; set; }

        public WorkflowStateDto(
            int id,
            string title,
            WorkflowSummaryDto workflow)
        {
            ID = id;
            Title = title;
            Workflow = workflow;
        }
    }
}
