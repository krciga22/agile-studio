using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(AccountModel)
            ) && to == typeof(AccountSummaryDto);
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

            AccountModel? model = null;
            if (from is int)
            {
                model = (AccountModel)referenceHydrator.Hydrate(
                    from, typeof(AccountModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is AccountModel)
            {
                model = (AccountModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new AccountSummaryDto(model.ID);
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

            var dto = (AccountSummaryDto)to;
            int nextDepth = depth + 1;

            if (from is AccountModel)
            {
                var model = (AccountModel)from;
                dto.ID = model.ID;
            }
        }
    }
}