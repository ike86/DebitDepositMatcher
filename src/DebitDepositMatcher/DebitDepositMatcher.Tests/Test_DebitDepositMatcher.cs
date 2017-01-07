using System;
using System.Collections.Generic;
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
