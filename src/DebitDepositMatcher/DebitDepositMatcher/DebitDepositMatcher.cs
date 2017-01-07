using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebitDepositMatcher
{
    public class DebitDepositMatcher
    {
        public IEnumerable<Tuple<decimal, decimal>> Match(IEnumerable<decimal> debits, IEnumerable<decimal> depostis)
        {
            foreach (var tuple in debits.Zip(depostis, (debit, deposit) => new Tuple<decimal, decimal>(debit, deposit)))
            {
                if (tuple.Item1 == tuple.Item2)
                {
                    yield return tuple;
                }
                else
                {
                    yield return new Tuple<decimal, decimal>(tuple.Item1, 0);
                    yield return new Tuple<decimal, decimal>(0, tuple.Item2);
                }
            }
        }
    }
}
