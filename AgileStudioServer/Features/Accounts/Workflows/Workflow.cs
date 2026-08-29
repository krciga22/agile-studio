using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class Workflow
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public int AccountID { get; set; }

        public Account Account { get; set; } = null!;

        public int? DefaultWorkflowStateID { get; set; } = null!;

        public WorkflowState? DefaultWorkflowState { get; set; } = null!;

        public Workflow(string title, int accountID)
        {
            Title = title;
            CreatedOn = DateTime.UtcNow;
            AccountID = accountID;
        }
    }
}