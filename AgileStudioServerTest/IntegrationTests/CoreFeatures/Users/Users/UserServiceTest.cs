using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Users.Users
{
    public class UserServiceTest : AbstractServiceTest
    {
        private readonly UserService _userService;

        private readonly UserFixture _UserFixture;

        public UserServiceTest(
            DBContext dbContext,
            UserService userService,
            UserFixture userFixture) : base(dbContext)
        {
            _userService = userService;
            _UserFixture = userFixture;
        }

        [Fact]
        public void Create_ReturnsUser()
        {
            UserModel user = new("test@test.com", "Test", "User");

            user = _userService.Create(user);

            Assert.NotNull(user);
            Assert.True(user.ID > 0);
        }

        [Fact]
        public void Get_ReturnsUser()
        {
            var user = _UserFixture.Create();

            var returnedUser = _userService.Get(user.ID);

            Assert.NotNull(returnedUser);
            Assert.Equal(user.ID, returnedUser.ID);
        }

        [Fact]
        public void Update_ReturnsUpdatedUser()
        {
            var user = _UserFixture.Create();
            var email = $"test2@test.com";

            user.Email = email;
            user = _userService.Update(user);

            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void Delete_DeletesUser()
        {
            var user = _UserFixture.Create();

            _userService.Delete(user);

            user = _userService.Get(user.ID);
            Assert.Null(user);
        }
    }
}
