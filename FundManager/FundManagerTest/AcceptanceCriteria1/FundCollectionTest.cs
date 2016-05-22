using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagerTest.AcceptanceCriteria1
{
    [TestClass]
    public class FundCollectionTest
    {
        [TestMethod]
        public void Should_BeObervableCollectionOfStocks()
        {
            // Arrange
            var funds = new FundCollection();

            // Act
            bool isObservable = funds is ObservableCollection<Stock>;

            // Assert
            Assert.IsTrue(isObservable);
        }

        [TestMethod]
        public void Should_Exist()
        {
            var funds = new FundCollection();
        }
    }
}
