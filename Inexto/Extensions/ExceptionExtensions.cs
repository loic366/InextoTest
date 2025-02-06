namespace Inexto.Extensions
{
    using System.Text;

    /// <summary>
    /// Extensions for Exception class
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Retrieve the provided exception message and all inner exceptions messages
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetAllExceptionsMessages(this Exception exception)
        {
            var sb = new StringBuilder();
            var currentException = exception;
            while (currentException != null)
            {
                sb.AppendLine(currentException.Message);
                currentException = currentException.InnerException;
            }

            return sb.ToString();
        }
    }
}
