
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.Features.Users.Users
{
    public class UserSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(UserModel)
            ) && to == typeof(UserSummaryDto);
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

            UserModel? model = null;
            if (from is int)
            {
                model = (UserModel)referenceHydrator.Hydrate(
                    from, typeof(UserModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is UserModel)
            {
                model = (UserModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new UserSummaryDto(model.ID, model.FirstName, model.LastName);
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

            var dto = (UserSummaryDto)to;

            if (from is UserModel)
            {
                var model = (UserModel)from;
                dto.ID = model.ID;
                dto.FirstName = model.FirstName;
                dto.LastName = model.LastName;
            }
        }
    }
}
