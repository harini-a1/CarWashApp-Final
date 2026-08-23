using CarWashAppFinal.Enums;
using CarWashAppFinal.Models;
using CarWashAppFinal.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace CarWashAppFinal.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly string _filePath;
        private readonly List<Vehicle> _vehicles;
        public VehicleRepository()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "vehicles.json");
            _vehicles = this.Load();
        }

        public void Add(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);
        }

        public IReadOnlyList<Vehicle> GetAll(Guid userId)
        {
            return _vehicles.Where(v => v.UserId == userId).ToList();
        }

        public Vehicle? Get(Guid userId, int id)
        {
            return this.GetAll(userId)[id - 1];
        }

        public void Update(Guid id, StatusType type, bool isNotified, DateTime completionTime)
        {
            Vehicle? vehicle = _vehicles.FirstOrDefault(v => v.Id  == id);
            vehicle?.Update(type, isNotified, completionTime);
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(
                _vehicles,
                new JsonSerializerOptions
                {
                    Converters = 
                    {
                        new JsonStringEnumConverter()
                    },
                    WriteIndented = true
                });
            File.WriteAllText(_filePath, json);
        }

        public List<Vehicle> Load()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Vehicle>();
            }
            var options = new JsonSerializerOptions
            {
                Converters =
                    {
                        new JsonStringEnumConverter()
                    }
            };
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Vehicle>>(json, options) ?? new List<Vehicle>();
        } 
    }
}
