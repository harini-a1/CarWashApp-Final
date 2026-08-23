using CarWash.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CarWash.Helpers
{
    public class Validators
    {
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

        public static Result IsValid(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new(false, " Username cannot be empty.");
            }

            return new(true, null);
        }

        public static Result IsValidLoginPassword(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new(false, "Password cannot be empty.");
            }

            return new(true, null);
        }

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
