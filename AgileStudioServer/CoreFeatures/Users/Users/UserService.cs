using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Users.Users
{
    public class UserService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public UserService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual UserModel? Get(int id)
        {
            User? entity = _DBContext.User.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateUserModel(entity);
        }

        public virtual UserModel Create(UserModel user)
        {
            User entity = HydrateUserEntity(user);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateUserModel(entity);
        }

        public virtual UserModel Update(UserModel user)
        {
            User entity = HydrateUserEntity(user);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateUserModel(entity);
        }

        public virtual void Delete(UserModel user)
        {
            User entity = HydrateUserEntity(user);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<UserModel> HydrateUserModels(List<User> entities, int depth = 3)
        {
            List<UserModel> models = new();

            entities.ForEach(entity =>
            {
                UserModel model = HydrateUserModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private UserModel HydrateUserModel(User user, int depth = 3)
        {
            return (UserModel)_Hydrator.Hydrate(
                user, typeof(UserModel), depth
            );
        }

        private User HydrateUserEntity(UserModel user, int depth = 3)
        {
            return (User)_Hydrator.Hydrate(
                user, typeof(User), depth
            );
        }
    }
}
