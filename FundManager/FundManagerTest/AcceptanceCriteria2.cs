using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FundManager;
using System.ComponentModel;
using FundManager.Models;

namespace FundManagerTest
{
    [TestClass]
    public class AcceptanceCriteria2
    {
        [TestMethod]
        public void Should_MainViewModelImplementINotifyPropertyChanged()
        {
            // Arrange
            var vm = new MainViewModel();

            // Act
            var iNotify = vm as INotifyPropertyChanged;

            // Assert
            Assert.IsNotNull(iNotify);
        }

        [TestMethod]
        public void Should_MainViewModelCallNotifyProperty_When_StockPriceChanges()
        {
            // Assert
            var vm = new MainViewModel();

            bool called = false;
            vm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(vm.NewStockPrice))
                {
                    called = true;
                }
            };

            // Act
            vm.NewStockPrice = 10;

            // Assert
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void Should_MainViewModelCallNotifyProperty_When_StockQuantityChanges()
        {
            // Assert
            var vm = new MainViewModel();

            bool called = false;
            vm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(vm.NewStockQuantity))
                {
                    called = true;
                }
            };

            // Act
            vm.NewStockQuantity = 10;

            // Assert
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void Should_MainViewModelForbidToAddStock_When_OnlyStockPriceIsWrong()
        {
            // Arrange
            var vm = new MainViewModel();
            vm.NewStockPrice = -1;
            vm.NewStockQuantity = 1;

            // Assert
            var canAdd = vm.AddStock.CanExecute(null);

            // Assert
            Assert.IsFalse(canAdd);
        }

        [TestMethod]
        public void Should_MainViewModelForbidToAddStock_When_OnlyStockQuantityIsWrong()
        {
            // Arrange
            var vm = new MainViewModel();
            vm.NewStockQuantity = -1;
            vm.NewStockPrice = 1;

            // Assert
            var canAdd = vm.AddStock.CanExecute(null);

            // Assert
            Assert.IsFalse(canAdd);
        }

        [TestMethod]
        public void Should_MainViewModelHaveNewStockPrice()
        {
            double price = new MainViewModel().NewStockPrice;
        }

        [TestMethod]
        public void Should_MainViewModelHaveNewStockQuantity()
        {
            double quantity = new MainViewModel().NewStockQuantity;
        }

        [TestMethod]
        public void Should_MainViewModelReportError_When_NewStockPriceIsNegative()
        {
            // Arrange
            var vm = new MainViewModel();
            vm.NewStockPrice = -10;

            // Act
            var error = vm[nameof(vm.NewStockPrice)];

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(error));
        }

        [TestMethod]
        public void Should_MainViewModelReportError_When_NewStockPriceIsZero()
        {
            // Arrange
            var vm = new MainViewModel();
            vm.NewStockPrice = 0;

            // Act
            var error = vm[nameof(vm.NewStockPrice)];

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(error));
        }

        [TestMethod]
        public void Should_MainViewModelReportError_When_NewStockQuantityIsNegative()
        {
            // Arrange
            var vm = new MainViewModel();
            vm.NewStockQuantity = -10;

            // Act
            var error = vm[nameof(vm.NewStockQuantity)];

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(error));
        }

        [TestMethod]
        public void Should_MainViewModelReportError_When_NewStockQuantityIsZero()
        {
            // Arrange
            var vm = new MainViewModel();
            vm.NewStockQuantity = 0;

            // Act
            var error = vm[nameof(vm.NewStockQuantity)];

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(error));
        }

        [TestMethod]
        public void Should_MainViewModelSetPrice_When_AddsStock()
        {
            // Arrange
            const float ExpectedPrice = 15;

            var vm = new MainViewModel();
            vm.NewStockPrice = ExpectedPrice;

            // Act
            vm.AddStock.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.Funds.Count);
            Assert.AreEqual(ExpectedPrice, vm.Funds[0].Price);
        }

        [TestMethod]
        public void Should_MainViewModelSetQuantity_When_AddsStock()
        {
            // Arrange
            const float ExpectedQuantity = 20;

            var vm = new MainViewModel();
            vm.NewStockQuantity = ExpectedQuantity;

            // Act
            vm.AddStock.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.Funds.Count);
            Assert.AreEqual(ExpectedQuantity, vm.Funds[0].Quantity);
        }

        [TestMethod]
        public void Should_MainViewModelSupportIDataErrorInfo()
        {
            // Arrange
            var vm = new MainViewModel();

            // Act
            var errorInfo = vm as IDataErrorInfo;

            // Assert
            Assert.IsNotNull(errorInfo);
        }

        [TestMethod]
        public void Should_StockHasPrice()
        {
            double price = new Stock().Price;
        }

        [TestMethod]
        public void Should_StockHasQuantity()
        {
            double quantity = new Stock().Quantity;
        }
    }
}
