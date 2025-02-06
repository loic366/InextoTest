namespace Inexto.Services.IntervalCalculator
{
    using Inexto.Models;

    /// <summary>
    /// Provides methods to calculate intervals between different values
    /// </summary>
    public interface IIntervalCalculatorService
    {
        /// <summary>
        /// Indicates if two intervals overlap each other
        /// </summary>
        /// <param name="intervalA"></param>
        /// <param name="intervalB"></param>
        /// <returns></returns>
        bool DoesOverlap(Interval intervalA, Interval intervalB);
    }
}
