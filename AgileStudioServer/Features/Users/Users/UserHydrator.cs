using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Features.Users.Users
{
    public class UserHydrator : AbstractEntityHydrator
    {
        public UserHydrator(DBContext dBContext) : base(dBContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(UserModel)
            ) && to == typeof(User);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is UserModel)
            {
                var model = (UserModel)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.User.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(User), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new User(model.Email, model.FirstName, model.LastName);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.User.Find(from);
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

            var entity = (User)to;

            if (from is UserModel)
            {
                var model = (UserModel)from;

                entity.ID = model.ID;
                entity.Email = model.Email;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.CreatedOn = model.CreatedOn;
                entity.AuthServerUserID = model.AuthServerUserID;
            }
        }
    }
}
