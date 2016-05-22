using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagerTest.AcceptanceCriteria3
{
    [TestClass]
    public class FundCollectionTest
    {
        [TestMethod]
        public void Should_AddItselfToStock_When_StockIsAddedToFundCollection()
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
        public void Should_GivesStockName_When_BondIsAdded()
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
        public void Should_GivesStockName_When_EquityIsAdded()
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
        public void Should_HaveTotalBondValue()
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
        public void Should_HaveTotalEquityValue()
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
        public void Should_HaveTotalValue()
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
        public void Should_NotifyStocksAboutWeightChange_When_NewStockIsAdded()
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
    }
}
