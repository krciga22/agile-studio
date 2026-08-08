using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeForProjectDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string TitleOpposite { get; set; }

        public string? Description { get; set; }

        public AccountSummaryDto Account { get; set; }

        public BacklogItemLinkTypeForProjectDto(
            int id,
            string title,
            string titleOpposite,
            AccountSummaryDto account)
        {
            ID = id;
            Title = title;
            TitleOpposite = titleOpposite;
            Account = account;
        }
    }
}
