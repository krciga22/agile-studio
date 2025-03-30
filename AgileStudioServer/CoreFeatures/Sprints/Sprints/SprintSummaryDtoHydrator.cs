
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.CoreFeatures.Sprints.Sprints
{
    public class SprintSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(SprintModel)
            ) && to == typeof(SprintSummaryDto);
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

            SprintModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (SprintModel)referenceHydrator.Hydrate(
                    from, typeof(SprintModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is SprintModel)
            {
                model = (SprintModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new SprintSummaryDto(model.ID, model.SprintNumber);
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

            var dto = (SprintSummaryDto)to;

            if (from is SprintModel)
            {
                var model = (SprintModel)from;
                dto.ID = model.ID;
                dto.SprintNumber = model.SprintNumber;
            }
        }
    }
}
