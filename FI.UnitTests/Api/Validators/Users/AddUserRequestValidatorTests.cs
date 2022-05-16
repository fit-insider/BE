using AutoFixture;
using FI.API.Requests.Users;
using NUnit.Framework;
using FluentValidation.TestHelper;
using FluentAssertions;
using FI.UnitTests.Utilities;

namespace FI.UnitTests.Api.Validators.Users
{
    class AddUserRequestValidatorTests
    {
        private AddUserRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new AddUserRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenRequestOk_ShouldReturnTrue()
        {
            var model = _fixture.Build<AddUserRequest>()
                .With(x => x.Email, "john@doe.com")
                .With(x => x.Password, "johndoepass")
                .With(x => x.FirstName, "John")
                .With(x => x.LastName, "Doe")
                .Create();

            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void WhenPasswordMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddUserRequest>()
                .Without(x => x.Password)
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.Password, model);
        }

        [Test]
        public void WhenPasswordMinumLengthNotMet_ShouldReturnError()
        {
            var model = _fixture.Build<AddUserRequest>()
                .With(x => x.Password, StringUtilities.GenerateString(1))
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.Password, model);
        }

        [Test]
        public void WhenPasswordMaximumLengthNotMet_ShouldReturnError()
        {
            var model = _fixture.Build<AddUserRequest>()
                .With(x => x.Password, StringUtilities.GenerateString(21))
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.Password, model);
        }
    }
}
