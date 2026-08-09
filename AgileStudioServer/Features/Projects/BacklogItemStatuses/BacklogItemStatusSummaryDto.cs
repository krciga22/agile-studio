using AgileStudioServer.Features.Accounts.WorkflowStates;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusSummaryDto
    {
        public int ID { get; set; }

        public WorkflowStateSummaryDto WorkflowState { get; set; }

        public string? Comment { get; set; }

        public BacklogItemStatusSummaryDto(int id, WorkflowStateSummaryDto workflowState)
        {
            ID = id;
            WorkflowState = workflowState;
        }
    }
}