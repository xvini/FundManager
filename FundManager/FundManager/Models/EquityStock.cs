using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManager.Models
{
    public class EquityStock : Stock
    {
        public override double Ratio { get { return 0.005; } }
        public override double Tolerance { get { return 200000; } }
        public override StockType Type { get { return StockType.Equity; } }
    }
}
