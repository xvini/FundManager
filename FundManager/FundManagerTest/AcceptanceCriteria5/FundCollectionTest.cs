using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagerTest.AcceptanceCriteria5
{
    [TestClass]
    public class FundCollectionTest
    {
        [TestMethod]
        public void Should_CreateStockSummaryWithTotalSummaryProvided()
        {
            // Arrange
            var funds = new FundCollection();

            // Act
            var bondSummary = funds.BondSummary;
            var equitySummary = funds.EquitySummary;

            // Assert
            Assert.AreEqual(funds.TotalSummary, bondSummary.TotalSummary);
            Assert.AreEqual(funds.TotalSummary, equitySummary.TotalSummary);
        }

        [TestMethod]
        public void Should_HaveBondSummary()
        {
            // Arrange
            var funds = new FundCollection();
            funds.Add(new BondStock { Price = 1, Quantity = 1 });

            // Act
            StockSummary summary = funds.BondSummary;

            // Assert
            Assert.IsNotNull(summary);
            Assert.AreEqual(1, summary.Count);
            Assert.AreEqual(1, summary.TotalValue);
        }

        [TestMethod]
        public void Should_HaveEquitySummary()
        {
            // Arrange
            var funds = new FundCollection();
            funds.Add(new EquityStock { Price = 1, Quantity = 1 });

            // Act
            StockSummary summary = funds.EquitySummary;

            // Assert
            Assert.IsNotNull(summary);
            Assert.AreEqual(1, summary.Count);
            Assert.AreEqual(1, summary.TotalValue);
        }

        [TestMethod]
        public void Should_HaveTotalSummary()
        {
            // Arrange
            var funds = new FundCollection();
            funds.Add(new BondStock { Price = 10, Quantity = 1 });
            funds.Add(new EquityStock { Price = 10, Quantity = 1 });

            // Act
            StockSummary summary = funds.TotalSummary;

            // Assert
            Assert.IsNotNull(summary);
            Assert.AreEqual(2, summary.Count);
            Assert.AreEqual(20, summary.TotalValue);
        }
    }
}
