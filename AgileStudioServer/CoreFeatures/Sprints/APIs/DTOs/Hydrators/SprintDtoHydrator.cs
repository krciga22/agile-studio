
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Sprints.APIs.DTOs;
using AgileStudioServer.CoreFeatures.Sprints.Services.Models;
using AgileStudioServer.CoreFeatures.Projects.APIs.DTOs;
using AgileStudioServer.CoreFeatures.Users.APIs.DTOs;

namespace AgileStudioServer.CoreFeatures.Sprints.APIs.DTOs.Hydrators
{
    public class SprintDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Sprint)
            ) && to == typeof(SprintDto);
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

            Sprint? model = null;
            if (from is int)
            {
                model = (Sprint)referenceHydrator.Hydrate(
                    from, typeof(Sprint), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is Sprint)
            {
                model = (Sprint)from;
            }

            object? dto = null;
            if (model != null)
            {
                var projectSummaryDto = (ProjectSummaryDto)referenceHydrator.Hydrate(
                    model.ProjectID, typeof(ProjectSummaryDto), maxDepth, depth
                );

                dto = new SprintDto(model.ID, model.SprintNumber, projectSummaryDto, model.CreatedOn);
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

            var dto = (SprintDto)to;
            int nextDepth = depth + 1;

            if (from is Sprint)
            {
                var model = (Sprint)from;
                dto.ID = model.ID;
                dto.SprintNumber = model.SprintNumber;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;
                dto.StartDate = model.StartDate;
                dto.EndDate = model.EndDate;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.Project = (ProjectSummaryDto)referenceHydrator.Hydrate(
                        model.ProjectID, typeof(ProjectSummaryDto), maxDepth, depth
                    );

                    if (model.CreatedByID != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedByID, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
