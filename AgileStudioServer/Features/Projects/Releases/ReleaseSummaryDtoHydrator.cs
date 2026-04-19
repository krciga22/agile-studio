
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.Features.Projects.Releases
{
    public class ReleaseSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(ReleaseModel)
            ) && to == typeof(ReleaseSummaryDto);
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

            ReleaseModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (ReleaseModel)referenceHydrator.Hydrate(
                    from, typeof(ReleaseModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is ReleaseModel)
            {
                model = (ReleaseModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new ReleaseSummaryDto(model.ID, model.Title);
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

            var dto = (ReleaseSummaryDto)to;

            if (from is ReleaseModel)
            {
                var model = (ReleaseModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
