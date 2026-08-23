using CarWash.Helpers;
using CarWash.Models;
using CarWash.Repositories;

namespace CarWash.Services
{
    public class UserManager
    {
        private readonly UserRepository _repository;
        private static Guid? _currentUserId;
        public UserManager(UserRepository userRepository)
        {
            _repository = userRepository;
        }

        public void Register(string username, string password, string phone, string email)
        {
            User user = new User(username, password, phone, email);
            _repository.Register(user);
        }

        public User? Login(string username, string password)
        {
            User? user = _repository.Login(username);
            if (user != null && user.VerifyPassword(password))
            {
                _currentUserId = user.Id;
                return user;
            }

            return null;
        }

        public Guid? GetCurrentLoggedinUserId()
        {
            return _currentUserId;
        }

        public void ResetCurrentUser()
        {
            _currentUserId = null;
        }
    }
}
