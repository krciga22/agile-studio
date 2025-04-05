
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.CoreFeatures.Workflows.Workflows
{
    public class WorkflowSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(WorkflowModel)
            ) && to == typeof(WorkflowSummaryDto);
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

            WorkflowModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (WorkflowModel)referenceHydrator.Hydrate(
                    from, typeof(WorkflowModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is WorkflowModel)
            {
                model = (WorkflowModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new WorkflowSummaryDto(model.ID, model.Title);
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

            var dto = (WorkflowSummaryDto)to;

            if (from is WorkflowModel)
            {
                var model = (WorkflowModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
