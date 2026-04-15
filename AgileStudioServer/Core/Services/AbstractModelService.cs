using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories.Exceptions;

namespace AgileStudioServer.Core.Services
{
    public abstract class AbstractModelService<TModel, TIdentifier> : IModelService<TModel, TIdentifier>
        where TModel : class
        where TIdentifier : notnull
    {
        public abstract TModel Create(TModel model);
        public abstract void Delete(TModel model);
        public abstract TModel Get(TIdentifier id);
        public abstract PaginationResults<TModel> GetCollection();
        public abstract TModel Update(TModel model);

        /// <summary>
        /// Convert an object array into the appropriate TIdentifier type.
        /// </summary>
        /// <exception cref="ToIdentifierException"></exception>
        /// <exception cref="UnsupportedIdentifierException"></exception>
        public TIdentifier ToIdentifier(object[] id)
        {
            var targetType = typeof(TIdentifier);

            try
            {
                if (id.Length == 1)
                {
                    // Try to convert single value to TIdentifier (e.g., int, Guid, string, etc.)
                    return (TIdentifier)Convert.ChangeType(id[0], targetType);
                }
                else if (targetType == typeof(int[]))
                {
                    return (TIdentifier)(object)id.Select(x => Convert.ToInt32(x)).ToArray();
                }
                else if (targetType == typeof(string[]))
                {
                    return (TIdentifier)(object)id.Select(x => Convert.ToString(x)).ToArray();
                }
            }
            catch (Exception e)
            {
                throw new ToIdentifierException(targetType, e);
            }

            throw new UnsupportedIdentifierException(targetType);
        }
    }
}
