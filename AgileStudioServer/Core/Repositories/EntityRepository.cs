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
        where TIdentifier : struct
    {
        protected TDBContext _DbContext;
        protected Hydrator.Hydrator _Hydrator;

        public EntityRepository(TDBContext dBContext, Hydrator.Hydrator hydrator)
        {
            _DbContext = dBContext;
            _Hydrator = hydrator;
        }

        public abstract TIdentifier GetIdentifier(TModel model);

        public TModel? Get(TIdentifier id)
        {
            DbSet<TEntity> dbSet = GetDbSet();
            var entity = dbSet.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateModel(entity);
        }

        public TModel Create(TModel model)
        {
            TEntity entity = HydrateEntity(model);

            _DbContext.Add(entity);
            _DbContext.SaveChanges();

            return HydrateModel(entity);
        }

        public TModel Update(TModel model)
        {
            TEntity entity = HydrateEntity(model);

            _DbContext.Update(entity);
            _DbContext.SaveChanges();

            return HydrateModel(entity);
        }

        public void Delete(TModel model)
        {
            var entity = HydrateEntity(model);

            _DbContext.Remove(entity);
            _DbContext.SaveChanges();
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
