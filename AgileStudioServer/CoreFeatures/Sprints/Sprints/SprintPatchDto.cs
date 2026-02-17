using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.CoreFeatures.Sprints.Sprints
{
    public class SprintPatchDto
    {
        [Required]
        public int ID { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        public SprintPatchDto(int id)
        {
            ID = id;
        }
    }
}
