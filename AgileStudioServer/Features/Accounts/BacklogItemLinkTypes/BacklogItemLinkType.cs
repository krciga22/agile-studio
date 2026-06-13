using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkType
    {
        public int ID { get; set; }

        public string Title { get; set; } // eg. blocks

        public string TitleOpposite { get; set; } // eg. is blocked by

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public int AccountID { get; set; }

        public Account Account { get; set; } = null!;

        public BacklogItemLinkType(string title, string titleOpposite, int accountID)
        {
            Title = title;
            TitleOpposite = titleOpposite;
            AccountID = accountID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}