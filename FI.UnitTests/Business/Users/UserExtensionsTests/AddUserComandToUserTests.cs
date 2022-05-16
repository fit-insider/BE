using FI.Business.Users;
using FI.Business.Users.Commands;
using FI.Data.Models.Users;
using FluentAssertions;
using NUnit.Framework;

namespace FI.UnitTests.Business.Users.UserExtensionsTests
{
    [TestFixture]
    class AddUserComandToUserTests
    {
        private AddUserCommand _command;
        private User _user;

        [SetUp]
        public void Init() {
            CreateRequest();
            _user = _command.ToUser();
        }

        [TearDown]
        public void Clean()
        {
            _command = null;
            _user = null;
        }

        [Test]
        public void ShouldReturnUserWithCorrectEmail()
        {
            _user.Email.Should().Be("john@doe.com");
        }

        [Test]
        public void ShouldReturnUserWithCorrectPassword()
        {
           _user.Detail.Password.Should().Be("password1");
        }

        [Test]
        public void ShouldReturnUserWIthCorrectFirstName()
        {
            _user.Detail.FirstName.Should().Be("John");
        }

        [Test]
        public void ShouldReturnUserWithCorrectLastName()
        {
            _user.Detail.LastName.Should().Be("Doe");
        }

        private void CreateRequest()
        {
            _command = new AddUserCommand
            {
                Email = "john@doe.com",
                Password = "password1",
                FirstName = "John",
                LastName = "Doe"
            };
        }
    }
}
