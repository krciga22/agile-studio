using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Auth.Permissions;

public class PermissionHydrator(DBContext dBContext) : AbstractEntityHydrator(dBContext)
{
    public override bool Supports(Type from, Type to)
    {
        return (
            from == typeof(int) ||
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
            if (model.ID > 0)
            {
                entity = _DBContext.Permission.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Permission), model.ID.ToString());
                }
            }
            else
            {
                entity = new Permission(model.Title);
            }

            if (entity != null)
            {
                Hydrate(model, entity, maxDepth, depth, referenceHydrator);
            }
        }
        else if (from is int)
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

            entity.ID = model.ID;
            entity.UUID = model.UUID;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Scope = model.Scope;
            entity.CreatedOn = model.CreatedOn;
            entity.CreatedByID = model.CreatedByID;

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