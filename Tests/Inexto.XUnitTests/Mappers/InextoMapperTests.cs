namespace Inexto.XUnitTests.Mappers
{
    using Inexto.Mappers;
    using Inexto.Models;

    public class InextoMapperTests
    {
        /// <summary>
        /// Should map ValidationFailure to Error successfully
        /// </summary>
        [Fact]
        public void ShouldMapErrorToValidationFailure()
        {
            var validationFailure = new FluentValidation.Results.ValidationFailure()
            {
                ErrorCode = "C-001",
                ErrorMessage = "Foo",
            };

            var sut = InextoMapper.GetMapper();
            var error = sut.Map<Error>(validationFailure);

            Assert.Equal(error.Code, validationFailure.ErrorCode);
            Assert.Equal(error.Message, validationFailure.ErrorMessage);
        }

        /// <summary>
        /// Should not throw exception while mapping ValidationFailure to Error
        /// </summary>
        [Fact]
        public void ShouldNotThrowExceptionWhileMappingErrorToValidationFailure()
        {
            var validationFailure = new FluentValidation.Results.ValidationFailure();

            var sut = InextoMapper.GetMapper();
            var error = sut.Map<Error>(validationFailure);

            var exception = Record.Exception(() => sut.Map<Error>(validationFailure));
            Assert.Null(exception);
        }
    }
}
