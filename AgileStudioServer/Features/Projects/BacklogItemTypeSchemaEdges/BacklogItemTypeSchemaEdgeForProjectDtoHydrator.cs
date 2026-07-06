using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeForProjectDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchemaEdgeModel)
            ) && to == typeof(BacklogItemTypeSchemaEdgeForProjectDto);
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

            BacklogItemTypeSchemaEdgeModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemTypeSchemaEdgeModel)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemTypeSchemaEdgeModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemTypeSchemaEdgeModel)
            {
                model = (BacklogItemTypeSchemaEdgeModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                var toTypeSummaryDto = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                    model.ToTypeID, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                );

                BacklogItemTypeSummaryDto? fromTypeSummaryDto = null;
                if (model.FromTypeID != null) {
                    fromTypeSummaryDto = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                        model.FromTypeID, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                    );
                }

                dto = new BacklogItemTypeSchemaEdgeForProjectDto(
                    model.ID, fromTypeSummaryDto, toTypeSummaryDto);

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

            var dto = (BacklogItemTypeSchemaEdgeForProjectDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemTypeSchemaEdgeModel)
            {
                var model = (BacklogItemTypeSchemaEdgeModel)from;
                dto.ID = model.ID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.ToType = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                        model.ToTypeID, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                    );

                    if (model.FromTypeID != null)
                    {
                        dto.FromType = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                            model.FromTypeID, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
