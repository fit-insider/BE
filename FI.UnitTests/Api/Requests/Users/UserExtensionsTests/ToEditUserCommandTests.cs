using FI.API.Requests.Users;
using FI.Business.Users.Commands;
using FluentAssertions;
using NUnit.Framework;

namespace FI.UnitTests.Api.Requests.Users.UserExtensionsTests
{
    [TestFixture]
    public class ToEditUserCommandTests
    {
        private EditUserRequest _request;
        private EditUserCommand _command;

        [SetUp]
        public void Init()
        {
            CreateRequest();
            _command = _request.ToCommand();
        }

        [TearDown]
        public void Clean()
        {
            _command = null;
        }


        [Test]
        public void ShouldReturnCommandWithCorrectEmail()
        {
            _command.Email.Should().Be("john@doe.com");
        }

        [Test]
        public void ShouldReturnCommandWithCorrectOldPassword()
        {
            _command.OldPassword.Should().Be("johndoepass");
        }

        [Test]
        public void ShouldReturnCommandWithCorrectNewPassword()
        {
            _command.NewPassword.Should().Be("newpassword");
        }

        [Test]
        public void ShouldReturnCommandWithCorrectConfirmPassword()
        {
            _command.ConfirmPassword.Should().Be("newpassword");
        }

        [Test]
        public void ShouldReturnCommandWithCorrectFirstName()
        {
            _command.FirstName.Should().Be("John");
        }

        [Test]
        public void ShouldReturnCommandWithCorrectLastName()
        {
            _command.LastName.Should().Be("Doe");
        }

        private void CreateRequest()
        {
            _request = new EditUserRequest
            {
                Email = "john@doe.com",
                OldPassword = "johndoepass",
                NewPassword = "newpassword",
                ConfirmPassword = "newpassword",
                FirstName = "John",
                LastName = "Doe"
            };
        }
    }
}
