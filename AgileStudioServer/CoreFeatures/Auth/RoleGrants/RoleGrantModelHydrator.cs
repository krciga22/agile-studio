using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Auth.RoleGrants
{
    public class RoleGrantModelHydrator(DBContext dbContext) : AbstractModelHydrator(dbContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(RoleGrant)
            ) && to == typeof(RoleGrantModel);
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
                var grant = _DBContext.RoleGrant.Find(from);
                if (grant != null)
                {
                    from = grant;
                }
            }

            if (from is RoleGrant)
            {
                var entity = (RoleGrant)from;
                model = new RoleGrantModel(entity.RoleKey, entity.SubjectType, entity.SubjectID);
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

            var model = (RoleGrantModel)to;

            if (from is RoleGrant)
            {
                var entity = (RoleGrant)from;
                model.ID = entity.ID;
                model.RoleKey = entity.RoleKey;
                model.SubjectType = entity.SubjectType;
                model.SubjectID = entity.SubjectID;
                model.Scope = entity.Scope;
                model.ScopeID = entity.ScopeID;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
        }
    }
}