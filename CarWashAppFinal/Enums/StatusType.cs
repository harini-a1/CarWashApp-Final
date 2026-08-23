using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashAppFinal.Enums
{
    /// <summary>
    /// Represents the status type of the service
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// Indicates that the vehicle is available for a car wash.
        /// </summary>
        Available = 0,

        /// <summary>
        /// Indicates that the car wash has been completed.
        /// </summary>
        Completed = 2,
    }
}
