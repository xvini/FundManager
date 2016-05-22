using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FundManager.Models;
using System.Collections.ObjectModel;
using FundManager;
using System.Windows.Input;

namespace FundManagerTest
{
    [TestClass]
    public class AcceptanceCriteria1
    {
        [TestMethod]
        public void Should_AddStock_When_StockIsBond()
        {
            // Arrange
            var vm = new MainViewModel();
            vm.NewStockType = StockType.Bond;

            // Act
            vm.AddStock.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.Funds.Count);
        }

        [TestMethod]
        public void Should_AddStock_When_StockIsEquity()
        {
            // Arrange
            var vm = new MainViewModel();
            vm.NewStockType = StockType.Equity;

            // Act
            vm.AddStock.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.Funds.Count);
        }

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
        public void Should_FundCollectionBeObervableCollectionOfStocks()
        {
            // Arrange
            var funds = new FundCollection();

            // Act
            bool isObservable = funds is ObservableCollection<Stock>;

            // Assert
            Assert.IsTrue(isObservable);
        }

        [TestMethod]
        public void Should_HaveFundCollection()
        {
            var funds = new FundCollection();
        }

        [TestMethod]
        public void Should_HaveMainViewModel()
        {
            var vm = new MainViewModel();
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

        [TestMethod]
        public void Should_MainViewModelHaveAddStockCommand()
        {
            // Arrange
            var vm = new MainViewModel();

            // Act
            var cmd = vm.AddStock;

            // Assert
            Assert.IsTrue(cmd is ICommand);
        }

        [TestMethod]
        public void Should_MainViewModelHaveFunds()
        {
            // Arrange
            var vm = new MainViewModel();

            // Act
            var funds = vm.Funds;

            // Assert
            Assert.IsTrue(funds is FundCollection);
        }

        [TestMethod]
        public void Should_MainViewModelHaveNewStockType()
        {
            // Arrange
            var vm = new MainViewModel();

            // Act
            var stockType = vm.NewStockType;

            // Assert
            Assert.IsTrue(stockType is StockType);
        }
    }
}
