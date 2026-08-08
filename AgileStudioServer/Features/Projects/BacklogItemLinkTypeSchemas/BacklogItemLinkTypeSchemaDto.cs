using AgileStudioServer.Features.Accounts.Accounts;

namespace AgileStudioServer.Features.Projects.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public AccountSummaryDto Account { get; set; }

        public BacklogItemLinkTypeSchemaDto(
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
