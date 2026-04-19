namespace AgileStudioServer.Features.Projects.Sprints
{
    public class SprintModel
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        public int ProjectID { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public SprintModel(int sprintNumber, int projectId)
        {
            SprintNumber = sprintNumber;
            CreatedOn = DateTime.UtcNow;
            ProjectID = projectId;
        }
    }
}