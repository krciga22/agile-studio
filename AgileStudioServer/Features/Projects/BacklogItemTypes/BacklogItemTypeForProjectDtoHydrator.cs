
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;

namespace AgileStudioServer.Features.Projects.BacklogItemTypes
{
    public class BacklogItemTypeForProjectDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeModel)
            ) && to == typeof(BacklogItemTypeForProjectDto);
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

            BacklogItemTypeModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemTypeModel)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemTypeModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemTypeModel)
            {
                model = (BacklogItemTypeModel)from;
            }

            object? dto = null;
            if (model != null && referenceHydrator != null)
            {
                dto = new BacklogItemTypeForProjectDto(
                    model.ID, model.Title);

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

            var dto = (BacklogItemTypeForProjectDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemTypeModel)
            {
                var model = (BacklogItemTypeModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
            }
        }
    }
}
