using System.Text.Json;
using CarWash.Models;

namespace CarWash.Repositories
{
    public class UserRepository
    {
        private readonly string _filePath;
        private readonly List<User> _users;
        public UserRepository()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "users.json");
            _users = Load();
        }

        public void Register(User user)
        {
            _users.Add(user);
        }

        public User? Login(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

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
