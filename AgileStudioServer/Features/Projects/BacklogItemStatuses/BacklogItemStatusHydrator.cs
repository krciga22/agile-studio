using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusHydrator : AbstractEntityHydrator
    {
        public BacklogItemStatusHydrator(DBContext dBContext) : base(dBContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemStatusModel)
            ) && to == typeof(BacklogItemStatus);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is BacklogItemStatusModel)
            {
                var model = (BacklogItemStatusModel)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemStatus.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemStatus), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new BacklogItemStatus(
                        model.BacklogItemID,
                        model.WorkflowStateID
                    );
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.BacklogItemStatus.Find(from);
            }

            if (entity == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return entity;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var entity = (BacklogItemStatus)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemStatusModel)
            {
                var model = (BacklogItemStatusModel)from;

                entity.ID = model.ID;
                entity.BacklogItemID = model.BacklogItemID;
                entity.WorkflowStateID = model.WorkflowStateID;
                entity.CreatedOn = model.CreatedOn;
                entity.CreatedByID = model.CreatedByID;
                entity.Comment = model.Comment;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.BacklogItem = (BacklogItem)referenceHydrator.Hydrate(
                        model.BacklogItemID, typeof(BacklogItem), maxDepth, nextDepth
                    );

                    entity.WorkflowState = (WorkflowState)referenceHydrator.Hydrate(
                        model.WorkflowStateID, typeof(WorkflowState), maxDepth, nextDepth
                    );

                    if (model.CreatedByID != null)
                    {
                        entity.CreatedBy = (User)referenceHydrator.Hydrate(
                            model.CreatedByID, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
        }
    }
}