using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Auth.RolePermissions
{
    public class RolePermissionHydrator(DBContext dBContext) : AbstractEntityHydrator(dBContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(object[]) ||
                from == typeof(RolePermissionModel)
            ) && to == typeof(RolePermission);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is RolePermissionModel)
            {
                var model = (RolePermissionModel)from;
                if (model.RoleKey != null && model.PermissionKey != null)
                {
                    entity = _DBContext.RolePermission.Find([model.RoleKey, model.PermissionKey]);
                    if (entity == null)
                    {
                        entity = new RolePermission(model.RoleKey, model.PermissionKey);
                    }
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is object[])
            {
                entity = _DBContext.RolePermission.Find(from);
            }

            if (entity == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return entity;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var entity = (RolePermission)to;
            int nextDepth = depth + 1;

            if (from is RolePermissionModel)
            {
                var model = (RolePermissionModel)from;

                entity.RoleKey = model.RoleKey;
                entity.PermissionKey = model.PermissionKey;
                entity.CreatedOn = model.CreatedOn;
                entity.CreatedByID = model.CreatedByID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.Role = (Role)referenceHydrator.Hydrate(
                        model.RoleKey, typeof(Role), maxDepth, nextDepth
                    );

                    entity.Permission = (Permission)referenceHydrator.Hydrate(
                        model.PermissionKey, typeof(Permission), maxDepth, nextDepth
                    );

                    if (model.CreatedByID != null)
                    {
                        entity.CreatedBy = (User)referenceHydrator.Hydrate(
                            model.CreatedByID, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
        }
    }
}