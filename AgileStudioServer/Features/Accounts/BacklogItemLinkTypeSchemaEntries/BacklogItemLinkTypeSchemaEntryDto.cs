using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries
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
