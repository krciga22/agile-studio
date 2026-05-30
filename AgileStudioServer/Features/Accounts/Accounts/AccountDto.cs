using AgileStudioServer.Features.Accounts.AccountTypes;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountDto(int id, AccountTypeDto accountType, DateTime createdOn)
    {
        public int ID { get; set; } = id;

        public AccountTypeDto AccountType { get; set; } = accountType;

        public DateTime CreatedOn { get; set; } = createdOn;

        public UserSummaryDto? CreatedBy { get; set; }
    }
}