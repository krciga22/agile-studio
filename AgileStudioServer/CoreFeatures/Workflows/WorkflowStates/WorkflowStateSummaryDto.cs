namespace AgileStudioServer.CoreFeatures.Workflows.WorkflowStates
{
    public class WorkflowStateSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowStateSummaryDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
