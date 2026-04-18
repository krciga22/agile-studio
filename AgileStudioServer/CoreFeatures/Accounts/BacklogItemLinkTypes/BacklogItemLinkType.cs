using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypes
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

        public BacklogItemLinkType(string title, string titleOpposite)
        {
            Title = title;
            TitleOpposite = titleOpposite;
            CreatedOn = DateTime.Now;
        }
    }
}