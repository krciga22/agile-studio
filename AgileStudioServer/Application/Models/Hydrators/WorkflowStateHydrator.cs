
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Workflows.APIs.DTOs;
using AgileStudioServer.CoreFeatures.Workflows.Services.Models;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class WorkflowStateHydrator : AbstractModelHydrator
    {
        public WorkflowStateHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) || 
                from == typeof(Data.Entities.WorkflowState) || 
                from == typeof(WorkflowStatePostDto) || 
                from == typeof(WorkflowStatePatchDto)
            ) && to == typeof(WorkflowState);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            Object? model = null;

            if (from is int)
            {
                var workflowState = _DBContext.WorkflowState.Find(from);
                if (workflowState != null)
                {
                    from = workflowState;
                }
            }

            if (from is Data.Entities.WorkflowState)
            {
                var entity = (Data.Entities.WorkflowState)from;
                model = new WorkflowState(entity.Title, entity.WorkflowID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is WorkflowStatePostDto)
            {
                var dto = (WorkflowStatePostDto)from;
                model = new WorkflowState(dto.Title, dto.WorkflowId);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is WorkflowStatePatchDto)
            {
                var dto = (WorkflowStatePatchDto)from;
                var entity = _DBContext.WorkflowState.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(WorkflowState), maxDepth, depth, referenceHydrator);
                    Hydrate(dto, model, maxDepth, depth, referenceHydrator);
                }
            }

            if (model == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var model = (WorkflowState)to;

            if (from is Data.Entities.WorkflowState)
            {
                var entity = (Data.Entities.WorkflowState)from;
                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;

                if (entity.Workflow != null)
                {
                    model.WorkflowId = entity.WorkflowID;
                }

                if (entity.CreatedBy != null)
                {
                    model.CreatedById = entity.CreatedByID;
                }
            }
            else if (from is WorkflowStatePostDto)
            {
                var dto = (WorkflowStatePostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                if (dto.WorkflowId > 0)
                {
                    model.WorkflowId = dto.WorkflowId;
                }
            }
            else if (from is WorkflowStatePatchDto)
            {
                var dto = (WorkflowStatePatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
