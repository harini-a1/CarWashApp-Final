using System.Timers;
using CarWashAppFinal.Enums;
using CarWashAppFinal.Models;
using CarWashAppFinal.Repositories;
namespace CarWashAppFinal.Services
{
    /// <summary>
    /// Manages vehicle operations for users.
    /// </summary>
    public class VehicleManager
    {
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleManager"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The repository used to manage vehicle data.</param>
        public VehicleManager(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Adds a new vehicle for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the vehicle owner.</param>
        /// <param name="name">The name of the vehicle.</param>
        /// <param name="number">The registration number of the vehicle.</param>
        public void Add(Guid userId, string name, string number)
        {
            Vehicle vehicle = new Vehicle(userId, name, number);
            _vehicleRepository.Add(vehicle);
        }

        /// <summary>
        /// Retrieves all vehicles belonging to the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the vehicle owner.</param>
        /// <returns>A read-only list of vehicles belonging to the user.</returns>
        public IReadOnlyList<Vehicle> GetAll(Guid userId)
        {
            return this._vehicleRepository.GetAll(userId);
        }
    }
}
