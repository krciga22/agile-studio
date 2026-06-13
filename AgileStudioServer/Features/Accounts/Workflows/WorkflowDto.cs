using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public AccountSummaryDto Account { get; set; }

        public WorkflowDto(
            int id,
            string title,
            DateTime createdOn,
            AccountSummaryDto account)
        {
            ID = id;
            Title = title;
            CreatedOn = createdOn;
            Account = account;
        }
    }
}
