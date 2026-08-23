using CarWashAppFinal.Enums;
using System.Text.Json.Serialization;

namespace CarWashAppFinal.Models
{
    /// <summary>
    /// Represents a vehicle registered in the car wash application.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Initializes a new vehicle for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the vehicle owner.</param>
        /// <param name="name">The name of the vehicle.</param>
        /// <param name="number">The vehicle registration number.</param>
        public Vehicle(Guid userId, string name, string number)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId;
            this.Name = name;
            this.Number = number;
        }

        /// <summary>
        /// Initializes a vehicle with existing details for data restoration.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <param name="userId">The unique identifier of the vehicle owner.</param>
        /// <param name="name">The name of the vehicle.</param>
        /// <param name="number">The vehicle registration number.</param>
        /// <param name="status">The current status of the vehicle.</param>
        /// <param name="isNotified">Indicates whether the user has been notified.</param>
        /// <param name="washCompletionTime">The expected or actual wash completion time.</param>
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

        /// <summary>
        /// Gets or sets the unique identifier of the vehicle owner.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the vehicle.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the vehicle name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the vehicle number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public StatusType Status { get; set; } = StatusType.Available;

        /// <summary>
        /// Gets or sets a value indicating whether a notification is pending.
        /// </summary>
        public bool IsNotified { get; set; } = false;

        /// <summary>
        /// Gets or sets the wash completion time.
        /// </summary>
        public DateTime? WashCompletionTime { get; set; }

        /// <summary>
        /// Updates the vehicle's wash status, notification status, and completion time.
        /// </summary>
        /// <param name="status">The updated vehicle status.</param>
        /// <param name="isNotified">The updated notification status.</param>
        /// <param name="washCompletionTime">The updated wash completion time.</param>
        public void Update(StatusType status , bool isNotified, DateTime washCompletionTime)
        {
            this.Status = status;
            this.IsNotified = isNotified;
            this.WashCompletionTime = washCompletionTime;
        }
    }
}
