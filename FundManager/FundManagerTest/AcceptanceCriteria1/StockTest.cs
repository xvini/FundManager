using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagerTest.AcceptanceCriteria1
{
    [TestClass]
    public class StockTest
    {
        [TestMethod]
        public void Should_BondStockHaveType()
        {
            // Arrange
            var bond = new BondStock();

            // Act
            var type = bond.Type;

            // Assert
            Assert.AreEqual(StockType.Bond, type);
        }

        [TestMethod]
        public void Should_EquityStockHaveType()
        {
            // Arrange
            var equity = new EquityStock();

            // Act
            var type = equity.Type;

            // Assert
            Assert.AreEqual(StockType.Equity, type);
        }

        [TestMethod]
        public void Should_HaveStocks()
        {
            // Arrange
            var bond = new BondStock();
            var equity = new EquityStock();

            // Act
            var isBondAStock = bond is Stock;
            var isEquityAStock = equity is Stock;

            // Assert
            Assert.IsTrue(isBondAStock);
            Assert.IsTrue(isEquityAStock);
        }
    }
}
