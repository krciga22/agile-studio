namespace AgileStudioServer.CoreFeatures.Projects.BacklogItems
{
    public class BacklogItemSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public BacklogItemSummaryDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
