using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Auth.RolePermissions
{
    public class RolePermissionModelHydrator(DBContext dbContext) : AbstractModelHydrator(dbContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(RolePermission)
            ) && to == typeof(RolePermissionModel);
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
                var entity = _DBContext.RolePermission.Find(from);
                if (entity != null)
                {
                    from = entity;
                }
            }

            if (from is RolePermission)
            {
                var entity = (RolePermission)from;
                model = new RolePermissionModel(entity.RoleKey, entity.PermissionID);
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

            var model = (RolePermissionModel)to;

            if (from is RolePermission)
            {
                var entity = (RolePermission)from;

                model.ID = entity.ID;
                model.RoleKey = entity.RoleKey;
                model.PermissionID = entity.PermissionID;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
        }
    }
}