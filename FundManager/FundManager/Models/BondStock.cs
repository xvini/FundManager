using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManager.Models
{
    public class BondStock : Stock
    {
        public override double Ratio { get { return 0.02; } }
        public override double Tolerance { get { return 100000; } }
        public override StockType Type { get { return StockType.Bond; } }
    }
}
