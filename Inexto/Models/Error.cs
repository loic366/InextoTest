namespace Inexto.Models
{
    /// <summary>
    /// Model for basic error
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="Code"></param>
    /// <param name="Exception"></param>
    public class Error
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
        public Exception? Exception { get; set; }

        public Error()
        {

        }

        public Error(string? message)
        {
            this.Message = message;
        }

        public Error(string? message, string? code)
        {
            this.Message = message;
            this.Code = code;
        }

        public Error(string? message, Exception? exception)
        {
            this.Message = message;
            this.Exception = exception;
        }

        public Error(string? message, string? code, Exception? exception)
        {
            this.Message = message;
            this.Code = code;
            this.Exception = exception;
        }
    }
}
