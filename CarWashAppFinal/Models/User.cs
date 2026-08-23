using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarWash.Models
{
    public class User
    {
        public User()
        {
        }
        public User(string username, string password, string phone, string email)
        {
            this.Id = Guid.NewGuid();
            this.Username = username;
            this.Password = password;
            this.Phone = phone;
            this.Email = email;
        }

        [JsonConstructor]
        public User(Guid id, string username, string password, string phone, string email)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Phone = phone;
            this.Email = email;
        }

        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Phone {  get; set; }

        public string Email { get; set; }


        public bool VerifyPassword(string password)
        {
            return Password == password;
        }
    }
}
