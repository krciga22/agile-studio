namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string TitleOpposite { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public BacklogItemLinkTypeModel(string title, string titleOpposite)
        {
            Title = title;
            TitleOpposite = titleOpposite;
            CreatedOn = DateTime.UtcNow;
        }
    }
}