
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Releases.APIs.DTOs;
using AgileStudioServer.CoreFeatures.Releases.Services.Models;

namespace AgileStudioServer.CoreFeatures.Releases.APIs.DTOs.Hydrators
{
    public class ReleaseSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Release)
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

            Release? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (Release)referenceHydrator.Hydrate(
                    from, typeof(Release), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is Release)
            {
                model = (Release)from;
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

            if (from is Release)
            {
                var model = (Release)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
