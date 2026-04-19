namespace AgileStudioServer.Features.Projects.Releases
{
    public class ReleaseModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int ProjectID { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ReleaseModel(string title, int projectId)
        {
            Title = title;
            CreatedOn = DateTime.UtcNow;
            ProjectID = projectId;
        }
    }
}