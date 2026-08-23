using CarWashAppFinal.Enums;
using System.Text.Json.Serialization;

namespace CarWashAppFinal.Models
{
    public class Vehicle
    {
        public Vehicle()
        {
        }
        public Vehicle(Guid userId, string name, string number)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId;
            this.Name = name;
            this.Number = number;
        }

        public Vehicle(
            Guid id,
            Guid userId,
            string name,
            string number,
            StatusType status,
            bool isNotified,
            DateTime washCompletionTime)
        {
            this.Id = id;
            this.UserId = userId;
            this.Name = name;
            this.Number = number;
            this.Status = status;
            this.IsNotified = isNotified;
            this.WashCompletionTime = washCompletionTime;
        }

        public Guid UserId { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public StatusType Status { get; set; } = StatusType.Available;

        public bool IsNotified { get; set; } = false;

        public DateTime? WashCompletionTime { get; set; }

        public void Update(StatusType status , bool isNotified, DateTime washCompletionTime)
        {
            this.Status = status;
            this.IsNotified = isNotified;
            this.WashCompletionTime = washCompletionTime;
        }
    }
}
