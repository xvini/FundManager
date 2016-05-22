using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagerTest
{
    [TestClass]
    public class AcceptanceCriteria4
    {
        [TestMethod]
        public void Should_HighlightStock_When_MarketValueIsNegative()
        {
            // Arrange
            var stock = new BondStock { Price = -1, Quantity = 1 };

            // Act
            var highlight = stock.Highlight;

            // Assert
            Assert.IsTrue(stock.MarketValue < 0);
            Assert.IsTrue(highlight);
        }

        [TestMethod]
        public void Should_HighlightStock_When_TransactionCostHigherThanTolerance()
        {
            // Arrange
            var stock = new BondStock();
            stock.Price = stock.Tolerance / stock.Ratio + 1;
            stock.Quantity = 1;

            // Act
            bool highlight = stock.Highlight;

            // Assert
            Assert.IsTrue(stock.TransactionCost > stock.Tolerance);
            Assert.IsTrue(highlight);
        }

        [TestMethod]
        public void Should_NotHighlightStock_When_IsANewStock()
        {
            // Arrange
            var stock = new BondStock();

            // Act
            var highlight = stock.Highlight;

            // Assert
            Assert.IsFalse(highlight);
        }

        [TestMethod]
        public void Should_StockHaveHighlightProperty()
        {
            Stock stock = new BondStock();
            bool highlight = stock.Highlight;
        }

        [TestMethod]
        public void Should_StockHaveTolerance()
        {
            Stock stock = new BondStock();
            double tolerance = stock.Tolerance;
        }

        [TestMethod]
        public void Should_StockToleranceIs100000_When_StockIsBond()
        {
            // Arrange
            var bond = new BondStock();

            // Act
            var tolerance = bond.Tolerance;

            // Assert
            Assert.AreEqual(100000, tolerance);
        }

        [TestMethod]
        public void Should_StockToleranceIs200000_When_StockIsEquity()
        {
            // Arrange
            var equity = new EquityStock();

            // Act
            var tolerance = equity.Tolerance;

            // Assert
            Assert.AreEqual(200000, tolerance);
        }
    }
}
