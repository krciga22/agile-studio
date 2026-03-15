using AgileStudioServer.Core.Pagination;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;

namespace AgileStudioServer.Core.Services
{
    public class ResourceService : AbstractService
    {
        public PaginationResults<object> GetAll(string type, ServiceContext serviceContext)
        {
            // todo get resources using appropriate repository
            List<object> resources = new();
            resources.Add(new BacklogItemTypeSchemaDto(123, "test", new DateTime()));

            PaginationResults<object> paginationResults = new(
                resources,
                0,
                serviceContext.Page,
                serviceContext.ItemsPerPage
            );

            return paginationResults;
        }
    }
}
