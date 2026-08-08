using AgileStudioServer.Features.Accounts.BacklogItemTypes;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeDto
    {
        public int ID { get; set; }

        public BacklogItemTypeSummaryDto? FromType { get; set; }

        public BacklogItemTypeSummaryDto ToType { get; set; }

        public BacklogItemTypeSchemaEdgeDto(
            int id,
            BacklogItemTypeSummaryDto? fromTypeSummaryDto,
            BacklogItemTypeSummaryDto toTypeSummaryDto)
        {
            ID = id;
            FromType = fromTypeSummaryDto;
            ToType = toTypeSummaryDto;
        }
    }
}
