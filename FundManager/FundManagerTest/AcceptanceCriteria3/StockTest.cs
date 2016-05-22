using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagerTest.AcceptanceCriteria3
{
    [TestClass]
    public class StockTest
    {
        [TestMethod]
        public void Should_BeAbleToTriggerStockWeightPropertyNotification()
        {
            // Arrange
            Stock stock = new BondStock();

            bool changed = false;
            stock.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(stock.Weight))
                {
                    changed = true;
                }
            };

            // Act
            stock.TriggerWeightNotification();

            // Assert
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void Should_BondStockCostRatioBeTwoPercentage()
        {
            // Arrange
            Stock stock = new BondStock();

            // Act
            var ratio = stock.Ratio;

            // Assert
            Assert.AreEqual(0.02, ratio);
        }

        [TestMethod]
        public void Should_EquityStockCostRatioBeHalfPercentage()
        {
            // Arrange
            Stock stock = new EquityStock();

            // Act
            var ratio = stock.Ratio;

            // Assert
            Assert.AreEqual(0.005, ratio);
        }

        [TestMethod]
        public void Should_StockCostBeRatioTimesValue()
        {
            // Arrange
            var stock = new BondStock
            {
                Price = 45,
                Quantity = 4
            };

            // Act
            var cost = stock.TransactionCost;

            // Assert
            Assert.AreEqual(stock.MarketValue * stock.Ratio, cost);
        }

        [TestMethod]
        public void Should_HaveName()
        {
            Stock stock = new BondStock();
            string name = stock.Name;
        }

        [TestMethod]
        public void Should_HaveMarketValue()
        {
            Stock stock = new BondStock();
            double value = stock.MarketValue;
        }

        [TestMethod]
        public void Should_HaveTransactionCost()
        {
            Stock stock = new BondStock();
            double cost = stock.TransactionCost;
        }

        [TestMethod]
        public void Should_HaveWeight()
        {
            Stock stock = new BondStock();
            double weight = stock.Weight;
        }

        [TestMethod]
        public void Should_ImplementINotifyPropertyChanged()
        {
            // Arrange
            Stock stock1 = new BondStock();
            Stock stock2 = new EquityStock();

            // Act
            var iNotify1 = stock1 as INotifyPropertyChanged;
            var iNotify2 = stock2 as INotifyPropertyChanged;

            // Assert
            Assert.IsNotNull(iNotify1);
            Assert.IsNotNull(iNotify2);
        }

        [TestMethod]
        public void Should_MarketValueBeCalculatedFromPriceAndQuantity()
        {
            // Arrange
            var stock1 = new BondStock { Price = 10, Quantity = 3 };
            var stock2 = new EquityStock { Price = 10, Quantity = 3 };

            // Act
            var value1 = stock1.MarketValue;
            var value2 = stock2.MarketValue;

            // Assert
            Assert.AreEqual(30, value1);
            Assert.AreEqual(30, value2);
        }

        [TestMethod]
        public void Should_WeightBeValuePercentageOfTotalStockValue()
        {
            // Arrange
            var bond = new BondStock { Price = 1, Quantity = 1 };
            var equity = new EquityStock { Price = 1, Quantity = 99 };

            var funds = new FundCollection();
            funds.Add(bond);
            funds.Add(equity);

            // Act
            var bondWeight = bond.Weight;
            var equityWeight = equity.Weight;

            // Assert
            Assert.AreEqual(1, bondWeight);
            Assert.AreEqual(99, equityWeight);
        }
    }
}
