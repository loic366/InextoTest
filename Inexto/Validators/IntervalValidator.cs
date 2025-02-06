namespace Inexto.Validators
{
    using Inexto.Models;

    using FluentValidation;

    public class IntervalValidator : AbstractValidator<Interval>
    {
        public IntervalValidator()
        {
            RuleFor(i => i.Start)
                .LessThanOrEqualTo(i => i.End)
                .WithMessage("The start of the interval must be smaller than the end.");
        }
    }
}
