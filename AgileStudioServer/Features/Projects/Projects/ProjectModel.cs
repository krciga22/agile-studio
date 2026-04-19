namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public int BacklogItemTypeSchemaID { get; set; }

        public int BacklogItemLinkTypeSchemaID { get; set; }

        public ProjectModel(string title, int backlogItemTypeSchemaID, int backlogItemLinkTypeSchemaID)
        {
            Title = title;
            CreatedOn = DateTime.UtcNow;
            BacklogItemTypeSchemaID = backlogItemTypeSchemaID;
            BacklogItemLinkTypeSchemaID = backlogItemLinkTypeSchemaID;
        }
    }
}