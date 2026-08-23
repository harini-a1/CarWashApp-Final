using System.Text.Json;
using CarWashAppFinal.Models;
using CarWashAppFinal.Repositories;

namespace CarWashAppFinal.Repositories
{
    /// <summary>
    /// Defines operations for managing user data.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath;
        private readonly List<User> _users;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class
        /// </summary>
        public UserRepository()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "users.json");
            _users = Load();
        }

        /// <inheritdoc/>
        public void Register(User user)
        {
            _users.Add(user);
        }

        /// <inheritdoc/>
        public User? Login(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        /// <inheritdoc/>
        public void Save()
        {
            string json = JsonSerializer.Serialize(
                _users,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                });
            File.WriteAllText( _filePath, json );
        }

        /// <inheritdoc/>
        public List<User> Load()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
    }
}
