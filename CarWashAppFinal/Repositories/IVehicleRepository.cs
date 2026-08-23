using CarWashAppFinal.Enums;
using CarWashAppFinal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarWashAppFinal.Repositories
{
    public interface IVehicleRepository
    {
        void Add(Vehicle vehicle);
        IReadOnlyList<Vehicle> GetAll(Guid userId);
        Vehicle? Get(Guid userId, int id);
        void Update(Guid id, StatusType type, bool isNotified, DateTime completionTime);
        void Save();
        List<Vehicle> Load();
    }
}
