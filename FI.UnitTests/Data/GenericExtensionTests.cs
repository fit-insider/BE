using System.Collections.Generic;
using System.Linq;
using FI.Business;
using FI.Data;
using FluentAssertions;
using NUnit.Framework;

namespace FI.UnitTests.Data
{
    [TestFixture]
    public class PageAccommodationTests
    {
        private IQueryable<int> _query;

        [SetUp]
        public void Init()
        {
            _query = new List<int> { 1, 2, 3 }.AsQueryable();
        }

        [TearDown]
        public void Clean()
        {
            _query = null;
        }

        [Test]
        public void WhenAskingForFirstPage_ShouldReturnFirstAccommodation()
        {
            var res = _query.GetPage(1, 1).ToList();

            res.Should().OnlyContain(a => a == 1);
        }

        [Test]
        public void WhenAskingForSecondPage_ShouldReturnSecondAccommodation()
        {
            var res = _query.GetPage(2, 1).ToList();
            res.Should().OnlyContain(a => a == 2);
        }

        [Test]
        public void WhenAskingForThirdPage_ShouldReturnThirdAccommodation()
        {
            var res = _query.GetPage(3, 1).ToList();
            res.Should().OnlyContain(a => a == 3);
        }
    }
}
