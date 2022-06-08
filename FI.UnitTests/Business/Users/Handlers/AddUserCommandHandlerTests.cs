using FI.Business.Users.Commands;
using FI.Business.Users.Handlers;
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

namespace FI.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    class AddUserCommandHandlerTests
    {
        private Mock<FIContext> _context;
        private AddUserCommandHandler _handler;
        private AddUserCommand _command;

        [SetUp]
        public void Init()
        {
            _context = new Mock<FIContext>();
            _handler = new AddUserCommandHandler(_context.Object);
            SetupContext();
            SetupRequest();
        }

        [TearDown]
        public void Clear()
        {
            _context = null;
            _handler = null;
        }


        [Test]
        public void ShouldReturnCustomException_WhenEmailExists()
        {
            var cex = new CustomException(ErrorCode.AddUser_Email, "Email should be unique");
            _command.Email = "one@email.com";

            Func<Task> action = async () => await _handler.Handle(_command, new CancellationToken());

            action.Should().Throw<CustomException>().WithMessage(cex.Message);
        }


        private void SetupContext()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = "1",
                    Email = "one@email.com"
                },
                new User
                {
                    Id = "2",
                    Email = "two@email.com"
                },
                new User
                {
                    Id = "3",
                    Email = "three@email.com"
                }
            };

            _context.Setup(u => u.Users).ReturnsDbSet(users);
        }

        private void SetupRequest()
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
