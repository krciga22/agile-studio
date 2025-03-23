using AgileStudioServer.Core.Hydrator;
using Entities = AgileStudioServer.CoreFeatures.BacklogItems.Repositories.Entities;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.Services
{
    public class BacklogItemLinkTypeService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public BacklogItemLinkTypeService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual BacklogItemLinkType? Get(int id)
        {
            Entities.BacklogItemLinkType? entity = _DBContext.BacklogItemLinkType.Find(id);
            if (entity is null)
            {
                return null;
            }

            _DBContext.Entry(entity).Reference("CreatedBy").Load();

            return HydrateBacklogItemLinkTypeModel(entity);
        }

        public virtual BacklogItemLinkType Create(BacklogItemLinkType backlogItemLinkType)
        {
            Entities.BacklogItemLinkType entity = HydrateBacklogItemLinkTypeEntity(backlogItemLinkType);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemLinkTypeModel(entity);
        }

        public virtual BacklogItemLinkType Update(BacklogItemLinkType backlogItemLinkType)
        {
            Entities.BacklogItemLinkType entity = HydrateBacklogItemLinkTypeEntity(backlogItemLinkType);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemLinkTypeModel(entity);
        }

        public virtual void Delete(BacklogItemLinkType backlogItemLinkType)
        {
            Entities.BacklogItemLinkType entity = HydrateBacklogItemLinkTypeEntity(backlogItemLinkType);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private BacklogItemLinkType HydrateBacklogItemLinkTypeModel(Entities.BacklogItemLinkType backlogItemLinkType, int depth = 3)
        {
            return (BacklogItemLinkType)_Hydrator.Hydrate(
                backlogItemLinkType, typeof(BacklogItemLinkType), depth
            );
        }

        private Entities.BacklogItemLinkType HydrateBacklogItemLinkTypeEntity(BacklogItemLinkType backlogItemLinkType, int depth = 3)
        {
            return (Entities.BacklogItemLinkType)_Hydrator.Hydrate(
                backlogItemLinkType, typeof(Entities.BacklogItemLinkType), depth
            );
        }
    }
}
