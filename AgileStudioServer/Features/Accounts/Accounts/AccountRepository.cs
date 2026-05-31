using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountRepository(DBContext dbContext, Hydrator hydrator) : 
        EntityRepository<DBContext, AccountModel, Account, int>(dbContext, hydrator)
    {
        public override int GetIdentifier(AccountModel model)
        {
            return model.ID;
        }

        /// <summary>
        /// Get accounts readable by the current user.
        /// </summary>
        public virtual PaginationResults<AccountModel> GetAccountsForCurrentUser(ServiceContext serviceContext)
        {
            var currentUserId = serviceContext.GetCurrentUserIdStrict();

            var query =
                (from account in _DBContext.Account
                 join grant in _DBContext.RoleGrant on account.ID.ToString() equals grant.ScopeID
                 join rolePerm in _DBContext.RolePermission on grant.RoleKey equals rolePerm.RoleKey
                 where grant.SubjectType == RoleSubjectTypes.USER
                     && grant.SubjectID == currentUserId.ToString()
                     && grant.Scope == Scopes.ACCOUNT
                     && rolePerm.PermissionKey == PermissionKeys.READ
                 select account)
                .Distinct();

            query = ApplySearchToQuery(query, serviceContext);

            // todo apply filters to query

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            query = query.Include(a => a.AccountType);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        protected override DbSet<Account> GetDbSet()
        {
            return _DBContext.Account;
        }

        private static IQueryable<Account> ApplySearchToQuery(IQueryable<Account> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(a => a.ID.ToString().Equals(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<Account> ApplySortToQuery(IQueryable<Account> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<Account>? result = null;

            int sortedFieldsCount = 0;
            if (!string.IsNullOrWhiteSpace(serviceContext.Sort))
            {
                string[] sorts = serviceContext.Sort.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (string sort in sorts)
                {
                    sortedFieldsCount++;

                    string[] sortParts = sort.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    string sortField = sortParts[0];
                    bool descending = sortParts.Length > 1 && sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                    Expression<Func<Account, string>> sortKeySelector = a => ((DateTimeOffset)a.CreatedOn).ToUnixTimeSeconds().ToString();
                    switch (sortField)
                    {
                        case "accountType":
                            sortKeySelector = a => a.AccountType != null ? a.AccountType.Title : string.Empty;
                            break;
                        case "id":
                            sortKeySelector = a => a.ID.ToString();
                            break;
                        case "createdOn":
                            sortKeySelector = a => ((DateTimeOffset)a.CreatedOn).ToUnixTimeSeconds().ToString();
                            break;
                        default:
                            sortedFieldsCount--;
                            break;
                    }

                    result = descending ?
                        query.OrderByDescending(sortKeySelector) :
                        query.OrderBy(sortKeySelector);
                }
            }

            if (result == null)
            {
                Expression<Func<Account, string>> sortKeySelector = a => a.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<AccountModel> GetPaginationResultsFromQuery(IQueryable<Account> query, ServiceContext serviceContext, int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<Account> entities = query.ToList();
            List<AccountModel> models = HydrateModels(entities);
            return new PaginationResults<AccountModel>(models, total, page, pageSize);
        }
    }
}