using CarWashAppFinal.Enums;
using CarWashAppFinal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarWashAppFinal.Repositories
{
    /// <summary>
    /// Defines operations for managing vehicle data.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        void Add(Vehicle vehicle);

        /// <summary>
        /// Retrieves all vehicles belonging to the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A read-only list of the user's vehicles.</returns>
        IReadOnlyList<Vehicle> GetAll(Guid userId);

        /// <summary>
        /// Retrieves a vehicle by its display ID for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="id">The vehicle's display ID.</param>
        /// <returns>The matching vehicle, or <c>null</c> if not found.</returns>
        Vehicle? Get(Guid userId, int id);

        /// <summary>
        /// Updates the status, notification state, and wash completion time of a vehicle.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <param name="type">The updated status of the vehicle.</param>
        /// <param name="isNotified">Indicates whether the user has been notified.</param>
        /// <param name="completionTime">The wash completion time.</param>
        void Update(Guid id, StatusType type, bool isNotified, DateTime completionTime);

        /// <summary>
        /// Saves all vehicles to persistent storage.
        /// </summary>
        void Save();

        /// <summary>
        /// Loads all vehicles from persistent storage.
        /// </summary>
        /// <returns>A list containing all stored vehicles.</returns>
        List<Vehicle> Load();
    }
}
