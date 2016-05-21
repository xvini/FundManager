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
    public class AcceptanceCriteria3
    {
        [TestMethod]
        public void Should_BeAbleToTriggerStockWeightPropertyNotification()
        {
            // Arrange
            var stock = new Stock();

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
        public void Should_FundCollectionAddItselfToStock_When_StockIsAddedToFundCollection()
        {
            // Arrange
            var stock = new Stock();
            var funds = new FundCollection();

            // Act
            funds.Add(stock);

            // Assert
            Assert.AreEqual(funds, stock.Funds);
        }

        [TestMethod]
        public void Should_FundCollectionGivesStockName_When_BondIsAdded()
        {
            // Assert
            var bond1 = new Stock { Type = StockType.Bond };
            var bond2 = new Stock { Type = StockType.Bond };

            var funds = new FundCollection();

            // Act
            funds.Add(bond1);
            funds.Add(bond2);

            // Assert
            Assert.AreEqual(2, funds.Count);
            Assert.AreEqual("Bond1", bond1.Name);
            Assert.AreEqual("Bond2", bond2.Name);
        }

        [TestMethod]
        public void Should_FundCollectionGivesStockName_When_EquityIsAdded()
        {
            // Assert
            var equity1 = new Stock { Type = StockType.Equity };
            var equity2 = new Stock { Type = StockType.Equity };

            var funds = new FundCollection();

            // Act
            funds.Add(equity1);
            funds.Add(equity2);

            // Assert
            Assert.AreEqual(2, funds.Count);
            Assert.AreEqual("Equity1", equity1.Name);
            Assert.AreEqual("Equity2", equity2.Name);
        }

        [TestMethod]
        public void Should_FundCollectionHasTotalBondValue()
        {
            // Arrange
            var bond1 = new Stock { Type = StockType.Bond, Price = 10, Quantity = 2 };
            var bond2 = new Stock { Type = StockType.Bond, Price = 10, Quantity = 8 };
            var equity = new Stock { Type = StockType.Equity, Price = 10, Quantity = 1 };

            var funds = new FundCollection();
            funds.Add(bond1);
            funds.Add(bond2);
            funds.Add(equity);

            // Act
            double totalBondValue = funds.GetTotalValue(StockType.Bond);

            // Assert
            Assert.AreEqual(100, totalBondValue);
        }

        [TestMethod]
        public void Should_FundCollectionHasTotalEquityValue()
        {
            // Arrange
            var equity1 = new Stock { Type = StockType.Equity, Price = 10, Quantity = 8 };
            var equity2 = new Stock { Type = StockType.Equity, Price = 10, Quantity = 2 };
            var bond = new Stock { Type = StockType.Bond, Price = 10, Quantity = 1 };

            var funds = new FundCollection();
            funds.Add(equity1);
            funds.Add(equity2);
            funds.Add(bond);

            // Act
            double totalBondValue = funds.GetTotalValue(StockType.Equity);

            // Assert
            Assert.AreEqual(100, totalBondValue);
        }

        [TestMethod]
        public void Should_FundCollectionNotifyStockAboutWeightChange_When_TheSameStockTypeWasAdded()
        {
            // Arrange
            var bond = new Stock { Type = StockType.Bond };
            var equity = new Stock { Type = StockType.Equity };

            var funds = new FundCollection();
            funds.Add(bond);
            funds.Add(equity);

            bool bondNotified = false;
            bond.PropertyChanged += (s, a) => bondNotified = true;

            bool equityNotified = false;
            equity.PropertyChanged += (s, a) => equityNotified = true;

            // Act
            funds.Add(new Stock { Type = StockType.Bond });

            // Assert
            Assert.IsTrue(bondNotified);
            Assert.IsFalse(equityNotified);
        }

        [TestMethod]
        public void Should_StockCostBeHalfPercentageOfValue_When_ItIsEquityStock()
        {
            // Arrange
            var stock = new Stock
            {
                Type = StockType.Equity,
                Price = 45,
                Quantity = 4
            };

            // Act
            var cost = stock.TransactionCost;

            // Assert
            Assert.AreEqual(stock.MarketValue * 0.005, cost);
        }

        [TestMethod]
        public void Should_StockCostBeTwoPercentagesOfValue_When_ItIsBondStock()
        {
            // Arrange
            var stock = new Stock
            {
                Type = StockType.Bond,
                Price = 45,
                Quantity = 4
            };

            // Act
            var cost = stock.TransactionCost;

            // Assert
            Assert.AreEqual(stock.MarketValue * 0.02, cost);
        }

        [TestMethod]
        public void Should_StockHasName()
        {
            string name = new Stock().Name;
        }

        [TestMethod]
        public void Should_StockHasMarketValue()
        {
            double value = new Stock().MarketValue;
        }

        [TestMethod]
        public void Should_StockHasTransactionCost()
        {
            double cost = new Stock().TransactionCost;
        }

        [TestMethod]
        public void Should_StockHasWeight()
        {
            double weight = new Stock().Weight;
        }

        [TestMethod]
        public void Should_StockImplementINotifyPropertyChanged()
        {
            // Arrange
            var stock = new Stock();

            // Act
            var iNotify = stock as INotifyPropertyChanged;

            // Assert
            Assert.IsNotNull(iNotify);
        }

        [TestMethod]
        public void Should_StockMarketValueBeCalculatedFromPriceAndQuantity()
        {
            // Arrange
            var stock = new Stock { Price = 10, Quantity = 3 };

            // Act
            var value = stock.MarketValue;

            // Assert
            Assert.AreEqual(30, value);
        }

        [TestMethod]
        public void Should_StockWeightBeValuePercentageOfTotalStockTypeValue()
        {
            // Arrange
            var bond = new Stock { Type = StockType.Bond, Price = 1, Quantity = 1 };
            var equity1= new Stock { Type = StockType.Equity, Price = 1, Quantity = 99 };
            var equity2 = new Stock { Type = StockType.Equity, Price = 1, Quantity = 1 };

            var funds = new FundCollection();
            funds.Add(bond);
            funds.Add(equity1);
            funds.Add(equity2);

            // Act
            var bondWeight = bond.Weight;
            var equity1Weight = equity1.Weight;
            var equity2Weight = equity2.Weight;

            // Assert
            Assert.AreEqual(100, bondWeight);
            Assert.AreEqual(99, equity1Weight);
            Assert.AreEqual(1, equity2Weight);
        }
    }
}
