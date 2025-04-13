
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;

namespace AgileStudioServerTest.CoreFeatures.Users.Users
{
    public class UserFixture : AbstractEntityFixture
    {
        public UserFixture(DBContext dbContext) : base(dbContext)
        {

        }

        public User Create(
            string? email = null,
            string? firstName = null,
            string? lastName = null)
        {
            firstName ??= "Test";
            lastName ??= "User";
            email ??= "testuser@local.agilestudio.dev";

            var user = new User(email, firstName, lastName);
            _DBContext.User.Add(user);
            _DBContext.SaveChanges();
            return user;
        }
    }
}
