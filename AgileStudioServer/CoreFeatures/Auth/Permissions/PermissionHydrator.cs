using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Auth.Permissions;

public class PermissionHydrator(DBContext dBContext) : AbstractEntityHydrator(dBContext)
{
    public override bool Supports(Type from, Type to)
    {
        return (
            from == typeof(string) ||
            from == typeof(PermissionModel)
        ) && to == typeof(Permission);
    }

    public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
    {
        if (!Supports(from.GetType(), to))
        {
            throw new HydrationNotSupportedException(from.GetType(), to);
        }

        object? entity = null;

        if (from is PermissionModel)
        {
            var model = (PermissionModel)from;
            if (model.PermissionKey != null)
            {
                entity = _DBContext.Permission.Find(model.PermissionKey);
                if (entity == null)
                {
                    entity = new Permission(model.PermissionKey, model.Title);
                }
            }

            if (entity != null)
            {
                Hydrate(model, entity, maxDepth, depth, referenceHydrator);
            }
        }
        else if (from is string)
        {
            entity = _DBContext.Permission.Find(from);
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

        var entity = (Permission)to;
        int nextDepth = depth + 1;

        if (from is PermissionModel)
        {
            var model = (PermissionModel)from;

            entity.PermissionKey = model.PermissionKey;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Scope = model.Scope;
            entity.CreatedOn = model.CreatedOn;
            entity.CreatedByID = model.CreatedByID;
            entity.IsSystemPermission = model.IsSystemPermission;

            if (referenceHydrator != null && nextDepth <= maxDepth)
            {
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