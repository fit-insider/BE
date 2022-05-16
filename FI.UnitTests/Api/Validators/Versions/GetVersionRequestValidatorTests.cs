using AutoFixture;
using FI.API.Requests.Versions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace FI.UnitTests.Api.Validators.Versions
{
    [TestFixture]
    public class GetVersionRequestValidatorTests
    {
        private GetVersionRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new GetVersionRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenVersionMissing_ShouldReturnError()
        {
            var model = _fixture.Build<GetVersionRequest>()
                .Without(x => x.Version)
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.Version, model);
        }
    }
}
