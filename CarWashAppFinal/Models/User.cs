using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarWashAppFinal.Models
{
    public class User
    {
        /// <summary>
        /// Initializes a new user with the specified details.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="phone">The user's phone number.</param>
        /// <param name="email">The user's email address.</param>
        public User(string username, string password, string phone, string email)
        {
            this.Id = Guid.NewGuid();
            this.Username = username;
            this.Password = password;
            this.Phone = phone;
            this.Email = email;
        }

        /// <summary>
        /// Initializes a user with existing details for JSON deserialization.
        /// </summary>
        /// <param name="id">The user's unique identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="phone">The user's phone number.</param>
        /// <param name="email">The user's email address.</param>
        [JsonConstructor]
        public User(Guid id, string username, string password, string phone, string email)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Phone = phone;
            this.Email = email;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        public string Phone {  get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Verifies whether the specified password matches the user's password.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <returns><c>true</c> if the password matches; otherwise, <c>false</c>.</returns>
        public bool VerifyPassword(string password)
        {
            return Password == password;
        }
    }
}
