using AgileStudioServer.Features.Accounts.BacklogItemTypes;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeDto
    {
        public int ID { get; set; }

        public BacklogItemTypeSummaryDto BacklogItemType { get; set; }

        public BacklogItemTypeSchemaNodeDto(
            int id,
            BacklogItemTypeSummaryDto backlogItemTypeSummaryDto)
        {
            ID = id;
            BacklogItemType = backlogItemTypeSummaryDto;
        }
    }
}
