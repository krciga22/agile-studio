
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Workflows.APIs.DTOs;
using AgileStudioServer.CoreFeatures.Workflows.Services.Models;

namespace AgileStudioServer.CoreFeatures.Workflows.APIs.DTOs.Hydrators
{
    public class WorkflowSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Workflow)
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

            Workflow? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (Workflow)referenceHydrator.Hydrate(
                    from, typeof(Workflow), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is Workflow)
            {
                model = (Workflow)from;
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

            if (from is Workflow)
            {
                var model = (Workflow)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
