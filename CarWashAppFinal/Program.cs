using CarWashAppFinal.Repositories;
using CarWashAppFinal.Services;
using CarWashAppFinal.View;

namespace CarWashAppFinal
{
    /// <summary>
    /// Program class to start app
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Any argument</param>
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
