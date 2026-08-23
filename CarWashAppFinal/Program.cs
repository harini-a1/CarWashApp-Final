using CarWashAppFinal.Repositories;
using CarWashAppFinal.Services;
using CarWashAppFinal.View;

namespace CarWashAppFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserRepository userRepository = new UserRepository();

            UserManager userManager = new UserManager(userRepository);
            IVehicleRepository vehicleRepository = new VehicleRepository();
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

            VehicleManager vehicleManager = new VehicleManager(vehicleRepository);
            CarWashManager carWashManager = new CarWashManager(vehicleRepository, userManager);
            ConsoleUI consoleUI = new ConsoleUI(userManager, vehicleManager, carWashManager);
            ConsoleReader consoleReader = new ConsoleReader(userManager, vehicleManager, consoleUI);
            consoleUI.DisplayMenu();
        }
    }
}
