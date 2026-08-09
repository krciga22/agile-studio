using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Features.Accounts.WorkflowStates;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemStatusModel)
            ) && to == typeof(BacklogItemStatusSummaryDto);
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

            BacklogItemStatusModel? model = null;
            if (from is int)
            {
                model = (BacklogItemStatusModel)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemStatusModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemStatusModel)
            {
                model = (BacklogItemStatusModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                var workflowStateSummaryDto = (WorkflowStateSummaryDto)referenceHydrator.Hydrate(
                    model.WorkflowStateID, typeof(WorkflowStateSummaryDto), maxDepth, depth
                );

                dto = new BacklogItemStatusSummaryDto(model.ID, workflowStateSummaryDto);
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

            var dto = (BacklogItemStatusSummaryDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemStatusModel)
            {
                var model = (BacklogItemStatusModel)from;
                dto.ID = model.ID;
                dto.Comment = model.Comment;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.WorkflowState = (WorkflowStateSummaryDto)referenceHydrator.Hydrate(
                        model.WorkflowStateID, typeof(WorkflowStateSummaryDto), maxDepth, depth
                    );
                }
            }
        }
    }
}