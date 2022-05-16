using FI.Business.Users;
using FI.Business.Users.Commands;
using FI.Business.Users.Models;
using FluentAssertions;
using NUnit.Framework;

namespace FI.UnitTests.Business.Users.UserExtensionsTests
{
    [TestFixture]
    public class EditUserCommandToUserDetailsTests
    {
        private EditUserCommand _command;
        private UserDetail _userDetail;

        [SetUp]
        public void Init()
        {
            CreateCommand();
            _userDetail = _command.ToUserDetails();
        }

        [TearDown]
        public void Clean()
        {
            _command = null;
            _userDetail = null;
        }

        [Test]
        public void ShouldReturnUserDetailsWithCorrectId()
        {
            _userDetail.UserId.Should().Be(1);
        }

        [Test]
        public void ShouldReturnUserDetailsWithCorrectFirstName()
        {
            _userDetail.FirstName.Should().BeEquivalentTo("John");
        }

        [Test]
        public void ShouldReturnUserDetailsWithCorrectLastName()
        {
            _userDetail.LastName.Should().BeEquivalentTo("Doe");
        }

        private void CreateCommand()
        {
            _command = new EditUserCommand
            {
                Id = 1,
                Email = "john@doe.com",
                OldPassword = "password1",
                NewPassword = "newpassword",
                ConfirmPassword = "newpassword",
                FirstName = "John",
                LastName = "Doe"
            };
        }
    }
}
