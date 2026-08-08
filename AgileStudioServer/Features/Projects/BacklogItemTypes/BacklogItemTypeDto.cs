namespace AgileStudioServer.Features.Projects.BacklogItemTypes
{
    public class BacklogItemTypeDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public BacklogItemTypeDto(
            int id,
            string title)
        {
            ID = id;
            Title = title;
        }
    }
}
