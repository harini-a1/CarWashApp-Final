using CarWash.Repositories;
using CarWash.Services;
using CarWash.View;

namespace CarWash
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();

            UserManager userManager = new UserManager(userRepository);
            VehicleRepository vehicleRepository = new VehicleRepository();
            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                userRepository.Save();
                vehicleRepository.Save();
            };
            Console.CancelKeyPress += (sender, e) =>
            {
                userRepository.Save();
                vehicleRepository.Save();
            };

            VehicleManager vehicleManager = new VehicleManager(userRepository, vehicleRepository);
            CarWashManager carWashManager = new CarWashManager(vehicleRepository, userManager);
            ConsoleUI consoleUI = new ConsoleUI(userManager, vehicleManager, carWashManager);
            ConsoleReader consoleReader = new ConsoleReader(userManager, vehicleManager, consoleUI);
            consoleUI.DisplayMenu();
        }
    }
}
