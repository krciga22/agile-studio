using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Auth.Roles
{
    public class RoleModelHydrator(DBContext dbContext) : AbstractModelHydrator(dbContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Role)
            ) && to == typeof(RoleModel);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? model = null;

            if (from is int)
            {
                var role = _DBContext.Role.Find(from);
                if (role != null)
                {
                    from = role;
                }
            }

            if (from is Role)
            {
                var entity = (Role)from;
                model = new RoleModel(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }

            if (model == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var model = (RoleModel)to;

            if (from is Role)
            {
                var entity = (Role)from;

                model.ID = entity.ID;
                model.UUID = entity.UUID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.Scope = entity.Scope;
                model.ScopeID = entity.ScopeID;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
        }
    }
}
