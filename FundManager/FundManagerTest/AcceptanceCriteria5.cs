using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagerTest
{
    [TestClass]
    public class AcceptanceCriteria5
    {
        [TestMethod]
        public void Should_CalculateStockSummaryWeight_When_TotalStockSummaryProvided()
        {
            // Arrange
            var totalSummary = new StockSummary { TotalValue = 100 };
            var summary = new StockSummary(totalSummary) { TotalValue = 30 };

            // Act
            var weight = summary.TotalWeight;

            // Assert
            Assert.AreEqual(30, weight);
        }

        [TestMethod]
        public void Should_FundCollectionCreateStockSummaryWithTotalSummaryProvided()
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
        public void Should_FundCollectionHaveBondSummary()
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
        public void Should_FundCollectionHaveEquitySummary()
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
        public void Should_FundCollectionHaveTotalSummary()
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

        [TestMethod]
        public void Should_StackSummaryHaveWeightProperty()
        {
            double weight = new StockSummary().TotalWeight;
        }

        [TestMethod]
        public void Should_StockSummaryImplementINotifyPropertyChanged()
        {
            // Arrange
            var summary = new StockSummary();

            // Act
            bool isNotify = summary is INotifyPropertyChanged;

            // Assert
            Assert.IsTrue(isNotify);
        }

        [TestMethod]
        public void Should_StockSummaryPresentTotalSummary()
        {
            // Arrange
            var totalSummary = new StockSummary();
            var summary = new StockSummary(totalSummary);

            // Act
            StockSummary returnedSummary = summary.TotalSummary;

            // Assert
            Assert.AreEqual(totalSummary, returnedSummary);
        }

        [TestMethod]
        public void Should_StockSummaryNotify_When_CountChanged()
        {
            // Arrange
            var summary = new StockSummary();

            bool notified = false;
            summary.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(summary.Count))
                {
                    notified = true;
                }
            };

            // Act
            summary.Count++;

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void Should_StockSummaryNotify_When_TotalValueChanged()
        {
            // Arrange
            var summary = new StockSummary();

            bool notified = false;
            summary.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(summary.TotalValue))
                {
                    notified = true;
                }
            };

            // Act
            summary.TotalValue = 100;

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void Should_StockSummaryNotifyWeightChanged_When_TotalSummaryValueChanged()
        {
            // Arrange
            var totalSummary = new StockSummary();
            var summary = new StockSummary(totalSummary);

            bool notified = false;
            summary.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(summary.TotalWeight))
                {
                    notified = true;
                }
            };

            // Act
            totalSummary.TotalValue = 100;

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void Should_StockSummaryNotifyWeightChanged_When_TotalValueChanged()
        {
            // Arrange
            var summary = new StockSummary();

            bool notified = false;
            summary.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(summary.TotalWeight))
                {
                    notified = true;
                }
            };

            // Act
            summary.TotalValue = 100;

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void Should_StockSummaryWeightBeAHundredPercentages_When_NoTotalSummaryProvided()
        {
            // Arrange
            var summary = new StockSummary();

            // Act
            var weight = summary.TotalWeight;

            // Assert
            Assert.AreEqual(100, weight);
        }

        [TestMethod]
        public void Should_StockSummaryWeightBeZero_When_TotalSummaryValueIsZero()
        {
            // Arrange
            var totalSummary = new StockSummary { TotalValue = 0 };
            var summary = new StockSummary(totalSummary);

            // Act
            var weight = summary.TotalWeight;

            // Assert
            Assert.AreEqual(0, weight);
        }
    }
}
