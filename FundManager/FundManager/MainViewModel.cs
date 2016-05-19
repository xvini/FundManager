using FundManager.Models;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System;

namespace FundManager
{
    public class MainViewModel
    {
        public ICommand AddStock { get; }
        public FundCollection Funds { get; } = new FundCollection();
        public StockType NewStockType { get; set; }

        public MainViewModel()
        {
            AddStock = new DelegateCommand(
                () => Funds.Add(new Stock { Type = NewStockType }));
        }
    }
}