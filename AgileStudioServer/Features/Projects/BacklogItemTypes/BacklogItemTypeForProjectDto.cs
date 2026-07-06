namespace AgileStudioServer.Features.Projects.BacklogItemTypes
{
    public class BacklogItemTypeForProjectDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public BacklogItemTypeForProjectDto(
            int id,
            string title)
        {
            ID = id;
            Title = title;
        }
    }
}
