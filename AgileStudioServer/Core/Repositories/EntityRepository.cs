using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Core.Repositories
{
    /// <summary>
    /// Repository for storage and retrieval of entities.
    /// </summary>
    public abstract class EntityRepository <TDBContext, TModel, TEntity, TIdentifier> : Repository, IRepository<TModel, TIdentifier>
        where TDBContext : DbContext
        where TModel : class
        where TEntity : class
        where TIdentifier : notnull
    {
        protected TDBContext _DBContext;
        protected Hydrator.Hydrator _Hydrator;

        public EntityRepository(TDBContext dBContext, Hydrator.Hydrator hydrator)
        {
            _DBContext = dBContext;
            _Hydrator = hydrator;
        }

        public abstract TIdentifier GetIdentifier(TModel model);

        public TModel? Get(TIdentifier id)
        {
            DbSet<TEntity> dbSet = GetDbSet();
            var entity = (id is object[] arr) ? dbSet.Find(arr) : dbSet.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateModel(entity);
        }

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
            catch(Exception e)
            {
                throw new ToIdentifierException(targetType, e);
            }

            throw new UnsupportedIdentifierException(targetType);
        }

        public bool Exists(TIdentifier id)
        {
            var entityType = _DBContext.Model.FindEntityType(typeof(TEntity)) ?? 
                throw new Exception($"Unable to find entity type for entity {typeof(TEntity)}");

            var primaryKey = entityType.FindPrimaryKey() ??
                throw new Exception($"Unable to find primary key for entity {entityType}");

            var primaryKeyProperty = primaryKey.Properties[0];

            return GetDbSet().Any(e => 
                EF.Property<TIdentifier>(e, primaryKeyProperty.Name).Equals(id));
        }

        public TModel Create(TModel model)
        {
            TEntity entity = HydrateEntity(model);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateModel(entity);
        }

        public TModel Update(TModel model)
        {
            TEntity entity = HydrateEntity(model);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateModel(entity);
        }

        public void Delete(TModel model)
        {
            var entity = HydrateEntity(model);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        protected abstract DbSet<TEntity> GetDbSet();

        protected List<TModel> HydrateModels(List<TEntity> entities, int depth = 3)
        {
            List<TModel> models = new();

            entities.ForEach(entity =>
            {
                TModel model = HydrateModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        protected TModel HydrateModel(TEntity entity, int depth = 3)
        {
            return (TModel)_Hydrator.Hydrate(
                entity, typeof(TModel), depth
            );
        }

        protected TEntity HydrateEntity(TModel model, int depth = 3)
        {
            return (TEntity)_Hydrator.Hydrate(
                model, typeof(TEntity), depth
            );
        }
    }
}
