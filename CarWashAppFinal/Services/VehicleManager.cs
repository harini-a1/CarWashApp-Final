using System.Timers;
using CarWashAppFinal.Enums;
using CarWashAppFinal.Models;
using CarWashAppFinal.Repositories;
namespace CarWashAppFinal.Services
{
    ///public delegate void Notify();
    public class VehicleManager
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleManager(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public void Add(Guid userId, string name, string number)
        {
            Vehicle vehicle = new Vehicle(userId, name, number);
            _vehicleRepository.Add(vehicle);
        }

        public IReadOnlyList<Vehicle> GetAll(Guid userId)
        {
            return this._vehicleRepository.GetAll(userId);
        }
    }
}
