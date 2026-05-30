using AgileStudioServer.Features.Accounts.AccountTypes;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class Account(int accountTypeID)
    {
        public int ID { get; set; }

        public int AccountTypeID { get; set; } = accountTypeID;

        public AccountType AccountType { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;
    }
}