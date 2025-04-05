using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.CoreFeatures.Projects.Services.Models
{
    public class ProjectModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public int BacklogItemTypeSchemaID { get; set; }

        public ProjectModel(string title, int backlogItemTypeSchemaID)
        {
            Title = title;
            CreatedOn = DateTime.Now;
            BacklogItemTypeSchemaID = backlogItemTypeSchemaID;
        }
    }
}