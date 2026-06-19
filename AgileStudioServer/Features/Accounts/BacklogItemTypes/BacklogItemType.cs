using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemType
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public int AccountID { get; set; }

        public Account Account { get; set; } = null!;

        public int WorkflowID { get; set; }

        public Workflow Workflow { get; set; } = null!;

        public BacklogItemType(string title, int accountID, int workflowID)
        {
            Title = title;
            CreatedOn = DateTime.UtcNow;
            AccountID = accountID;
            WorkflowID = workflowID;
        }
    }
}