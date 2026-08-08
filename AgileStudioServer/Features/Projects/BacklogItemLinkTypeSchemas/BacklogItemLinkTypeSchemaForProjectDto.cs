using AgileStudioServer.Features.Accounts.Accounts;

namespace AgileStudioServer.Features.Projects.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaForProjectDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public AccountSummaryDto Account { get; set; }

        public BacklogItemLinkTypeSchemaForProjectDto(
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
