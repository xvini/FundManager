using FundManager;
using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FundManagerTest.AcceptanceCriteria1
{
    [TestClass]
    public class MainViewModelTest
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
        public void Should_Exist()
        {
            var vm = new MainViewModel();
        }

        [TestMethod]
        public void Should_HaveAddStockCommand()
        {
            // Arrange
            var vm = new MainViewModel();

            // Act
            var cmd = vm.AddStock;

            // Assert
            Assert.IsTrue(cmd is ICommand);
        }

        [TestMethod]
        public void Should_HaveFunds()
        {
            // Arrange
            var vm = new MainViewModel();

            // Act
            var funds = vm.Funds;

            // Assert
            Assert.IsTrue(funds is FundCollection);
        }

        [TestMethod]
        public void Should_HaveNewStockTypeProperty()
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
