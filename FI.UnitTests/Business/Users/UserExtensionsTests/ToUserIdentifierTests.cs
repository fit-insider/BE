using System.Collections.Generic;
using System.Linq;
using FI.Business.Users;
using FluentAssertions;
using NUnit.Framework;
using BusinessModel = FI.Business.Users.Models;
using DataModel = FI.Data.Models.Users;

namespace FI.UnitTests.Business.Users.UserExtensionsTests
{
    [TestFixture]
    public class ToUserIdentifierTests
    {
        private DataModel.User _user;
        private BusinessModel.UserIdentifier _result;

        [SetUp]
        public void Init()
        {
            _user = new DataModel.User
            {
                Id = 1,
                Email = "john@doe.com",
                Detail = new DataModel.UserDetail
                {
                    Id = 1,
                    UserId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Password = "password",
                    User = _user
                }
            };
            _result = new List<DataModel.User> { _user }
                .AsQueryable()
                .ToUserIdentifier().First();
        }


        [Test]
        public void ShouldReturnUserDetailstWithCorrectEmail()
        {
            _result.Email.Should().Be("john@doe.com");
        }

        [Test]
        public void ShouldReturnUserDetailsWithCorrectID()
        {
            _result.Detail.UserId.Should().Be(1);
        }

        [Test]
        public void ShouldReturnUserDetailstWithCorrectFirstName()
        {
            _result.Detail.FirstName.Should().Be("John");
        }

        [Test]
        public void ShouldReturnUserDetailstWithCorrectLastName()
        {
            _result.Detail.LastName.Should().Be("Doe");
        }
    }
}
