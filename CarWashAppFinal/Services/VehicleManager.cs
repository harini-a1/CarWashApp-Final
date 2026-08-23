using System.Timers;
using CarWash.Enums;
using CarWash.Models;
using CarWash.Repositories;
namespace CarWash.Services
{
    ///public delegate void Notify();
    public class VehicleManager
    {
        private readonly VehicleRepository _vehicleRepository;
        public VehicleManager(UserRepository userRepository, VehicleRepository vehicleRepository)
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
