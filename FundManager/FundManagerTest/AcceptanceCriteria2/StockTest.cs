using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagerTest.AcceptanceCriteria2
{
    [TestClass]
    public class StockTest
    {
        [TestMethod]
        public void Should_HavePrice()
        {
            Stock stock = new BondStock();
            double price = stock.Price;
        }

        [TestMethod]
        public void Should_HaveQuantity()
        {
            Stock stock = new BondStock();
            double quantity = stock.Quantity;
        }
    }
}
