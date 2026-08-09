using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusDto
    {
        public int ID { get; set; }

        public BacklogItemSummaryDto BacklogItem { get; set; }

        public WorkflowStateSummaryDto WorkflowState { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public string? Comment { get; set; }

        public BacklogItemStatusDto(int id, BacklogItemSummaryDto backlogItem, WorkflowStateSummaryDto workflowState, DateTime createdOn)
        {
            ID = id;
            BacklogItem = backlogItem;
            WorkflowState = workflowState;
            CreatedOn = createdOn;
        }
    }
}