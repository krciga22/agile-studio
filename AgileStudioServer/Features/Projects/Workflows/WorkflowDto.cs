using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.Workflows
{
    public class WorkflowDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public AccountSummaryDto Account { get; set; }

        public WorkflowDto(
            int id,
            string title,
            AccountSummaryDto account)
        {
            ID = id;
            Title = title;
            Account = account;
        }
    }
}
