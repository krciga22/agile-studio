using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public AccountSummaryDto Account { get; set; }

        public WorkflowSummaryDto Workflow { get; set; }

        public BacklogItemTypeDto(
            int id,
            string title,
            DateTime createdOn,
            AccountSummaryDto account,
            WorkflowSummaryDto workflow)
        {
            ID = id;
            Title = title;
            CreatedOn = createdOn;
            Account = account;
            Workflow = workflow;
        }
    }
}
