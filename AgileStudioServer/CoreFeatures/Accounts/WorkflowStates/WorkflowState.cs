using AgileStudioServer.CoreFeatures.Accounts.Workflows;
using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Accounts.WorkflowStates
{
    public class WorkflowState
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int WorkflowID { get; set; }

        public Workflow Workflow { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public WorkflowState(string title, int workflowID)
        {
            Title = title;
            CreatedOn = DateTime.UtcNow;
            WorkflowID = workflowID;
        }
    }
}