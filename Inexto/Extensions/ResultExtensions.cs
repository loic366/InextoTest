namespace Inexto.Extensions
{
    using Inexto.Models;
    using System.Text;

    /// <summary>
    /// Extensions methods for Result objects
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Retrieve all the error messages from a Result object
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string GetErrorMessages<E>(this Result<E> result) where E : Error
        {
            var sb = new StringBuilder();
            foreach (var error in result.Errors)
            {
                var message = string.IsNullOrEmpty(error.Code)
                    ? $"Error message: {error.Message}"
                    : $"Error code: {error.Code} | Message: {error.Message}";

                sb.AppendLine(message);
                if (error.Exception != null)
                {
                    sb.AppendLine("Error exceptions:");
                    sb.AppendLine(error.Exception.GetAllExceptionsMessages());
                }
            }

            return sb.ToString();
        }
    }
}
