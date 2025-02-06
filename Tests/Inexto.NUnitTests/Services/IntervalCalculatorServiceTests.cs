namespace Inexto.Tests.Services
{
    using Inexto.Mappers;
    using Inexto.Models;
    using Inexto.Services.IntervalCalculator;
    using Inexto.Validators;

    internal class IntervalCalculatorServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Should not detect overlapping intervals
        /// </summary>
        /// <param name="intervalAStart"></param>
        /// <param name="intervalAEnd"></param>
        /// <param name="intervalBStart"></param>
        /// <param name="intervalBEnd"></param>
        [TestCase(1, 3, 4, 6)]
        [TestCase(1, 3, 3, 6)]
        [TestCase(1, 3, 3, 3)]
        [TestCase(1, 3, 3, 4)]
        [TestCase(3, 4, 1, 2)]
        [TestCase(0, 0, 0, 1)]
        public void ShouldNotOverlapTest(int intervalAStart, int intervalAEnd, int intervalBStart, int intervalBEnd)
        {
            var intervalA = new Interval(intervalAStart, intervalAEnd);
            var intervalB = new Interval(intervalBStart, intervalBEnd);

            var sut = new IntervalCalculatorService(InextoMapper.GetMapper(), new IntervalValidator());
            Assert.IsFalse(sut.DoesOverlap(intervalA, intervalB));
        }

        /// <summary>
        /// Should detect overlapping intervals
        /// </summary>
        /// <param name="intervalAStart"></param>
        /// <param name="intervalAEnd"></param>
        /// <param name="intervalBStart"></param>
        /// <param name="intervalBEnd"></param>
        [TestCase(1, 11, 2, 12)]
        [TestCase(-4, 5, 0, 1000)]
        [TestCase(42, 456, 25, 123)]
        [TestCase(24, 56, 1, 26)]
        [TestCase(0, 0, 0, 0)]
        public void ShouldOverlapTest(int intervalAStart, int intervalAEnd, int intervalBStart, int intervalBEnd)
        {
            var intervalA = new Interval(intervalAStart, intervalAEnd);
            var intervalB = new Interval(intervalBStart, intervalBEnd);

            var sut = new IntervalCalculatorService(InextoMapper.GetMapper(), new IntervalValidator());
            Assert.IsTrue(sut.DoesOverlap(intervalA, intervalB));
        }

        /// <summary>
        /// Test DoesOverlap method with invalid parameters, expecting an ArgumentException
        /// </summary>
        /// <param name="intervalAStart"></param>
        /// <param name="intervalAEnd"></param>
        /// <param name="intervalBStart"></param>
        /// <param name="intervalBEnd"></param>
        [TestCase(4, -1, 2, 3)]
        [TestCase(4, 8, -1, -3)]
        [TestCase(1000, 423, -10, -42)]
        [TestCase(0, 0, 0, -1)]
        public void ShouldDetectInvalidParametersTest(int intervalAStart, int intervalAEnd, int intervalBStart, int intervalBEnd)
        {
            var intervalA = new Interval(intervalAStart, intervalAEnd);
            var intervalB = new Interval(intervalBStart, intervalBEnd);

            var sut = new IntervalCalculatorService(InextoMapper.GetMapper(), new IntervalValidator());
            Assert.Throws<ArgumentException>(() => sut.DoesOverlap(intervalA, intervalB));
        }
    }
}
