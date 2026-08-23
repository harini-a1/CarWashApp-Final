using CarWashAppFinal.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CarWashAppFinal.Helpers
{
    /// <summary>
    /// Provides validation methods for user, vehicle, and input data.
    /// </summary>
    public class Validators
    {
        /// <summary>
        /// Validates a vehicle ID against the available vehicle count.
        /// </summary>
        /// <param name="id">The vehicle ID provided as a string.</param>
        /// <param name="countOfVehicles">The total number of vehicles available.</param>
        /// <param name="idNumber">The parsed vehicle ID number.</param>
        /// <returns>A <see cref="Result"/> indicating whether the ID is valid.</returns>
        public static Result IsValidId(string? id, int countOfVehicles, out int idNumber)
        {
            idNumber = 0;
            if (string.IsNullOrWhiteSpace(id))
            {
                return new(false, "ID cannot be empty.");
            }
                

            if (!int.TryParse(id, out idNumber))
            {
                return new(false, "ID must be a valid number.");
            }
                

            if (idNumber <= 0 || idNumber > countOfVehicles)
            {
                idNumber = 0;
                return new(false, $"ID must be greater than 0 and less than Total Vehicle count => {countOfVehicles}");
            }

            return new(true, null);
        }

        /// <summary>
        /// Validates a username according to the required length and character rules.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <returns>A <see cref="Result"/> indicating whether the username is valid.</returns>
        public static Result IsValidUsername(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new(false, "Username cannot be empty");
            }

            if (username.Length <3 || username.Length > 20)
            {
                return new(false, "Username should have a length between 3 to 20(inclusive)");
            }

            if (!char.IsLetter(username[0]) || !char.IsLetterOrDigit(username[^1]))
            {
                return new(false, "Username should start with a letter and end with either a letter or a digit");
            }

            if (!username.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '-'))
            {
                return new(false, "Username should only contain letter, digit, -, or _");
            }

            return new(true, null);
        }

        /// <summary>
        /// Validates a password based on length and character requirements.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>A <see cref="Result"/> indicating whether the password is valid.</returns>
        public static Result IsValidPassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return new(false, "Password cannot be empty");
            }

            if (password.Length < 8 || password.Length > 20)
            {
                return new(false, "Password should have a length between 8 to 20(inclusive)");
            }

            if (!password.Any(char.IsUpper)
             || !password.Any(char.IsLower)
             || !password.Any(char.IsDigit)
             || !password.Any(c => !char.IsLetterOrDigit(c)))
            {
                return new(false, "Password should contain atleast one digit, one upper case, one lower case " +
                    "and one special character");
            }

            return new(true, null);
        }

        /// <summary>
        /// Validates a phone number to ensure it contains exactly ten digits.
        /// </summary>
        /// <param name="phone">The phone number to validate.</param>
        /// <returns>A <see cref="Result"/> indicating whether the phone number is valid.</returns>
        public static Result IsValidPhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return new(false, "Phone number cannot be empty.");
            }

            if (phone.Length != 10)
            {
                return new(false, "Phone number should contain 10 digits");
            }

            if (!phone.All(p => char.IsDigit(p)))
            {
                return new(false, "Phone number should contain only digits");
            }

            return new(true, null);
        }

        /// <summary>
        /// Validates an email address using email address formatting rules.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>A <see cref="Result"/> indicating whether the email address is valid.</returns>
        public static Result IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new(false, "Email cannot be empty.");
            }

            try
            {
                MailAddress mailAddress = new MailAddress(email);
                if (mailAddress.Address == email)
                {
                    return new(true, null);
                }
                else
                {
                    return new(false, "Email should contain valid '@' symbol and domain part");
                }
            }
            catch (FormatException)
            {
                return new(false, "Format of the email is not valid");
            }
        }

        /// <summary>
        /// Validates that the provided input is not empty or whitespace.
        /// </summary>
        /// <param name="input">The input value to validate.</param>
        /// <returns>A <see cref="Result"/> indicating whether the input is valid.</returns>
        public static Result IsValid(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new(false, " Username cannot be empty.");
            }

            return new(true, null);
        }

        // <summary>
        /// Validates that the login password is not empty or whitespace.
        /// </summary>
        /// <param name="input">The password input to validate.</param>
        /// <returns>A <see cref="Result"/> indicating whether the password is valid.</returns>
        public static Result IsValidLoginPassword(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new(false, "Password cannot be empty.");
            }

            return new(true, null);
        }

        /// <summary>
        /// Validates a vehicle registration number against the required format.
        /// </summary>
        /// <param name="vehicleNumber">The vehicle registration number to validate.</param>
        /// <returns>A <see cref="Result"/> indicating whether the vehicle number is valid.</returns>
        public static Result IsValidVehicleNumber(string? vehicleNumber)
        {
            if (string.IsNullOrWhiteSpace(vehicleNumber))
            {
                return new(false, "Vehicle number cannot be empty");
            }

            string pattern = @"^[A-Za-z]{2}\d{2}[A-Za-z]{2}\d{4}$";
            if (!Regex.IsMatch(vehicleNumber, pattern))
            {
                return new(false, "Vehicle number should be in the format of 2 char, 2 digit, 2 char, 4 digits. Eg: TN02AB0978");
            }

            return new(true, null);
        }

        /// <summary>
        /// Validates a vehicle name based on the required length.
        /// </summary>
        /// <param name="vehicleName">The vehicle name to validate.</param>
        /// <returns>A <see cref="Result"/> indicating whether the vehicle name is valid.</returns>
        public static Result IsValidVehicleName(string? vehicleName)
        {
            if (string.IsNullOrWhiteSpace(vehicleName))
            {
                return new(false, "Vehicle name cannot be empty");
            }

            if (vehicleName.Length < 3 || vehicleName.Length > 20)
            {
                return new(false, "Vehicle name should have a length between 8 to 20(inclusive)");
            }

            return new(true, null);
        }
    }
}
