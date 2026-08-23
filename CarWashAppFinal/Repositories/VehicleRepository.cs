using CarWashAppFinal.Enums;
using CarWashAppFinal.Models;
using CarWashAppFinal.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace CarWashAppFinal.Repositories
{
    /// <summary>
    /// Defines operations for managing vehicle data.
    /// </summary>
    public class VehicleRepository : IVehicleRepository
    {
        private readonly string _filePath;
        private readonly List<Vehicle> _vehicles;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class
        /// </summary>
        public VehicleRepository()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "vehicles.json");
            _vehicles = this.Load();
        }

        /// <inheritdoc/>
        public void Add(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);
        }

        /// <inheritdoc/>
        public IReadOnlyList<Vehicle> GetAll(Guid userId)
        {
            return _vehicles.Where(v => v.UserId == userId).ToList();
        }

        /// <inheritdoc/>
        public Vehicle? Get(Guid userId, int id)
        {
            return this.GetAll(userId)[id - 1];
        }

        /// <inheritdoc/>
        public void Update(Guid id, StatusType type, bool isNotified, DateTime completionTime)
        {
            Vehicle? vehicle = _vehicles.FirstOrDefault(v => v.Id  == id);
            vehicle?.Update(type, isNotified, completionTime);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
