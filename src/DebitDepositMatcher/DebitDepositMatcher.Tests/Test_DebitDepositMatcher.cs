using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace DebitDepositMatcher.Tests
{
    public class Test_DebitDepositMatcher
    {
        [TestClass]
        public class Match
        {
            private IFixture fixture = new Fixture();
            private DebitDepositMatcher subject;

            public Match()
            {
                subject = fixture.Freeze<DebitDepositMatcher>();
            }

            [TestMethod]
            public void ReturnsEmpty_if_PassedEmpty()
            {
                var debits = Enumerable.Empty<decimal>();
                var deposits = debits;

                var result = subject.Match(debits, deposits);

                result.Should().BeEmpty();
            }

            [TestMethod]
            public void ReturnsPairedDebitDepositValues_if_TheyMatch()
            {
                var debits = fixture.CreateMany<decimal>().ToArray();
                var deposits = debits;

                var result = subject.Match(debits, deposits);

                result.Select(t => t.Item1).ShouldBeEquivalentTo(debits,
                    "because we want all debit items in the result");
                result.Select(t => t.Item2).ShouldBeEquivalentTo(deposits,
                    "because we want all deposit items in the result");
                result.Should().OnlyContain(t => t.Item1 == t.Item2 || t.Item1 == 0 || t.Item2 == 0,
                    "because a pair should match, or either of its members should be 0");
                result.Should().HaveCount(debits.Count());
            }

            [TestMethod]
            public void ReturnsUnmatchedDebitDepositValues_if_TheyDoNotMatch()
            {
                var numberOfItems = Math.Min(fixture.Create<int>(), 5); // for the sake of debugger
                var debits = fixture.CreateMany<decimal>(numberOfItems).ToArray();
                var deposits = fixture.CreateMany<decimal>(numberOfItems).ToArray();

                var result = subject.Match(debits, deposits);

                result.Select(t => t.Item1).Where(d => d != 0).ShouldBeEquivalentTo(debits,
                    "because we want all debit items in the result");
                result.Select(t => t.Item2).Where(d => d != 0).ShouldBeEquivalentTo(deposits,
                    "because we want all deposit items in the result");
                result.Should().OnlyContain(t => t.Item1 == t.Item2 || t.Item1 == 0 || t.Item2 == 0,
                    "because a pair should match, or either of its members should be 0");
            }
        }
    }
}
