using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Users.Users
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

        protected override DbSet<User> GetDbSet()
        {
            return _DBContext.User;
        }
    }
}
