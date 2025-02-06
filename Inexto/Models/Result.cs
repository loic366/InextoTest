namespace Inexto.Models
{
    /// <summary>
    /// Container to wrap the result of an operation and its errors (if any)
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class Result<E> where E : Error
    {
        /// <summary>
        /// Indicates whenever the operation succeed or not
        /// </summary>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// List of errors that occurred during the operation
        /// </summary>
        public ICollection<E> Errors { get; set; } = new List<E>();
    }
}
