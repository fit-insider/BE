using FI.Business.Versions.Handlers;
using FI.Business.Versions.Queries;
using FI.Data;
using FI.Data.Models.ApplicationVersions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;

namespace FI.UnitTests.Business.Versions.Handlers
{
    [TestFixture]
    public class GetVersionQueryHandlerTests
    {
        private Mock<FIContext> _context;
        private GetVersionQueryHandler _handler;
        private GetVersionQuery _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<FIContext>();
            _handler = new GetVersionQueryHandler(_context.Object);

            CreateRequest();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldReturnCorrectVersion()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Version.Should().Be("1.0.1");
        }

        [Test]
        public async Task WhenUsingInvalidName_ShouldReturnNull()
        {
            _request.Name = "Version4";
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Should().BeNull();
        }

        private void SetupContext()
        {
            var applicationVersions = new List<ApplicationVersion>
            {
                new ApplicationVersion { Id=1, Name="Version1", Version="1.0.1" },
                new ApplicationVersion { Id=2, Name="Version2", Version="2.0.1" },
                new ApplicationVersion { Id=3, Name="Version3", Version="3.0.1" }
            };

            _context.Setup(c => c.ApplicationVersions).ReturnsDbSet(applicationVersions);
        }

        private void CreateRequest()
        {
            _request = new GetVersionQuery { Name = "Version1" };
        }
    }
}
