using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeDto
    {
        public int ID { get; set; }

        public BacklogItemTypeSchemaSummaryDto BacklogItemTypeSchema { get; set; }

        public BacklogItemTypeSummaryDto? FromType { get; set; }

        public BacklogItemTypeSummaryDto ToType { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemTypeSchemaEdgeDto(
            int id,
            BacklogItemTypeSchemaSummaryDto backlogItemTypeSchemaSummaryDto,
            BacklogItemTypeSummaryDto? fromTypeSummaryDto,
            BacklogItemTypeSummaryDto toTypeSummaryDto,
            DateTime createdOn)
        {
            ID = id;
            BacklogItemTypeSchema = backlogItemTypeSchemaSummaryDto;
            FromType = fromTypeSummaryDto;
            ToType = toTypeSummaryDto;
            CreatedOn = createdOn;
        }
    }
}
