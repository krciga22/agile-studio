
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Accounts.Workflows;

namespace AgileStudioServer.CoreFeatures.Accounts.WorkflowStates
{
    public class WorkflowStateDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(WorkflowStateModel)
            ) && to == typeof(WorkflowStateDto);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            if (referenceHydrator == null)
            {
                throw new ReferenceHydratorRequiredException(this);
            }

            WorkflowStateModel? model = null;
            if (from is int)
            {
                model = (WorkflowStateModel)referenceHydrator.Hydrate(
                    from, typeof(WorkflowStateModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is WorkflowStateModel)
            {
                model = (WorkflowStateModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                var workflowSummaryDto = (WorkflowSummaryDto)referenceHydrator.Hydrate(
                    model.WorkflowId, typeof(WorkflowSummaryDto), maxDepth, depth
                );

                dto = new WorkflowStateDto(model.ID, model.Title, workflowSummaryDto, model.CreatedOn);
                Hydrate(model, dto, maxDepth, depth, referenceHydrator);
            }

            if (dto == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return dto;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var dto = (WorkflowStateDto)to;
            int nextDepth = depth + 1;

            if (from is WorkflowStateModel)
            {
                var model = (WorkflowStateModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.Workflow = (WorkflowSummaryDto)referenceHydrator.Hydrate(
                        model.WorkflowId, typeof(WorkflowSummaryDto), maxDepth, depth
                    );

                    if (model.CreatedById != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedById, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
