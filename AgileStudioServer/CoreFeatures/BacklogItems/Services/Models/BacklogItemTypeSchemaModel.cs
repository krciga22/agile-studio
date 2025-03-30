namespace AgileStudioServer.CoreFeatures.BacklogItems.Services.Models
{
    public class BacklogItemTypeSchemaModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedById { get; set; } = null!;

        public BacklogItemTypeSchemaModel(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}