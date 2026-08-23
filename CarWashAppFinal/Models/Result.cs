namespace CarWash.Models
{
    public class Result
    {
        public Result(bool isSuccess, string? error)
        {
            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        public bool IsSuccess { get; set; }

        public string? Error { get; set; }
    }
}
