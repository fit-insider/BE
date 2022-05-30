using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FI.Business.Users.Commands;
using FI.Business.Users.Handlers;
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
    public class EditUserCommandHandlerTests
    {
        private Mock<FIContext> _context;
        private EditUserCommandHandler _handler;
        private EditUserCommand _command;

        [SetUp]
        public void Init()
        {
            _context = new Mock<FIContext>();
            _handler = new EditUserCommandHandler(_context.Object);
            SetupContext();
            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldReturnCorrectUser_WhenUpdateWithoutPasswordIsSuccessful()
        {
            _command.OldPassword = "";
            _command.NewPassword = "";
            _command.ConfirmPassword = "";

            var res = await _handler.Handle(_command, new CancellationToken());

            var correctEditedUser = new BusinessModel.UserDetail
            {
                UserId = "1",
                FirstName = "John",
                LastName = "Doe"
            };

            res.Should().BeEquivalentTo(correctEditedUser);
        }

        [Test]
        public async Task ShouldReturnCorrectUser_WhenUpdateWithPasswordIsSuccessful()
        {
            var res = await _handler.Handle(_command, new CancellationToken());

            var correctEditedUser = new BusinessModel.UserDetail
            {
                UserId = "1",
                FirstName = "John",
                LastName = "Doe",
            };

            res.Should().BeEquivalentTo(correctEditedUser);
        }

        [Test]
        public void ShouldThrowCustomException_WhenUserDoesNotExist()
        {
            var cex = new CustomException(ErrorCode.EditUser_User, "User does not exist!");

            _command.Id = "10";

            Func<Task> action = async () => await _handler.Handle(_command, new CancellationToken());

            action.Should().Throw<CustomException>().WithMessage(cex.Message);
        }

        [Test]
        public void ShouldThrowCustomException_WhenOldPasswordAndExistingPasswordsDoNotMatch()
        {
            var cex = new CustomException(ErrorCode.EditUser_Password, "Old password must match with existing!");
            _command.OldPassword = "wrongpassword";

            Func<Task> action = async () => await _handler.Handle(_command, new CancellationToken());

            action.Should().Throw<CustomException>().WithMessage(cex.Message);
        }

        [Test]
        public void ShouldThrowCustomException_WhenOldPasswordAndExistingPasswordsDoNotMatchCaseSensitive()
        {
            var cex = new CustomException(ErrorCode.EditUser_Password, "Old password must match with existing!");
            _command.OldPassword = "onepassword";

            Func<Task> action = async () => await _handler.Handle(_command, new CancellationToken());

            action.Should().Throw<CustomException>().WithMessage(cex.Message);
        }

        private void SetupContext()
        {
            var users = new List<DataModel.User>
            {
                new DataModel.User
                {
                    Id = "1",
                    Email = "one@email.com",
                    Detail = new DataModel.UserDetail
                    {
                        FirstName = "oneFN",
                        LastName = "oneLN",
                        Password = "onePassword"
                    }
                },
                new DataModel.User
                {
                    Id = "2",
                    Email = "two@email.com",
                    Detail = new DataModel.UserDetail
                    {
                        FirstName = "twoFN",
                        LastName = "twoLN",
                        Password = "twoPassword"
                    }
                },
                new DataModel.User
                {
                    Id = "3",
                    Email = "three@email.com",
                    Detail = new DataModel.UserDetail
                    {
                        FirstName = "threeFN",
                        LastName = "threeLN",
                        Password = "threePassword"
                    }
                }
            };

            _context.Setup(u => u.Users).ReturnsDbSet(users);
        }

        private void CreateRequest()
        {
            _command = new EditUserCommand
            {
                Id = "1",
                Email = "one@email.com",
                OldPassword = "onePassword",
                NewPassword = "newPassword",
                ConfirmPassword = "newPassword",
                FirstName = "John",
                LastName = "Doe"
            };
        }
    }
}
