using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowModelHydrator : AbstractModelHydrator
    {
        public WorkflowModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Workflow) ||
                from == typeof(WorkflowPostDto) ||
                from == typeof(WorkflowPatchDto)
            ) && to == typeof(WorkflowModel);
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
                var workflow = _DBContext.Workflow.Find(from);
                if (workflow != null)
                {
                    from = workflow;
                }
            }

            if (from is Workflow)
            {
                var entity = (Workflow)from;
                model = new WorkflowModel(entity.Title, entity.AccountID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is WorkflowPostDto)
            {
                var dto = (WorkflowPostDto)from;
                model = new WorkflowModel(dto.Title, dto.AccountId);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is WorkflowPatchDto)
            {
                var dto = (WorkflowPatchDto)from;
                var entity = _DBContext.Workflow.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(WorkflowModel), maxDepth, depth, referenceHydrator);
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

            var model = (WorkflowModel)to;

            if (from is Workflow)
            {
                var entity = (Workflow)from;
                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedById = entity.CreatedByID;
                model.AccountID = entity.AccountID;
            }
            else if (from is WorkflowPostDto)
            {
                var dto = (WorkflowPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.AccountID = dto.AccountId;
            }
            else if (from is WorkflowPatchDto)
            {
                var dto = (WorkflowPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
