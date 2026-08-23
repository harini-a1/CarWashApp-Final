namespace CarWashAppFinal.Models
{
    /// <summary>
    /// Represents the result of a validation or operation.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isSuccess">Indicates whether the operation was successful.</param>
        /// <param name="error">The error message if the operation failed.</param>
        public Result(bool isSuccess, string? error)
        {
            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string? Error { get; set; }
    }
}
