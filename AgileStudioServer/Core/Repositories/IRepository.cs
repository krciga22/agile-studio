namespace AgileStudioServer.Core.Repositories
{
    /// <summary>
    /// Provides data storage and retrieval operations 
    /// for a specific model type.
    /// </summary>
    public interface IRepository<TModel, TIdentifier> 
        where TModel : class
        where TIdentifier : notnull
    {
        TIdentifier GetIdentifier(TModel model);
        TModel? Get(TIdentifier id);
        bool Exists(TIdentifier id);
        TModel Create(TModel model);
        TModel Update(TModel model);
        void Delete(TModel model);
    }
}
