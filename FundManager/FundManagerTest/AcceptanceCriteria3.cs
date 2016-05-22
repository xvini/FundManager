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
        public void Should_FundCollectionAddItselfToStock_When_StockIsAddedToFundCollection()
        {
            // Arrange
            Stock stock = new BondStock();
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
            var bond1 = new BondStock();
            var bond2 = new BondStock();

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
            var equity1 = new EquityStock();
            var equity2 = new EquityStock();

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
        public void Should_FundCollectionHaveTotalBondValue()
        {
            // Arrange
            var bond1 = new BondStock { Price = 10, Quantity = 2 };
            var bond2 = new BondStock { Price = 10, Quantity = 8 };
            var equity = new EquityStock { Price = 10, Quantity = 1 };

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
        public void Should_FundCollectionHaveTotalEquityValue()
        {
            // Arrange
            var equity1 = new EquityStock { Price = 10, Quantity = 8 };
            var equity2 = new EquityStock { Price = 10, Quantity = 2 };
            var bond = new BondStock { Price = 10, Quantity = 1 };

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
        public void Should_FundCollectionHaveTotalValue()
        {
            // Arrange
            var bond = new BondStock { Price = 100, Quantity = 1 };
            var equity = new EquityStock { Price = 100, Quantity = 1 };

            var funds = new FundCollection();
            funds.Add(bond);
            funds.Add(equity);

            // Act
            double totalValue = funds.TotalValue;

            // Assert
            Assert.AreEqual(200, totalValue);
        }

        [TestMethod]
        public void Should_FundCollectionNotifyStocksAboutWeightChange_When_NewStockIsAdded()
        {
            // Arrange
            var bond = new BondStock();
            var equity = new EquityStock();

            var funds = new FundCollection();
            funds.Add(bond);
            funds.Add(equity);

            bool bondNotified = false;
            bond.PropertyChanged += (s, a) => bondNotified = true;

            bool equityNotified = false;
            equity.PropertyChanged += (s, a) => equityNotified = true;

            // Act
            funds.Add(new BondStock());

            // Assert
            Assert.IsTrue(bondNotified);
            Assert.IsTrue(equityNotified);
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
        public void Should_StockHasName()
        {
            Stock stock = new BondStock();
            string name = stock.Name;
        }

        [TestMethod]
        public void Should_StockHasMarketValue()
        {
            Stock stock = new BondStock();
            double value = stock.MarketValue;
        }

        [TestMethod]
        public void Should_StockHasTransactionCost()
        {
            Stock stock = new BondStock();
            double cost = stock.TransactionCost;
        }

        [TestMethod]
        public void Should_StockHasWeight()
        {
            Stock stock = new BondStock();
            double weight = stock.Weight;
        }

        [TestMethod]
        public void Should_StockImplementINotifyPropertyChanged()
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
        public void Should_StockMarketValueBeCalculatedFromPriceAndQuantity()
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
        public void Should_StockWeightBeValuePercentageOfTotalStockValue()
        {
            // Arrange
            var bond = new BondStock { Price = 1, Quantity = 1 };
            var equity= new EquityStock { Price = 1, Quantity = 99 };

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
