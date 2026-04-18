using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas
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
