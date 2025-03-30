using AgileStudioServer.CoreFeatures.Users.APIs.DTOs;

namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs
{
    public class BacklogItemLinkTypeSchemaDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemLinkTypeSchemaDto(
            int id,
            string title,
            DateTime createdOn)
        {
            ID = id;
            Title = title;
            CreatedOn = createdOn;
        }
    }
}
