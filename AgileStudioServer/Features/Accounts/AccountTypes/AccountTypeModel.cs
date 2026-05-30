namespace AgileStudioServer.Features.Accounts.AccountTypes
{
    public class AccountTypeModel(string title)
    {
        public int ID { get; set; }

        public string Title { get; set; } = title;

        public string? Description { get; set; } = null;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; } = null!;
    }
}