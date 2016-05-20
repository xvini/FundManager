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
            var stock = new Stock();
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

        [TestMethod]
        public void Should_StockHaveBondStockType()
        {
            new Stock().Type = StockType.Bond;
        }

        [TestMethod]
        public void Should_StockHaveEquityStockType()
        {
            new Stock().Type = StockType.Equity;
        }
    }
}
