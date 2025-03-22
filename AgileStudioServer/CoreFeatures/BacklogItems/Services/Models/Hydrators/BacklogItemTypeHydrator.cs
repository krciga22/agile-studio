using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.Services.Models.Hydrators
{
    public class BacklogItemTypeHydrator : AbstractModelHydrator
    {
        public BacklogItemTypeHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Data.Entities.BacklogItemType) ||
                from == typeof(BacklogItemTypePostDto) ||
                from == typeof(BacklogItemTypePatchDto)
            ) && to == typeof(BacklogItemType);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? model = null;

            if (from is int)
            {
                var backlogItemType = _DBContext.BacklogItemType.Find(from);
                if (backlogItemType != null)
                {
                    from = backlogItemType;
                }
            }

            if (from is Data.Entities.BacklogItemType)
            {
                var entity = (Data.Entities.BacklogItemType)from;
                model = new BacklogItemType(
                    entity.Title, entity.BacklogItemTypeSchemaID, entity.WorkflowID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemTypePostDto)
            {
                var dto = (BacklogItemTypePostDto)from;
                model = new BacklogItemType(
                    dto.Title, dto.BacklogItemTypeSchemaId, dto.WorkflowId);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemTypePatchDto)
            {
                var dto = (BacklogItemTypePatchDto)from;
                var entity = _DBContext.BacklogItemType.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItemType), maxDepth, depth, referenceHydrator);
                    Hydrate(dto, model, maxDepth, depth, referenceHydrator);
                }
            }

            if (model == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var model = (BacklogItemType)to;

            if (from is Data.Entities.BacklogItemType)
            {
                var entity = (Data.Entities.BacklogItemType)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.BacklogItemTypeSchemaID = entity.BacklogItemTypeSchemaID;
                model.WorkflowID = entity.WorkflowID;
                model.CreatedByID = entity.CreatedByID;
            }
            else if (from is BacklogItemTypePostDto)
            {
                var dto = (BacklogItemTypePostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.BacklogItemTypeSchemaID = dto.BacklogItemTypeSchemaId;
                model.WorkflowID = dto.WorkflowId;
            }
            else if (from is BacklogItemTypePatchDto)
            {
                var dto = (BacklogItemTypePatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
