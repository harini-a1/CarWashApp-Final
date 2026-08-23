using CarWash.Enums;
using CarWash.Helpers;
using CarWash.Models;
using CarWash.Repositories;
using CarWash.Services;

namespace CarWash.View
{
    public class ConsoleUI
    {
        private readonly UserManager _userManager;
        private readonly VehicleManager _vehicleManager;
        private readonly CarWashManager _carWashManager;
        public ConsoleUI(UserManager userManager, VehicleManager vehicleManager, CarWashManager carWashManager)
        {
            _userManager = userManager;
            _vehicleManager = vehicleManager;
            _carWashManager = carWashManager;
        }

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

        public void View(Guid userId)
        {
            IReadOnlyList<Vehicle> vehicles = this._vehicleManager.GetAll(userId);
            DisplayVehicles(vehicles);
            Pause();
        }

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

        public void ViewHistory(Guid userId)
        {
            IReadOnlyList<Vehicle> servicedVehicles = this._vehicleManager
                .GetAll(userId)
                .Where(v => v.IsNotified && v.Status == StatusType.Completed)
                .ToList();
            DisplayVehicles(servicedVehicles);
            Pause();
        }

        public void Notification(Guid userId)
        {
            IReadOnlyList<Vehicle> unnotifiedVehicles = this._vehicleManager
                .GetAll(userId)
                .Where(v => !v.IsNotified && v.Status == StatusType.Completed)
                .ToList();
            DisplayVehicles(unnotifiedVehicles);
            Pause();
        }

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
        public static void DisplayExitMessage()
        {
            Console.WriteLine("Exiting...");
        }

        public static void DisplayInvalidMessage()
        {
            Console.WriteLine("Invalid input.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void ShowTooManyAttemptsMessage()
        {
            Console.WriteLine("Too many invalid attempts.");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }

        public static void Pause()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public static void DisplaySlotsUnavailable()
        {
            Console.WriteLine("The current slots are filled. Status: Waiting.");
            Console.WriteLine("You will be notified upon completion.");
            Pause();
        }

        public static void DisplayCarReady()
        {
            Console.WriteLine("Your car has been washed and ready for pickup");
        }
    }
}
