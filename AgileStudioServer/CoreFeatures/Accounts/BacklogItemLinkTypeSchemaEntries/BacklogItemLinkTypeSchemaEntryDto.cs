using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryDto
    {
        public int ID { get; set; }

        public BacklogItemLinkTypeSchemaSummaryDto BacklogItemLinkTypeSchemaSummaryDto { get; set; }

        public BacklogItemLinkTypeSummaryDto BacklogItemLinkTypeSummaryDto { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemLinkTypeSchemaEntryDto(
            int id,
            BacklogItemLinkTypeSchemaSummaryDto backlogItemLinkTypeSchemaSummaryDto,
            BacklogItemLinkTypeSummaryDto backlogItemLinkTypeSummaryDto,
            DateTime createdOn)
        {
            ID = id;
            BacklogItemLinkTypeSchemaSummaryDto = backlogItemLinkTypeSchemaSummaryDto;
            BacklogItemLinkTypeSummaryDto = backlogItemLinkTypeSummaryDto;
            CreatedOn = createdOn;
        }
    }
}
