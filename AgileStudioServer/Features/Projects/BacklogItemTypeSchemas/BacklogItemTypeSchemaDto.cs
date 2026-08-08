namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public BacklogItemTypeSchemaDto(
            int id,
            string title)
        {
            ID = id;
            Title = title;
        }
    }
}
