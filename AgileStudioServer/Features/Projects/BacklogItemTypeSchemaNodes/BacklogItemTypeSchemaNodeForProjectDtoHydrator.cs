using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeForProjectDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchemaNodeModel)
            ) && to == typeof(BacklogItemTypeSchemaNodeForProjectDto);
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

            BacklogItemTypeSchemaNodeModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemTypeSchemaNodeModel)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemTypeSchemaNodeModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemTypeSchemaNodeModel)
            {
                model = (BacklogItemTypeSchemaNodeModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                var backlogItemTypeSummaryDto = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemTypeID, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                );

                dto = new BacklogItemTypeSchemaNodeForProjectDto(
                    model.ID, backlogItemTypeSummaryDto);
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

            var dto = (BacklogItemTypeSchemaNodeForProjectDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemTypeSchemaNodeModel)
            {
                var model = (BacklogItemTypeSchemaNodeModel)from;
                dto.ID = model.ID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.BacklogItemType = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                        model.BacklogItemTypeID, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                    );
                }
            }
        }
    }
}
