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
            return debits.Zip(depostis, (debit, deposit) => new Tuple<decimal, decimal>(debit, deposit));
        }
    }
}
