using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FI.Business.Users.Handlers;
using FI.Business.Users.Queries;
using FI.Data;
using FI.Infrastructure.Models.Exceptions;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using BusinessModel = FI.Business.Users.Models;
using DataModel = FI.Data.Models.Users;

namespace FI.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class LoginQueryHandlerTests
    {
        private Mock<FIContext> _context;
        private LoginQueryHandler _handler;
        private LoginQuery _request;
        private BusinessModel.UserDetail _result;

        [SetUp]
        public void Init()
        {
            _context = new Mock<FIContext>();
            _handler = new LoginQueryHandler(_context.Object);
            SetupContext();
            SetupRequest();
            SetupResult();
        }

        [TearDown]
        public void Clear()
        {
            _handler = null;
            _context = null;
            _request = null;
            _result = null;
        }

        [Test]
        public async Task WhenUsingCorrectEmailAndPassword_ShouldReturnUserDetails()
        {
            _request.Identifier = "john@doe.com";

            var res = await _handler.Handle(_request, new CancellationToken());

            res.Should().BeEquivalentTo(_result);
        }

        [Test]
        public void WhenUsingInvalidPassword_ShouldReturnCustomException()
        {
            var cex = new CustomException(ErrorCode.Login_Credentials, "Wrong credentials.");
            _request.Password = "wrongpass";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            action.Should().Throw<CustomException>().WithMessage(cex.Message);
        }

        [Test]
        public void WhenUsingCorrectPasswordWithWrongCasing_ShouldReturnCustomException()
        {
            var cex = new CustomException(ErrorCode.Login_Credentials, "Wrong credentials.");
            _request.Password = _request.Password.ToUpper();

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            action.Should().Throw<CustomException>().WithMessage(cex.Message);
        }

        [Test]
        public void WhenUsingInvalidIdentifier_ShouldReturnCustomException()
        {
            var cex = new CustomException(ErrorCode.Login_Credentials, "Wrong credentials.");
            _request.Identifier = "somethingwrong";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            action.Should().Throw<CustomException>().WithMessage(cex.Message);
        }

        private void SetupContext()
        {
            var user = new DataModel.User
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
                }
            };

            user.Detail.User = user;

            var users = new List<DataModel.User>
            {
                user
            };

            _context.Setup(u => u.Users).ReturnsDbSet(users);
        }

        private void SetupRequest()
        {
            _request = new LoginQuery
            {
                Identifier = "johndoe",
                Password = "password"
            };
        }

        private void SetupResult()
        {
            _result = new BusinessModel.UserDetail
            {
                UserId = "1",
                FirstName = "John",
                LastName = "Doe"
            };
        }
    }
}
