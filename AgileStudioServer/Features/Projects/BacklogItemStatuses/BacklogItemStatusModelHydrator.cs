using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusModelHydrator : AbstractModelHydrator
    {
        public BacklogItemStatusModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemStatus) ||
                from == typeof(BacklogItemStatusPostDto)
            ) && to == typeof(BacklogItemStatusModel);
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
                var entity = _DBContext.BacklogItemStatus.Find(from);
                if (entity != null)
                {
                    from = entity;
                }
            }

            if (from is BacklogItemStatus)
            {
                var entity = (BacklogItemStatus)from;
                model = new BacklogItemStatusModel(
                    entity.BacklogItemID,
                    entity.WorkflowStateID
                );
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemStatusPostDto)
            {
                var dto = (BacklogItemStatusPostDto)from;
                model = new BacklogItemStatusModel(
                    dto.BacklogItemID,
                    dto.WorkflowStateID
                );
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
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

            var model = (BacklogItemStatusModel)to;

            if (from is BacklogItemStatus)
            {
                var entity = (BacklogItemStatus)from;
                model.ID = entity.ID;
                model.BacklogItemID = entity.BacklogItemID;
                model.WorkflowStateID = entity.WorkflowStateID;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
                model.Comment = entity.Comment;
            }
            else if (from is BacklogItemStatusPostDto)
            {
                var dto = (BacklogItemStatusPostDto)from;
                model.BacklogItemID = dto.BacklogItemID;
                model.WorkflowStateID = dto.WorkflowStateID;
                model.Comment = dto.Comment;
            }
        }
    }
}