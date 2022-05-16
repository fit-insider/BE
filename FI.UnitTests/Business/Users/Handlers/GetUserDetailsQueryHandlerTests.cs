using FI.Business.Users.Handlers;
using FI.Business.Users.Models;
using FI.Business.Users.Queries;
using FI.Data;
using FI.Data.Models.Users;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FI.Infrastructure.Models.Exceptions;
using BusinessModel = FI.Business.Users.Models;
using DataModel = FI.Data.Models.Users;

namespace FI.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class GetUserDetailsQueryHandlerTests
    {
        private Mock<FIContext> _context;
        private GetUserDetailsQueryHandler _handler;
        private GetUserDetailsQuery _request;
        private UserIdentifier _result;

        [SetUp]
        public void Init()
        {
            _context = new Mock<FIContext>();
            _handler = new GetUserDetailsQueryHandler(_context.Object);
            SetupContext();
            SetupRequest();
            SetupResult();
        }

        [Test]
        public async Task WhenUserExists_ShouldReturnCorrectUserDetails()
        {
            var res = await _handler.Handle(_request, new CancellationToken());

            res.Should().BeEquivalentTo(_result);
        }

        [Test]
        public void WhenUserDoesNotExist_ShouldThrowCustomException()
        {
            var cex = new CustomException(ErrorCode.GetUserDetails_User, "User does not exist!");
            _request.UserId = 2;

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            action.Should().Throw<CustomException>().WithMessage(cex.Message);
        }

        private void SetupContext()
        {
            var user = new User
            {
                Id = 1,
                Email = "john@doe.com",
                Detail = new DataModel.UserDetail
                {
                    Id = 1,
                    UserId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Password = "johndoepass",
                }
            };

            user.Detail.User = user;

            var users = new List<User>
            {
                user
            };

            _context.Setup(u => u.Users).ReturnsDbSet(users);
        }

        private void SetupRequest()
        {
            _request = new GetUserDetailsQuery
            {
                UserId = 1
            };
        }

        private void SetupResult()
        {
            _result = new UserIdentifier
            {
                Email = "john@doe.com",
                Detail = new BusinessModel.UserDetail
                {
                    UserId = 1,
                    FirstName = "John",
                    LastName = "Doe"
                }
            };
        }
    }
}
