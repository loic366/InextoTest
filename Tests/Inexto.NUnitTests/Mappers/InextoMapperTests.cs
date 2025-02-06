namespace Inexto.NUnitTests.Mappers
{
    using Inexto.Mappers;
    using Inexto.Models;

    /// <summary>
    /// Tests for the InextoMapper class
    /// </summary>
    internal class InextoMapperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Should map ValidationFailure to Error successfully
        /// </summary>
        [Test]
        public void ShouldMapErrorToValidationFailure()
        {
            var validationFailure = new FluentValidation.Results.ValidationFailure()
            {
                ErrorCode = "C-001",
                ErrorMessage = "Foo",
            };

            var sut = InextoMapper.GetMapper();
            var error = sut.Map<Error>(validationFailure);

            Assert.That(error.Code, Is.EqualTo(validationFailure.ErrorCode));
            Assert.That(error.Message, Is.EqualTo(validationFailure.ErrorMessage));
        }

        /// <summary>
        /// Should not throw exception while mapping ValidationFailure to Error
        /// </summary>
        [Test]
        public void ShouldNotThrowExceptionWhileMappingErrorToValidationFailure()
        {
            var validationFailure = new FluentValidation.Results.ValidationFailure();

            var sut = InextoMapper.GetMapper();
            var error = sut.Map<Error>(validationFailure);

            Assert.DoesNotThrow(() => sut.Map<Error>(validationFailure));
        }
    }
}
