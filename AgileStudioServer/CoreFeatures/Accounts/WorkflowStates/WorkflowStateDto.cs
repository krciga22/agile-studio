using AgileStudioServer.CoreFeatures.Accounts.Workflows;
using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Accounts.WorkflowStates
{
    public class WorkflowStateDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowSummaryDto Workflow { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public WorkflowStateDto(
            int id,
            string title,
            WorkflowSummaryDto workflow,
            DateTime createdOn)
        {
            ID = id;
            Title = title;
            CreatedOn = createdOn;
            Workflow = workflow;
        }
    }
}
