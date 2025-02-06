namespace Inexto.NUnitTests.Validators
{
    using Inexto.Models;
    using Inexto.Validators;

    internal class IntervalValidatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Should validate the interval
        /// </summary>
        /// <param name="intervalStart"></param>
        /// <param name="intervalEnd"></param>
        [TestCase(1, 3)]
        [TestCase(3, 10000)]
        [TestCase(13, 14)]
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
        [TestCase(3, 1)]
        [TestCase(10000, 3)]
        [TestCase(-1, -2)]
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
        [TestCase(0, 0)]
        [TestCase(-2, -2)]
        [TestCase(100, 100)]
        [TestCase(42, 42)]
        public void ShouldValidateSameValuesInterval(int intervalStart, int intervalEnd)
        {
            var interval = new Interval(intervalStart, intervalEnd);
            var sut = new IntervalValidator();
            var result = sut.Validate(interval);

            Assert.True(result.IsValid);
        }
    }
}
