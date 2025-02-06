namespace Inexto.NUnitTests.Services
{
    using AutoMapper;
    using FluentValidation;
    using Moq;

    using Inexto.Models;
    using Inexto.Services.IntervalCalculator;

    internal class IntervalCalculatorServiceIntegrationTests
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

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ICollection<Error>>(It.IsAny<List<FluentValidation.Results.ValidationFailure>>()))
                .Returns(new List<Error>());

            var validatorMock = new Mock<IValidator<Interval>>();
            validatorMock.Setup(m => m.Validate(It.IsAny<Interval>())).Returns(new FluentValidation.Results.ValidationResult());

            var sut = new IntervalCalculatorService(mapperMock.Object, validatorMock.Object);
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

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ICollection<Error>>(It.IsAny<List<FluentValidation.Results.ValidationFailure>>()))
                .Returns(new List<Error>());

            var validatorMock = new Mock<IValidator<Interval>>();
            validatorMock.Setup(m => m.Validate(It.IsAny<Interval>())).Returns(new FluentValidation.Results.ValidationResult());

            var sut = new IntervalCalculatorService(mapperMock.Object, validatorMock.Object);
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

            var validationFailure = new FluentValidation.Results.ValidationFailure("Start", "The start of the interval must be smaller than the end.");

            var validatorMock = new Mock<IValidator<Interval>>();
            validatorMock.Setup(m => m.Validate(It.IsAny<Interval>()))
                .Returns(new FluentValidation.Results.ValidationResult(new[] { validationFailure }));

            var error = new Error(validationFailure.ErrorMessage);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ICollection<Error>>(It.IsAny<List<FluentValidation.Results.ValidationFailure>>()))
                .Returns(new List<Error>() { error });

            var sut = new IntervalCalculatorService(mapperMock.Object, validatorMock.Object);
            Assert.Throws<ArgumentException>(() => sut.DoesOverlap(intervalA, intervalB));
        }
    }
}
