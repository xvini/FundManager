using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagerTest.AcceptanceCriteria5
{
    [TestClass]
    public class StockSummaryTest
    {
        [TestMethod]
        public void Should_CalculateWeight_When_TotalStockSummaryProvided()
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
        public void Should_HaveWeightProperty()
        {
            double weight = new StockSummary().TotalWeight;
        }

        [TestMethod]
        public void Should_ImplementINotifyPropertyChanged()
        {
            // Arrange
            var summary = new StockSummary();

            // Act
            bool isNotify = summary is INotifyPropertyChanged;

            // Assert
            Assert.IsTrue(isNotify);
        }

        [TestMethod]
        public void Should_PresentTotalSummary()
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
        public void Should_Notify_When_CountChanged()
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
        public void Should_Notify_When_TotalValueChanged()
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
        public void Should_NotifyWeightChanged_When_TotalSummaryValueChanged()
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
        public void Should_NotifyWeightChanged_When_TotalValueChanged()
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
        public void Should_WeightBeAHundredPercentages_When_NoTotalSummaryProvided()
        {
            // Arrange
            var summary = new StockSummary();

            // Act
            var weight = summary.TotalWeight;

            // Assert
            Assert.AreEqual(100, weight);
        }

        [TestMethod]
        public void Should_WeightBeZero_When_TotalSummaryValueIsZero()
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
