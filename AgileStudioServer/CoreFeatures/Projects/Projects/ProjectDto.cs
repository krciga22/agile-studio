using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    public class ProjectDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemTypeSchemaSummaryDto BacklogItemTypeSchema { get; set; }

        public ProjectDto(
            int id,
            string title,
            DateTime createdOn,
            BacklogItemTypeSchemaSummaryDto backlogItemTypeSchema)
        {
            ID = id;
            Title = title;
            CreatedOn = createdOn;
            BacklogItemTypeSchema = backlogItemTypeSchema;
        }
    }
}
