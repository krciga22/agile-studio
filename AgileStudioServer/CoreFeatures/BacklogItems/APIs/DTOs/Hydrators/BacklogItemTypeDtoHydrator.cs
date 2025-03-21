
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Workflows.APIs.DTOs;
using AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs;
using AgileStudioServer.CoreFeatures.Users.APIs.DTOs;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;

namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs.Hydrators
{
    public class BacklogItemTypeDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemType)
            ) && to == typeof(BacklogItemTypeDto);
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

            BacklogItemType? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemType)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemType), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemType)
            {
                model = (BacklogItemType)from;
            }

            object? dto = null;
            if (model != null && referenceHydrator != null)
            {
                var backlogItemTypeSchemaSummaryDto = (BacklogItemTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemTypeSchemaID, typeof(BacklogItemTypeSchemaSummaryDto), maxDepth, depth
                );

                var workflowSummaryDto = (WorkflowSummaryDto)referenceHydrator.Hydrate(
                    model.WorkflowID, typeof(WorkflowSummaryDto), maxDepth, depth
                );

                dto = new BacklogItemTypeDto(
                    model.ID, model.Title, model.CreatedOn,
                    backlogItemTypeSchemaSummaryDto, workflowSummaryDto);

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

            var dto = (BacklogItemTypeDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemType)
            {
                var model = (BacklogItemType)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.BacklogItemTypeSchema = (BacklogItemTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                        model.BacklogItemTypeSchemaID, typeof(BacklogItemTypeSchemaSummaryDto), maxDepth, depth
                    );

                    dto.Workflow = (WorkflowSummaryDto)referenceHydrator.Hydrate(
                        model.WorkflowID, typeof(WorkflowSummaryDto), maxDepth, depth
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
