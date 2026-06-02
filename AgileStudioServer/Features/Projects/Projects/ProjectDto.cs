using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectDto
    {
        public int ID { get; set; }

        public AccountSummaryDto Account { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemTypeSchemaSummaryDto BacklogItemTypeSchema { get; set; }

        public BacklogItemLinkTypeSchemaSummaryDto BacklogItemLinkTypeSchema { get; set; }

        public ProjectDto(
            int id,
            AccountSummaryDto account,
            string title,
            DateTime createdOn,
            BacklogItemTypeSchemaSummaryDto backlogItemTypeSchema,
            BacklogItemLinkTypeSchemaSummaryDto backlogItemLinkTypeSchema)
        {
            ID = id;
            Account = account;
            Title = title;
            CreatedOn = createdOn;
            BacklogItemTypeSchema = backlogItemTypeSchema;
            BacklogItemLinkTypeSchema = backlogItemLinkTypeSchema;
        }
    }
}
