using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DebitDepositMatcher.Tests
{
    public class Test_DebitDepositMatcher
    {
        [TestClass]
        public class Match
        {
            [TestMethod]
            public void ReturnsEmpty_if_PassedEmpty()
            {
                var subject = new DebitDepositMatcher();

                var result = subject.Match(
                    Enumerable.Empty<decimal>(),
                    Enumerable.Empty<decimal>());

                result.Should().BeEmpty();
            }

            [TestMethod]
            public void ReturnsPairedDebitDepositValues_if_TheyMatch()
            {
                var subject = new DebitDepositMatcher();

                var result = subject.Match(
                    new[] { 42.0m },
                    new[] { 42.0m });

                result.Should().BeEquivalentTo(new[]
                {
                    new Tuple<decimal,decimal>(42m, 42m)
                });
            }
        }
    }
}
