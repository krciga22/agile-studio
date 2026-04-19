using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;

namespace AgileStudioServerTest.Features.Users.Users
{
    public class UserFixture : AbstractEntityFixture<UserRepository>
    {
        public UserFixture(UserRepository userRepository) : base(userRepository)
        {

        }

        public UserModel Create(
            string? email = null,
            string? firstName = null,
            string? lastName = null)
        {
            firstName ??= "Test";
            lastName ??= "User";
            email ??= "testuser@local.agilestudio.dev";

            var user = new UserModel(email, firstName, lastName);

            return _Repository.Create(user);
        }

        public UserModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
