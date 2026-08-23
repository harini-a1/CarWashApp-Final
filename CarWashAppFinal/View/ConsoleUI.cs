using CarWashAppFinal.Enums;
using CarWashAppFinal.Helpers;
using CarWashAppFinal.Models;
using CarWashAppFinal.Repositories;
using CarWashAppFinal.Services;

namespace CarWashAppFinal.View
{
    /// <summary>
    /// Provides the console-based user interface for the car wash application.
    /// </summary>
    public class ConsoleUI
    {
        private readonly UserManager _userManager;
        private readonly VehicleManager _vehicleManager;
        private readonly CarWashManager _carWashManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleUI"/> class.
        /// </summary>
        /// <param name="userManager">The manager used to handle user operations.</param>
        /// <param name="vehicleManager">The manager used to handle vehicle operations.</param>
        /// <param name="carWashManager">The manager used to handle car wash operations.</param>
        public ConsoleUI(UserManager userManager, VehicleManager vehicleManager, CarWashManager carWashManager)
        {
            _userManager = userManager;
            _vehicleManager = vehicleManager;
            _carWashManager = carWashManager;
        }

        /// <summary>
        /// Displays the main menu and handles user selections.
        /// </summary>
        public void DisplayMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------CAR WASH----------");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Enter your choice: ");
                string? inputChoice = Console.ReadLine();
                if (Enum.TryParse(inputChoice, out MainMenuOptions choiceEnum)
                 && Enum.IsDefined(choiceEnum))
                {
                    switch (choiceEnum)
                    {
                        case MainMenuOptions.Register:
                            this.Register();
                            break;
                        case MainMenuOptions.Login:
                            this.Login();
                            break;
                        case MainMenuOptions.Exit:
                            DisplayExitMessage();
                            return;
                    }
                }
                else
                {
                    DisplayInvalidMessage();
                }
            }
        }

        /// <summary>
        /// Handles the user registration process.
        /// </summary>
        public void Register()
        {
            string? username = ConsoleReader.Get("Enter username: ", Validators.IsValidUsername);
            if (username == null)
            {
                return;
            }

            string? password = ConsoleReader.Get("Enter password: ", Validators.IsValidPassword);
            if (password == null)
            {
                return;
            }
            
            string? phone = ConsoleReader.Get("Enter phone: ", Validators.IsValidPhone);
            if (phone == null)
            {
                return;
            }

            string? email = ConsoleReader.Get("Enter email: ", Validators.IsValidEmail);
            if (email == null)
            {
                return;
            }
             
            this._userManager.Register(username, password, phone, email);
            Console.WriteLine("Registered successfully");
            Pause();
        }

        /// <summary>
        /// Handles the user login process.
        /// </summary>
        public void Login()
        {
            string? username = ConsoleReader.Get("Enter username: ", Validators.IsValid);
            if (username == null)
            {
                return;
            }

            string? password = ConsoleReader.Get("Enter password: ", Validators.IsValidLoginPassword);
            if (password == null)
            {
                return;
            }

            User? user = this._userManager.Login(username, password);
            if (user == null)
            {
                Console.WriteLine("Invalid username or password");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Logged in. Welcome {user.Username}");
            this.DisplayUserMenu(user.Id);
        }

        // <summary>
        /// Displays the menu available to a logged-in user.
        /// </summary>
        /// <param name="userId">The unique identifier of the logged-in user.</param>
        public void DisplayUserMenu(Guid userId)
        {
            this.TriggerMissedNotifications(userId);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1, Add vehicles");
                Console.WriteLine("2. View vehicles");
                Console.WriteLine("3. Add car for Water wash");
                Console.WriteLine("4. History of service information");
                Console.WriteLine("5. Notifications");
                Console.WriteLine("6. Logout");
                Console.WriteLine("Enter your choice: ");
                string? inputChoice = Console.ReadLine();
                if (Enum.TryParse(inputChoice, out UserMenuOptions choiceEnum)
                 && Enum.IsDefined(choiceEnum))
                {
                    switch (choiceEnum)
                    {
                        case UserMenuOptions.Add:
                            this.Add(userId);
                            break;
                        case UserMenuOptions.View:
                            this.View(userId);
                            Pause();
                            break;
                        case UserMenuOptions.Wash:
                            this.Wash(userId);
                            break;
                        case UserMenuOptions.History:
                            this.ViewHistory(userId);
                            break;
                        case UserMenuOptions.Notification:
                            this.Notification(userId);
                            break;
                        case UserMenuOptions.Logout:
                            this._userManager.ResetCurrentUser();
                            return;
                    }
                }
            }
        }

        /// <summary>
        /// Adds a new vehicle for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        public void Add(Guid userId)
        {
            string? name = ConsoleReader.Get("Enter vehicle name: ", Validators.IsValidVehicleName);
            if (name == null)
            {
                return;
            }

            string? number = ConsoleReader.Get("Enter vehicle number: ", Validators.IsValidVehicleNumber);
            if (number == null)
            {
                return;
            }

            this._vehicleManager.Add(userId, name, number);
            Console.WriteLine("Added successfully");
            Pause();
        }

        /// <summary>
        /// Displays all vehicles belonging to the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        public void View(Guid userId)
        {
            IReadOnlyList<Vehicle> vehicles = this._vehicleManager.GetAll(userId);
            DisplayVehicles(vehicles);
            Pause();
        }

        /// <summary>
        /// Starts a car wash for the vehicle selected by the user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        public void Wash(Guid userId)
        {
            IReadOnlyList<Vehicle> vehicles = this._vehicleManager.GetAll(userId);
            if (vehicles.Count == 0)
            {
                Console.WriteLine("No vehicles found");
                return;
            }

            DisplayVehicles(vehicles);
            Console.WriteLine("Enter index of the Vehicle for wash: ");
            int? index = ConsoleReader.GetValidId("Enter index: ", this._vehicleManager.GetAll(userId).Count, Validators.IsValidId);
            if (index == null || index == 0)
            {
                return;
            }

            this._carWashManager.Wash(vehicles[index.Value - 1]);
            Console.WriteLine("Sending to wash...");
        }

        /// <summary>
        /// Displays the completed vehicle wash history for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        public void ViewHistory(Guid userId)
        {
            IReadOnlyList<Vehicle> servicedVehicles = this._vehicleManager
                .GetAll(userId)
                .Where(v => v.IsNotified && v.Status == StatusType.Completed)
                .ToList();
            DisplayVehicles(servicedVehicles);
            Pause();
        }

        /// <summary>
        /// Displays the pending notifications for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        public void Notification(Guid userId)
        {
            IReadOnlyList<Vehicle> unnotifiedVehicles = this._vehicleManager
                .GetAll(userId)
                .Where(v => !v.IsNotified && v.Status == StatusType.Completed)
                .ToList();
            DisplayVehicles(unnotifiedVehicles);
            Pause();
        }

        /// <summary>
        /// Displays notifications for washes that were completed while the user was logged out.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        public void TriggerMissedNotifications(Guid userId)
        {
            IReadOnlyList<Vehicle>? unnotifiedVehicles = this._vehicleManager
               .GetAll(userId)?
               .Where(v => !v.IsNotified && v.Status == StatusType.Completed)
               .ToList();
            if (unnotifiedVehicles == null)
            {
                return;
            }

            foreach (Vehicle vehicle in unnotifiedVehicles)
            {
                Console.WriteLine($"Your vehicle wash was completed at: {vehicle.WashCompletionTime}");
            }
            Pause();
        }

        /// <summary>
        /// Displays the details of the specified vehicles.
        /// </summary>
        /// <param name="vehicles">The vehicles to display.</param>
        public static void DisplayVehicles(IReadOnlyList<Vehicle> vehicles)
        {
            int index = 1;
            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine($"Index: {index++}");
                Console.WriteLine($"Name: {vehicle.Name}");
                Console.WriteLine($"Number: {vehicle.Number}");
                if (vehicle.Status == StatusType.Completed)
                {
                    Console.WriteLine($"Completion Time: {vehicle.WashCompletionTime}");
                }

                Console.WriteLine("---");
            }

            if (index == 1)
            {
                Console.WriteLine("No vehicles found.");
            }
        }

        /// <summary>
        /// Displays a message when the application is exiting.
        /// </summary>
        public static void DisplayExitMessage()
        {
            Console.WriteLine("Exiting...");
        }

        /// <summary>
        /// Displays a message indicating that the entered input is invalid.
        /// </summary>
        public static void DisplayInvalidMessage()
        {
            Console.WriteLine("Invalid input.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays a message when the maximum number of input attempts is exceeded.
        /// </summary>
        public static void ShowTooManyAttemptsMessage()
        {
            Console.WriteLine("Too many invalid attempts.");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }

        /// <summary>
        /// Pauses the console until the user presses a key.
        /// </summary>
        public static void Pause()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays a message indicating that all car wash slots are occupied.
        /// </summary>
        public static void DisplaySlotsUnavailable()
        {
            Console.WriteLine("The current slots are filled. Status: Waiting.");
            Console.WriteLine("You will be notified upon completion.");
            Pause();
        }

        /// <summary>
        /// Displays a notification indicating that the user's car wash is completed and ready for pickup.
        /// </summary>
        public static void DisplayCarReady()
        {
            Console.WriteLine("Your car has been washed and ready for pickup");
        }
    }
}
