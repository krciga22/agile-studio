using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string TitleOpposite { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public AccountSummaryDto Account { get; set; }

        public BacklogItemLinkTypeDto(
            int id,
            string title,
            string titleOpposite,
            DateTime createdOn,
            AccountSummaryDto account)
        {
            ID = id;
            Title = title;
            TitleOpposite = titleOpposite;
            CreatedOn = createdOn;
            Account = account;
        }
    }
}
