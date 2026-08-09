using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.BacklogItemLinks
{
    public class BacklogItemLink
    {
        public int ID { get; set; }

        public int SourceBacklogItemID { get; set; }

        public BacklogItem SourceBacklogItem { get; set; } = null!; // eg. source blocks the target

        public int TargetBacklogItemID { get; set; }

        public BacklogItem TargetBacklogItem { get; set; } = null!; // eg. target is blocked by the source

        public int BacklogItemLinkTypeID { get; set; }

        public BacklogItemLinkType BacklogItemLinkType { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public BacklogItemLink(int sourceBacklogItemID, int targetBacklogItemID, int backlogItemLinkTypeID)
        {
            SourceBacklogItemID = sourceBacklogItemID;
            TargetBacklogItemID = targetBacklogItemID;
            BacklogItemLinkTypeID = backlogItemLinkTypeID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}