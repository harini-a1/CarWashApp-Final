using CarWash.Enums;
using CarWash.Models;
using CarWash.Repositories;
using CarWash.View;
using System.Timers;

namespace CarWash.Services
{
    public class CarWashManager
    {
        private readonly VehicleRepository _vehicleRepository;
        private readonly UserManager _userManager;
        private readonly Dictionary<Guid, DateTime> _activeWashes = new();
        public CarWashManager(VehicleRepository vehicleRepository, UserManager userManager)
        {
            _vehicleRepository = vehicleRepository;
            _userManager = userManager;
        }

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
