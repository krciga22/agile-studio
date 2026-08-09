using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatus
    {
        public int ID { get; set; }

        public int BacklogItemID { get; set; }

        public BacklogItem BacklogItem { get; set; } = null!;

        public int WorkflowStateID { get; set; }

        public WorkflowState WorkflowState { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public string? Comment { get; set; }

        public BacklogItemStatus(int backlogItemID, int workflowStateID)
        {
            BacklogItemID = backlogItemID;
            WorkflowStateID = workflowStateID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}