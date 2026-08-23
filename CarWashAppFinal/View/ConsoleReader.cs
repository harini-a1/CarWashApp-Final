using CarWashAppFinal.Enums;
using CarWashAppFinal.Helpers;
using CarWashAppFinal.Models;
using CarWashAppFinal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashAppFinal.View
{
    public delegate Result IdValidatorDelegate(string? input, int maxCount, out int parsedId);

    /// <summary>
    /// Handles user input and validation for the console interface.
    /// </summary>
    public class ConsoleReader
    {
        private const int MaxAttempts = 3;
        private readonly UserManager _userManager;
        private readonly VehicleManager _vehicleManager;
        private readonly ConsoleUI _consoleUI;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleReader"/> class.
        /// </summary>
        /// <param name="userManager">The manager used to handle user operations.</param>
        /// <param name="vehicleManager">The manager used to handle vehicle operations.</param>
        /// <param name="consoleUI">The console UI used to display application information.</param>
        public ConsoleReader(UserManager userManager, VehicleManager vehicleManager, ConsoleUI consoleUI)
        {
            _userManager = userManager;
            _vehicleManager = vehicleManager;
            _consoleUI = consoleUI;
        }

        /// <summary>
        /// Reads and validates user input using the specified validator.
        /// </summary>
        /// <param name="prompt">The prompt displayed to the user.</param>
        /// <param name="validator">The function used to validate the input.</param>
        /// <returns>The valid input, or <c>null</c> if input is cancelled or validation fails.</returns>
        public static string? Get(string prompt, Func<string?, Result> validator)
        {
            for (int attempts = 1; attempts <= MaxAttempts; attempts++)
            {
                string? input;
                if (validator.Method.Name == nameof(Validators.IsValidPassword)
                 || validator.Method.Name == nameof(Validators.IsValidLoginPassword))
                {
                    input = ReadInput(prompt, true);
                }
                else
                {
                    input = ReadInput(prompt, false);
                }
                if (input == null)
                {
                    return null;
                }

                Result result = validator(input);
                if (result.IsSuccess)
                {
                    return input;
                }

                Console.WriteLine("Invalid input");
                Console.WriteLine(result.Error);
                Console.WriteLine($"Attempts remaining: {MaxAttempts - attempts}");
            }

            ConsoleUI.ShowTooManyAttemptsMessage();
            return null;
        }

        /// <summary>
        /// Reads and validates a vehicle ID.
        /// </summary>
        /// <param name="prompt">The prompt displayed to the user.</param>
        /// <param name="maxCount">The maximum valid vehicle ID.</param>
        /// <param name="validator">The delegate used to validate the ID.</param>
        /// <returns>The valid vehicle ID, or <c>null</c> if input is cancelled.</returns>
        public static int? GetValidId(string prompt, int maxCount, IdValidatorDelegate validator)
        {
            string? input = ReadInput(prompt, false);
            if (input == null)
            {
                return null;
            }

            Result result = validator(input, maxCount, out int parsedId);
            if (result.IsSuccess)
            {
                return parsedId;
            }

            Console.WriteLine($"Error: {result.Error}");
            return 0;
        }

        /// <summary>
        /// Reads input from the console, optionally masking the characters for passwords.
        /// </summary>
        /// <param name="prompt">The prompt displayed to the user.</param>
        /// <param name="isPassword">Indicates whether the input should be masked.</param>
        /// <returns>The entered input, or <c>null</c> if the user presses Escape.</returns>
        public static string? ReadInput(string prompt, bool isPassword)
        {
            Console.Write(prompt);
            string input = string.Empty;
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    return null;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return input;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input[..^1];

                        Console.Write("\b \b");
                    }

                    continue;
                }

                if (char.IsControl(key.KeyChar))
                {
                    continue;
                }

                input += key.KeyChar;
                if (isPassword)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(key.KeyChar);
                }
            }
        }
    }
}
