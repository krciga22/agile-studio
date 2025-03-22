using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.Users.Services.Models;
using AgileStudioServer.Data;
using Entities = AgileStudioServer.CoreFeatures.Users.Repositories.Entities;

namespace AgileStudioServer.CoreFeatures.Users.Services
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

        public virtual User? Get(int id)
        {
            Entities.User? entity = _DBContext.User.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateUserModel(entity);
        }

        public virtual User Create(User user)
        {
            Entities.User entity = HydrateUserEntity(user);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateUserModel(entity);
        }

        public virtual User Update(User user)
        {
            Entities.User entity = HydrateUserEntity(user);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateUserModel(entity);
        }

        public virtual void Delete(User user)
        {
            Entities.User entity = HydrateUserEntity(user);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<User> HydrateUserModels(List<Entities.User> entities, int depth = 3)
        {
            List<User> models = new();

            entities.ForEach(entity =>
            {
                User model = HydrateUserModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private User HydrateUserModel(Entities.User user, int depth = 3)
        {
            return (User)_Hydrator.Hydrate(
                user, typeof(User), depth
            );
        }

        private Entities.User HydrateUserEntity(User user, int depth = 3)
        {
            return (Entities.User)_Hydrator.Hydrate(
                user, typeof(Entities.User), depth
            );
        }
    }
}
