using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntrySummaryDto
    {
        public int ID { get; set; }

        public BacklogItemLinkTypeSchemaSummaryDto BacklogItemLinkTypeSchemaSummaryDto { get; set; }

        public BacklogItemLinkTypeSummaryDto BacklogItemLinkTypeSummaryDto { get; set; }

        public BacklogItemLinkTypeSchemaEntrySummaryDto(
            int id,
            BacklogItemLinkTypeSchemaSummaryDto backlogItemLinkTypeSchemaSummaryDto,
            BacklogItemLinkTypeSummaryDto backlogItemLinkTypeSummaryDto)
        {
            ID = id;
            BacklogItemLinkTypeSchemaSummaryDto = backlogItemLinkTypeSchemaSummaryDto;
            BacklogItemLinkTypeSummaryDto = backlogItemLinkTypeSummaryDto;
        }
    }
}
