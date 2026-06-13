using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public AccountSummaryDto Account { get; set; }

        public BacklogItemLinkTypeSchemaDto(
            int id,
            string title,
            DateTime createdOn,
            AccountSummaryDto accountSummaryDto)
        {
            ID = id;
            Title = title;
            CreatedOn = createdOn;
            Account = accountSummaryDto;
        }
    }
}
