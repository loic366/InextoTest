namespace Inexto.Extensions
{
    /// <summary>
    /// Extension methods for ICollection
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Add a range of items to a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                destination.Add(item);
            }
        }
    }
}
