using AgileStudioServer.CoreFeatures.Users.APIs.DTOs;

namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs
{
    public class BacklogItemLinkTypeDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string TitleOpposite { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemLinkTypeDto(
            int id,
            string title,
            string titleOpposite,
            DateTime createdOn)
        {
            ID = id;
            Title = title;
            TitleOpposite = titleOpposite;
            CreatedOn = createdOn;
        }
    }
}
