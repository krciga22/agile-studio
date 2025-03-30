using AgileStudioServer.CoreFeatures.Users.APIs.DTOs;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemTypeSchemaDto(
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
