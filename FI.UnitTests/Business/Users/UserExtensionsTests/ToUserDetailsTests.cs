using FI.Business.Users;
using FluentAssertions;
using NUnit.Framework;
using BusinessModel = FI.Business.Users.Models;
using DataModel = FI.Data.Models.Users;

namespace FI.UnitTests.Business.Users.UserExtensionsTests
{
    [TestFixture]
    public class ToUserDetailsTests
    {
        private DataModel.User _user;
        private BusinessModel.UserDetail _result;

        [SetUp]
        public void Init()
        {
            _user = new DataModel.User
            {
                Id = "1",
                Email = "john@doe.com",
                Detail = new DataModel.UserDetail
                {
                    Id = "1",
                    UserId = "1",
                    FirstName = "John",
                    LastName = "Doe",
                    Password = "password",
                    User = _user
                }
            };
            _result = _user.ToUserDetail();
        }

        [TearDown]
        public void Clean()
        {
            _user = null;
            _result = null;
        }

        [Test]
        public void ShouldReturnUserDetailsWithCorrectId()
        {
            _result.UserId.Should().Be("1");
        }

        [Test]
        public void ShouldReturnUserDetailsWithCorrectFirstName()
        {
            _result.FirstName.Should().Be("John");
        }

        [Test]
        public void ShouldReturnUserDetailsWithCorrectLastName()
        {
            _result.LastName.Should().Be("Doe");
        }
    }
}
