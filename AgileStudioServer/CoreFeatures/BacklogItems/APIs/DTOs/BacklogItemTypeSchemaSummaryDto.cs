namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs
{
    public class BacklogItemTypeSchemaSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public BacklogItemTypeSchemaSummaryDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
