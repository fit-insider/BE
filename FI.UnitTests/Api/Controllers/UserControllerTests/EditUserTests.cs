using FI.API.Controllers;
using FI.API.Requests.Users;
using FI.Business.Users.Commands;
using FI.Business.Users.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace FI.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    class EditUserTests
    {
        private UserController _controller;
        private Mock<IMediator> _mediator;
        private EditUserRequest _request;
        private string _id;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();
            _controller = new UserController(_mediator.Object);
            _id = "1";

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendEditUserCommand()
        {
            _mediator.Setup(m => m.Send(It.IsAny<EditUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new UserDetail()));

            var result = await _controller.EditUser(_id, _request);

            _mediator.Verify(m => m.Send(It.IsAny<EditUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldReturnStatusOk_WhenRequestCompletesSuccessfully()
        {
            var result = await _controller.EditUser(_id, _request);

            result.Result.Should().BeOfType<OkObjectResult>();
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
