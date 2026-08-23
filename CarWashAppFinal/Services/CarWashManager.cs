using CarWashAppFinal.Enums;
using CarWashAppFinal.Models;
using CarWashAppFinal.Repositories;
using CarWashAppFinal.View;

namespace CarWashAppFinal.Services
{
    /// <summary>
    /// Manages vehicle car wash operations and active wash tracking.
    /// </summary>
    public class CarWashManager
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly UserManager _userManager;
        private readonly Dictionary<Guid, DateTime> _activeWashes = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CarWashManager"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The repository used to manage vehicle data.</param>
        /// <param name="userManager">The manager used to track the currently logged-in user.</param>
        public CarWashManager(IVehicleRepository vehicleRepository, UserManager userManager)
        {
            _vehicleRepository = vehicleRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// Starts a timer for the specified vehicle's car wash.
        /// </summary>
        /// <param name="vehicle">The vehicle undergoing the car wash.</param>
        public void WashTimer(Vehicle vehicle)
        {
            System.Timers.Timer timer = new System.Timers.Timer(30000);
            timer.Elapsed += (sender, e) =>
            {
                timer.Stop();
                timer.Dispose();
                CompleteWash(vehicle, DateTime.Now);
            };

            timer.AutoReset = false;
            timer.Start();
        }

        /// <summary>
        /// Completes the car wash for the specified vehicle and updates its status and notification state.
        /// </summary>
        /// <param name="vehicle">The vehicle whose wash has been completed.</param>
        /// <param name="completionTime">The time at which the car wash was completed.</param>
        public void CompleteWash(Vehicle vehicle, DateTime completionTime)
        {
            if (_userManager.GetCurrentLoggedinUserId() == vehicle.UserId)
            {
                ConsoleUI.DisplayCarReady();
                _vehicleRepository.Update(vehicle.Id, StatusType.Completed, true, completionTime);
            }
            else
            {
                _vehicleRepository.Update(vehicle.Id, StatusType.Completed, false, completionTime);
            }
            
            _activeWashes.Remove(vehicle.Id);
        }

        /// <summary>
        /// Starts a car wash for the specified vehicle if an active slot is available
        /// and the vehicle is not already being washed.
        /// </summary>
        /// <param name="vehicle">The vehicle to be washed.</param>
        public void Wash(Vehicle vehicle)
        {
            if (_activeWashes.Count >= 3)
            {
                ConsoleUI.DisplaySlotsUnavailable();
            }
            else
            {
                if (vehicle != null)
                {
                    if (_activeWashes.ContainsKey(vehicle.Id))
                    {
                        Console.WriteLine("This vehicle is already being washed.");
                        return;
                    }
                    else
                    {
                        _activeWashes.Add(vehicle.Id, DateTime.Now);
                    }

                    this.WashTimer(vehicle);
                }
            }
        }
    }
}
