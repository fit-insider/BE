using AutoFixture;
using FI.API.Requests.Users;
using FluentAssertions;
using NUnit.Framework;
using FluentValidation.TestHelper;

namespace FI.UnitTests.Api.Validators.Users
{
    [TestFixture]
    public class EditUserRequestValidatorTests
    {
        private EditUserRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new EditUserRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenRequestWithPasswordIsOk_ShouldReturnTrue()
        {
            var model = _fixture.Build<ChangePasswordRequest>()
                .With(x => x.Email, "john@doe.com")
                .With(x => x.NewPassword, "johndoepassNEW")
                .With(x => x.ConfirmPassword, "johndoepassNEW")
                .With(x => x.FirstName, "John")
                .With(x => x.LastName, "Doe")
                .Create();

            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }
        
        [Test]
        public void WhenRequestWithoutPasswordIsOk_ShouldReturnTrue()
        {
            var model = _fixture.Build<ChangePasswordRequest>()
                .With(x => x.Email, "john@doe.com")
                .With(x => x.OldPassword, "")
                .With(x => x.NewPassword, "")
                .With(x => x.ConfirmPassword, "")
                .With(x => x.FirstName, "John")
                .With(x => x.LastName, "Doe")
                .Create();

            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void WhenRequestHasOnePassword_ShouldReturnError()
        {
            var model = _fixture.Build<ChangePasswordRequest>()
                .With(x => x.Email, "john@doe.com")
                .With(x => x.OldPassword, "password")
                .With(x => x.NewPassword, "")
                .With(x => x.ConfirmPassword, "")
                .With(x => x.FirstName, "John")
                .With(x => x.LastName, "Doe")
                .Create();

            _validator.ShouldHaveValidationErrorFor(x => x.NewPassword, model);
            _validator.ShouldHaveValidationErrorFor(x => x.ConfirmPassword, model);
        }

        [Test]
        public void WhenRequestHasSameOldAndNewPasswords_ShouldReturnError()
        {
            var model = _fixture.Build<ChangePasswordRequest>()
                .With(x => x.Email, "john@doe.com")
                .With(x => x.OldPassword, "password")
                .With(x => x.NewPassword, "password")
                .With(x => x.ConfirmPassword, "password")
                .With(x => x.FirstName, "John")
                .With(x => x.LastName, "Doe")
                .Create();
            
            _validator.ShouldHaveValidationErrorFor(x => x.NewPassword, model);
        }

        [Test]
        public void WhenRequestHasDifferentNewAndConfirmPasswords_ShouldReturnError()
        {
            var model = _fixture.Build<ChangePasswordRequest>()
                .With(x => x.Email, "john@doe.com")
                .With(x => x.OldPassword, "password")
                .With(x => x.NewPassword, "newpassword")
                .With(x => x.ConfirmPassword, "newpassword1")
                .With(x => x.FirstName, "John")
                .With(x => x.LastName, "Doe")
                .Create();
            
            _validator.ShouldHaveValidationErrorFor(x => x.NewPassword, model);
        }
    }
}
