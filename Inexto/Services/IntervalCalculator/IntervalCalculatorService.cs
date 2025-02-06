namespace Inexto.Services.IntervalCalculator
{
    using AutoMapper;
    using FluentValidation;
    using System.Collections.Generic;

    using Inexto.Extensions;
    using Inexto.Models;

    public class IntervalCalculatorService(
            IMapper mapper,
            IValidator<Interval> intervalValidator) 
        : IIntervalCalculatorService
    {
        private readonly IMapper mapper = mapper;
        private readonly IValidator<Interval> intervalValidator = intervalValidator;

        /// <summary>
        /// Indicates if two intervals overlap each other
        /// </summary>
        /// <param name="intervalA"></param>
        /// <param name="intervalB"></param>
        /// <exception cref="Exception">When invalid arguments are provided</exception>
        /// <returns></returns>
        public bool DoesOverlap(Interval intervalA, Interval intervalB)
        {
            var paramValidation = this.ValidateParameters(new List<Interval>(2) { intervalA, intervalB });
            if (!paramValidation.IsSuccess)
            {
                Console.WriteLine(paramValidation.GetErrorMessages());
                throw new ArgumentException("Invalid arguments provided");
            }

            return
                // In case the intervals are the same, they should be considered as overlapping
                (intervalA.Start == intervalB.Start && intervalA.End == intervalB.End) ||
                (intervalA.End > intervalB.Start && intervalB.End > intervalA.Start);
        }

        /// <summary>
        /// Validate that the provided Intervales are valid to be compared
        /// </summary>
        /// <param name="intervales"></param>
        /// <returns></returns>
        private Result<Error> ValidateParameters(IList<Interval> intervales)
        {
            var result = new Result<Error>();
            if ((intervales?.Any() ?? false) == false)
            {
                result.Errors.Add(new Error("No arguments provided"));
                return result;
            }

            foreach (var interval in intervales)
            {
                var validationResult = this.intervalValidator.Validate(interval);
                if (!validationResult.IsValid)
                {
                    result.Errors.AddRange(this.mapper.Map<ICollection<Error>>(validationResult.Errors));
                }
            }

            result.IsSuccess = !result.Errors.Any();
            return result;
        }
    }
}
