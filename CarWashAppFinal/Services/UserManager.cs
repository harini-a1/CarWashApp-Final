using CarWashAppFinal.Models;
using CarWashAppFinal.Repositories;

namespace CarWashAppFinal.Services
{
    /// <summary>
    /// Manages user registration, authentication, and login session state.
    /// </summary>
    public class UserManager
    {
        private readonly IUserRepository _repository;
        private static Guid? _currentUserId;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="userRepository">The repository used to manage user data.</param>
        public UserManager(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        /// <summary>
        /// Registers a new user with the specified details.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="phone">The phone number of the user.</param>
        /// <param name="email">The email address of the user.</param>
        public void Register(string username, string password, string phone, string email)
        {
            User user = new User(username, password, phone, email);
            _repository.Register(user);
        }

        /// <summary>
        /// Authenticates a user using the provided username and password.
        /// </summary>
        /// <param name="username">The username used for authentication.</param>
        /// <param name="password">The password used for authentication.</param>
        /// <returns>The authenticated user, or <c>null</c> if authentication fails.</returns>
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

        /// <summary>
        /// Gets the unique identifier of the currently logged-in user.
        /// </summary>
        /// <returns>The current user's identifier, or <c>null</c> if no user is logged in.</returns>
        public Guid? GetCurrentLoggedinUserId()
        {
            return _currentUserId;
        }

        /// <summary>
        /// Clears the current logged-in user's session.
        /// </summary>
        public void ResetCurrentUser()
        {
            _currentUserId = null;
        }
    }
}
