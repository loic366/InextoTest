namespace Inexto.XUnitTests.Services
{
    using AutoMapper;
    using FluentValidation;
    using Moq;

    using Inexto.Models;
    using Inexto.Services.IntervalCalculator;

    public class IntervalCalculatorServiceTests
    {
        /// <summary>
        /// Should not detect overlapping intervals
        /// </summary>
        /// <param name="intervalAStart"></param>
        /// <param name="intervalAEnd"></param>
        /// <param name="intervalBStart"></param>
        /// <param name="intervalBEnd"></param>
        [Theory]
        [InlineData(1, 3, 4, 6)]
        [InlineData(1, 3, 3, 6)]
        [InlineData(1, 3, 3, 3)]
        [InlineData(1, 3, 3, 4)]
        [InlineData(3, 4, 1, 2)]
        [InlineData(0, 0, 0, 1)]
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
            Assert.False(sut.DoesOverlap(intervalA, intervalB));
        }

        /// <summary>
        /// Should detect overlapping intervals
        /// </summary>
        /// <param name="intervalAStart"></param>
        /// <param name="intervalAEnd"></param>
        /// <param name="intervalBStart"></param>
        /// <param name="intervalBEnd"></param>
        [Theory]
        [InlineData(1, 11, 2, 12)]
        [InlineData(-4, 5, 0, 1000)]
        [InlineData(42, 456, 25, 123)]
        [InlineData(24, 56, 1, 26)]
        [InlineData(0, 0, 0, 0)]
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
            Assert.True(sut.DoesOverlap(intervalA, intervalB));
        }

        /// <summary>
        /// Test DoesOverlap method with invalid parameters, expecting an ArgumentException
        /// </summary>
        /// <param name="intervalAStart"></param>
        /// <param name="intervalAEnd"></param>
        /// <param name="intervalBStart"></param>
        /// <param name="intervalBEnd"></param>
        [Theory]
        [InlineData(4, -1, 2, 3)]
        [InlineData(4, 8, -1, -3)]
        [InlineData(1000, 423, -10, -42)]
        [InlineData(0, 0, 0, -1)]
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
