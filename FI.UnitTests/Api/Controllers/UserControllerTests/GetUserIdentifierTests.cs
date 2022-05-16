using FI.Business.Users.Models;
using FI.Business.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using FI.API.Controllers;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using MediatR;
using Moq;

namespace FI.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    public class GetUserIdentifierTests
    {
        private UserController _controller;
        private Mock<IMediator> _mediator;
        private int _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new UserController(_mediator.Object);

            CreateRequest();
        }

        [Test]
        public async Task ShouldSendGetUserDetailsQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetUserDetailsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new UserIdentifier()));

            var result = await _controller.GetUserDetails(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetUserDetailsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldReturnStatusOk_WhenRequestCompletesSuccessfully()
        {
            var result = await _controller.GetUserDetails(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        private void CreateRequest()
        {
            _request = 1;
        }
    }
}