using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeRepository : EntityRepository<DBContext,  BacklogItemLinkTypeModel, BacklogItemLinkType, int>
    {
        private readonly ServiceContext _ServiceContext;

        public BacklogItemLinkTypeRepository(
            DBContext dbContext, 
            Hydrator hydrator,
            ServiceContext serviceContext
            ) : base(dbContext, hydrator)
        {
            _ServiceContext = serviceContext;
        }

        public override int GetIdentifier(BacklogItemLinkTypeModel model)
        {
            return model.ID;
        }

        public virtual PaginationResults<BacklogItemLinkTypeModel> GetByAccountID(int accountId)
        {
            var query =
                (from account in _DBContext.Account
                 join backlogItemLinkType in _DBContext.BacklogItemLinkType on account.ID equals backlogItemLinkType.AccountID
                 where account.ID == accountId
                 select backlogItemLinkType)
                .Distinct();

            query = ApplySearchToQuery(query);

            int total = query.Count();

            query = ApplySortToQuery(query);

            return GetPaginationResultsFromQuery(query, total);
        }

        public virtual PaginationResults<BacklogItemLinkTypeModel> GetByProjectID(int projectID)
        {
            var query =
                (from project in _DBContext.Project
                 join schemaEntry in _DBContext.BacklogItemLinkTypeSchemaEntry on project.BacklogItemLinkTypeSchemaID equals schemaEntry.BacklogItemLinkTypeSchemaID
                 join linkType in _DBContext.BacklogItemLinkType on schemaEntry.BacklogItemLinkTypeID equals linkType.ID
                 where project.ID == projectID
                 select linkType)
                .Distinct();

            query = ApplySearchToQuery(query);

            int total = query.Count();

            query = ApplySortToQuery(query);

            return GetPaginationResultsFromQuery(query, total);
        }

        protected override DbSet<BacklogItemLinkType> GetDbSet()
        {
            return _DBContext.BacklogItemLinkType;
        }

        private IQueryable<BacklogItemLinkType> ApplySearchToQuery(IQueryable<BacklogItemLinkType> query)
        {
            if (!string.IsNullOrWhiteSpace(_ServiceContext.SearchQuery))
            {
                string searchLower = _ServiceContext.SearchQuery.ToLower();
                query = query.Where(backlogItemLinkType => backlogItemLinkType.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private IOrderedQueryable<BacklogItemLinkType> ApplySortToQuery(IQueryable<BacklogItemLinkType> query)
        {
            IOrderedQueryable<BacklogItemLinkType>? result = null;

            int sortedFieldsCount = 0;
            if (!string.IsNullOrWhiteSpace(_ServiceContext.Sort))
            {
                string[] sorts = _ServiceContext.Sort.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (string sort in sorts)
                {
                    sortedFieldsCount++;

                    string[] sortParts = sort.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    string sortField = sortParts[0];
                    bool descending = sortParts.Length > 1 && sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                    Expression<Func<BacklogItemLinkType, string>> sortKeySelector = item =>
                        ((DateTimeOffset)item.CreatedOn).ToUnixTimeSeconds().ToString();
                    switch (sortField)
                    {
                        case "title":
                            sortKeySelector = item => item.Title;
                            break;
                        case "titleOpposite":
                            sortKeySelector = item => item.TitleOpposite;
                            break;
                        case "id":
                            sortKeySelector = item => item.ID.ToString();
                            break;
                        default:
                            sortedFieldsCount--;
                            break;
                    }

                    if (result == null)
                    {
                        result = descending ?
                            query.OrderByDescending(sortKeySelector) :
                            query.OrderBy(sortKeySelector);
                    }
                    else
                    {
                        result = descending ?
                            result.ThenByDescending(sortKeySelector) :
                            result.ThenBy(sortKeySelector);
                    }
                }
            }

            if (result == null)
            {
                Expression<Func<BacklogItemLinkType, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<BacklogItemLinkTypeModel> GetPaginationResultsFromQuery(IQueryable<BacklogItemLinkType> query, int total)
        {
            int page = _ServiceContext.Page;
            int pageSize = _ServiceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<BacklogItemLinkType> entities = query.ToList();
            List<BacklogItemLinkTypeModel> models = HydrateModels(entities);
            return new PaginationResults<BacklogItemLinkTypeModel>(models, total, page, pageSize);
        }
    }
}
