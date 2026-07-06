using AgileStudioServer.Features.Accounts.BacklogItemTypes;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeForProjectDto
    {
        public int ID { get; set; }

        public BacklogItemTypeSummaryDto BacklogItemType { get; set; }

        public BacklogItemTypeSchemaNodeForProjectDto(
            int id,
            BacklogItemTypeSummaryDto backlogItemTypeSummaryDto)
        {
            ID = id;
            BacklogItemType = backlogItemTypeSummaryDto;
        }
    }
}
