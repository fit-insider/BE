using FI.API.Requests.Users;
using FI.Business.Users.Commands;
using NUnit.Framework;
using FluentAssertions;

namespace FI.UnitTests.Api.Requests.Users.UserExtensionsTests
{
    [TestFixture]
    public class ToAddUserCommandTests
    {
        private AddUserRequest _request;
        private AddUserCommand _command;

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
        public void ShouldReturnCommandWithCorrectPassword()
        {
            _command.Password.Should().Be("johndoepass");
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
            _request = new AddUserRequest
            {
                Email = "john@doe.com",
                Password = "johndoepass",
                FirstName = "John",
                LastName = "Doe"
            };
        }
    }
}
