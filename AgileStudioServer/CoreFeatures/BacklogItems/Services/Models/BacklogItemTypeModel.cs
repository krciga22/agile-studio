namespace AgileStudioServer.CoreFeatures.BacklogItems.Services.Models
{
    public class BacklogItemTypeModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public int BacklogItemTypeSchemaID { get; set; }

        public int WorkflowID { get; set; }

        public BacklogItemTypeModel(string title, int backlogItemTypeSchemaId, int workflowId)
        {
            Title = title;
            CreatedOn = DateTime.Now;
            BacklogItemTypeSchemaID = backlogItemTypeSchemaId;
            WorkflowID = workflowId;
        }
    }
}