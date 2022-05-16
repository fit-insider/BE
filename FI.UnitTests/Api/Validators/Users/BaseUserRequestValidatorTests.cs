using AutoFixture;
using FI.API.Requests.Users;
using NUnit.Framework;
using FluentValidation.TestHelper;
using FluentAssertions;
using FI.UnitTests.Utilities;

namespace FI.UnitTests.Api.Validators.Users
{
    class BaseUserRequestValidatorTests
    {
        private BaseUserRequestValidator<BaseUserRequest> _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new BaseUserRequestValidator<BaseUserRequest>();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenRequestOk_ShouldReturnTrue()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.Email, "john@doe.com")
                .With(x => x.FirstName, "John")
                .With(x => x.LastName, "Doe")
                .Create();

            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void WhenEmailMissing_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .Without(x => x.Email)
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.Email, model);
        }

        [Test]
        public void WhenEmailHasWrongFormat_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.Email, "johndoe.com")
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.Email, model);
        }

        [Test]
        public void WhenEmailMinimumLengthNotMet_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.Email, "j@d.ro")
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.Email, model);
        }

        [Test]
        public void WhenEmailMaximumLengthNotMet_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.Email, "j@" + StringUtilities.GenerateString(70) + ".com")
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.Email, model);
        }

        [Test]
        public void WhenFirstNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .Without(x => x.FirstName)
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.FirstName, model);
        }

        [Test]
        public void WhenFirstNameHasWrongFormat_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.FirstName, "John12")
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.FirstName, model);
        }

        [Test]
        public void WhenFirstNameMinimumLengthNotMet_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.FirstName, StringUtilities.GenerateString(1))
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.FirstName, model);
        }

        [Test]
        public void WhenFirstNameMaximumLengthNotMet_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.FirstName, StringUtilities.GenerateString(101))
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.FirstName, model);
        }

        [Test]
        public void WhenLastNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .Without(x => x.LastName)
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.LastName, model);
        }

        [Test]
        public void WhenLastNameHasWrongFormat_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.LastName, "Doe12")
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.LastName, model);
        }

        [Test]
        public void WhenLastNameMinimumLengthNotMet_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.LastName, StringUtilities.GenerateString(1))
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.LastName, model);
        }

        [Test]
        public void WhenLastNameMaximumLengthNotMet_ShouldReturnError()
        {
            var model = _fixture.Build<BaseUserRequest>()
                .With(x => x.LastName, StringUtilities.GenerateString(101))
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.LastName, model);
        }
    }
}
