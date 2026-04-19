using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Features.Users.Users
{
    public class UserRepository : EntityRepository<DBContext, UserModel, User, int>
    {
        public UserRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {
        }

        public override int GetIdentifier(UserModel model)
        {
            return model.ID;
        }

        public UserModel? GetByEmail(string email)
        {
            DbSet<User> dbSet = GetDbSet();
            var user = dbSet.Select(e => e).Where(e => e.Email == email).FirstOrDefault();
            if (user is null){
                return null;
            }

            return HydrateModel(user);
        }

        public UserModel? GetByAuthServerUserId(string authServerUserId)
        {
            DbSet<User> dbSet = GetDbSet();
            var user = dbSet.Select(e => e)
                .Where(e => e.AuthServerUserID == authServerUserId)
                .FirstOrDefault();

            if (user is null){
                return null;
            }

            return HydrateModel(user);
        }

        protected override DbSet<User> GetDbSet()
        {
            return _DBContext.User;
        }
    }
}
