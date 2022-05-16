using System.Threading;
using System.Threading.Tasks;
using FI.API.Controllers;
using FI.API.Requests.Users;
using FI.Business.Users.Models;
using FI.Business.Users.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FI.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    public class LoginTests
    {
        private Mock<IMediator> _mediator;
        private UserController _controller;
        private LoginRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();
            _controller = new UserController(_mediator.Object);
            SetupRequest();
        }

        [TearDown]
        public void Clean()
        {
            _mediator = null;
            _controller = null;
            _request = null;
        }

        [Test]
        public async Task ShouldReturnStatusOk_WhenRequestCompletesSuccessfully()
        {
            var res = await _controller.Login(_request);
            
            res.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task ShouldReturnLoginResult_WhenRequestCompletesSuccessfully()
        {
            _mediator.Setup(m => m.Send(It.IsAny<LoginQuery>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new UserDetail()));

            await _controller.Login(_request);

            _mediator.Verify(m => m.Send(It.IsAny<LoginQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        private void SetupRequest()
        {
            _request = new LoginRequest
            {
                Identifier = "johndoe",
                Password = "password"
            };
        }
    }
}