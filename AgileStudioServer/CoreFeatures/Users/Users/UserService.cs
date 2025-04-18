
namespace AgileStudioServer.CoreFeatures.Users.Users
{
    public class UserService
    {
        private readonly UserRepository _UserRepository;

        public UserService(UserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public virtual UserModel? Get(int id)
        {
            return _UserRepository.Get(id);
        }

        public virtual UserModel Create(UserModel user)
        {
            return _UserRepository.Create(user);
        }

        public virtual UserModel Update(UserModel user)
        {
            return _UserRepository.Update(user);
        }

        public virtual void Delete(UserModel user)
        {
            _UserRepository.Delete(user);
        }
    }
}
