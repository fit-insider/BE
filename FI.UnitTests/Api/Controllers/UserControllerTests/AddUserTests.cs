using FI.API.Controllers;
using FI.API.Requests.Users;
using FI.Business.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace FI.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    class AddUserTests
    {
        private UserController _controller;
        private Mock<IMediator> _mediator;
        private AddUserRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new UserController(_mediator.Object);

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendAddUserCommand()
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new bool()));

            var result = await _controller.AddUser(_request);

            _mediator.Verify(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldReturnStatusOk_WhenRequestCompletesSuccessfully()
        {
            var result = await _controller.AddUser(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
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
