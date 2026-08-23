using CarWashAppFinal.Models;

namespace CarWashAppFinal.Repositories
{
    /// <summary>
    /// Defines operations for managing user data.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user to register.</param>
        void Register(User user);

        /// <summary>
        /// Retrieves a user by username for authentication.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The matching user, or <c>null</c> if not found.</returns>
        User? Login (string username);

        /// <summary>
        /// Saves all users to persistent storage.
        /// </summary>
        void Save();

        /// <summary>
        /// Loads users from persistent storage.
        /// </summary>
        /// <returns>A list of users.</returns>
        List<User> Load();
    }
}
