using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Auth.Permissions
{
    public class PermissionModelHydrator(DBContext dbContext) : AbstractModelHydrator(dbContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(string) ||
                from == typeof(Permission)
            ) && to == typeof(PermissionModel);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? model = null;

            if (from is string)
            {
                var permission = _DBContext.Permission.Find(from);
                if (permission != null)
                {
                    from = permission;
                }
            }

            if (from is Permission)
            {
                var entity = (Permission)from;
                model = new PermissionModel(entity.PermissionKey, entity.Title);
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

            var model = (PermissionModel)to;

            if (from is Permission)
            {
                var entity = (Permission)from;

                model.PermissionKey = entity.PermissionKey;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.Scope = entity.Scope;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
                model.IsSystemPermission = entity.IsSystemPermission;
            }
        }
    }
}