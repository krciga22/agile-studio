using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Features.Projects.Releases;

namespace AgileStudioServer.Core.Services
{
    public interface IModelService
    {
        
    }

    public interface IModelService<TModel, TIdentifier>  : IModelService
        where TModel : class
        where TIdentifier : notnull
    {
        TIdentifier ToIdentifier(object[] id);

        TIdentifier GetIdentifier(TModel model);

        PaginationResults<TModel> GetCollection();

        PaginationResults<TModel> GetSubCollection(String parentResourceType, Object[] id);

        TModel Get(TIdentifier id);

        TModel Create(TModel model);

        TModel Update(TModel model);

        void Delete(TModel model);
    }
}
