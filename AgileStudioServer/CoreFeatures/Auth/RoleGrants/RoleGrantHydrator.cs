using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Auth.RoleGrants
{
    public class RoleGrantHydrator(DBContext dBContext) : AbstractEntityHydrator(dBContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(RoleGrantModel)
            ) && to == typeof(RoleGrant);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is RoleGrantModel)
            {
                var model = (RoleGrantModel)from;
                if (model.RoleKey != null && model.SubjectType != null && model.SubjectID != null)
                {
                    entity = _DBContext.RoleGrant.FirstOrDefault(
                        g => g.RoleKey == model.RoleKey
                          && g.SubjectType == model.SubjectType
                          && g.SubjectID == model.SubjectID
                          && g.Scope == model.Scope
                          && g.ScopeID == model.ScopeID
                    );
                    if (entity == null)
                    {
                        entity = new RoleGrant(model.RoleKey, model.SubjectType, model.SubjectID, model.Scope, model.ScopeID);
                    }
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.RoleGrant.Find(from);
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

            var entity = (RoleGrant)to;
            int nextDepth = depth + 1;

            if (from is RoleGrantModel)
            {
                var model = (RoleGrantModel)from;

                entity.RoleKey = model.RoleKey;
                entity.SubjectType = model.SubjectType;
                entity.SubjectID = model.SubjectID;
                entity.Scope = model.Scope;
                entity.ScopeID = model.ScopeID;
                entity.CreatedOn = model.CreatedOn;
                entity.CreatedByID = model.CreatedByID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.Role = (Role)referenceHydrator.Hydrate(
                        model.RoleKey, typeof(Role), maxDepth, nextDepth
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