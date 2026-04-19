
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(ProjectModel)
            ) && to == typeof(ProjectSummaryDto);
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

            ProjectModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (ProjectModel)referenceHydrator.Hydrate(
                    from, typeof(ProjectModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is ProjectModel)
            {
                model = (ProjectModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new ProjectSummaryDto(model.ID, model.Title);
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

            var dto = (ProjectSummaryDto)to;

            if (from is ProjectModel)
            {
                var model = (ProjectModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
