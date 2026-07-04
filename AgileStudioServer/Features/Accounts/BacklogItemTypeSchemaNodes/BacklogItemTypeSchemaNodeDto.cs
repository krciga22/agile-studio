using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeDto
    {
        public int ID { get; set; }

        public BacklogItemTypeSchemaSummaryDto BacklogItemTypeSchema { get; set; }

        public BacklogItemTypeSummaryDto BacklogItemType { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemTypeSchemaNodeDto(
            int id,
            DateTime createdOn,
            BacklogItemTypeSchemaSummaryDto backlogItemTypeSchemaSummaryDto,
            BacklogItemTypeSummaryDto backlogItemTypeSummaryDto)
        {
            ID = id;
            BacklogItemTypeSchema = backlogItemTypeSchemaSummaryDto;
            BacklogItemType = backlogItemTypeSummaryDto;
            CreatedOn = createdOn;
        }
    }
}
