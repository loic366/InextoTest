namespace Inexto.XUnitTests.Validators
{
    using Inexto.Models;
    using Inexto.Validators;

    public class IntervalValidatorTests
    {
        /// <summary>
        /// Should validate the interval
        /// </summary>
        /// <param name="intervalStart"></param>
        /// <param name="intervalEnd"></param>
        [Theory]
        [InlineData(1, 3)]
        [InlineData(3, 10000)]
        [InlineData(13, 14)]
        public void ShouldValidateInterval(int intervalStart, int intervalEnd)
        {
            var interval = new Interval(intervalStart, intervalEnd);
            var sut = new IntervalValidator();
            var result = sut.Validate(interval);

            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Should not validate the interval
        /// </summary>
        /// <param name="intervalStart"></param>
        /// <param name="intervalEnd"></param>
        [Theory]
        [InlineData(3, 1)]
        [InlineData(10000, 3)]
        [InlineData(-1, -2)]
        public void ShouldNotValidateInterval(int intervalStart, int intervalEnd)
        {
            var interval = new Interval(intervalStart, intervalEnd);
            var sut = new IntervalValidator();
            var result = sut.Validate(interval);

            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Should validate interval with identitcal start and end
        /// </summary>
        /// <param name="intervalStart"></param>
        /// <param name="intervalEnd"></param>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-2, -2)]
        [InlineData(100, 100)]
        [InlineData(42, 42)]
        public void ShouldValidateSameValuesInterval(int intervalStart, int intervalEnd)
        {
            var interval = new Interval(intervalStart, intervalEnd);
            var sut = new IntervalValidator();
            var result = sut.Validate(interval);

            Assert.True(result.IsValid);
        }
    }
}
