namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaForProjectDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public BacklogItemTypeSchemaForProjectDto(
            int id,
            string title)
        {
            ID = id;
            Title = title;
        }
    }
}
