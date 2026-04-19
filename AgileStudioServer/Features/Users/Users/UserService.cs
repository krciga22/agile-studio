using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Features.Users.Users
{
    public class UserService : AbstractService
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

        public virtual UserModel? GetByEmail(string email)
        {
            return _UserRepository.GetByEmail(email);
        }

        public virtual UserModel? GetByAuthServerUserId(string authServerUserId)
        {
            return _UserRepository.GetByAuthServerUserId(authServerUserId);
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
