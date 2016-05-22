using FundManager.Models;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FundManager
{
    public class MainViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private double _newStockPrice = 1;
        private double _newStockQuantity = 1;

        public ICommand AddStock { get; }
        public string Error { get; } = null;
        public FundCollection Funds { get; } = new FundCollection();

        public double NewStockPrice
        {
            get { return _newStockPrice; }
            set
            {
                _newStockPrice = value;
                OnNotifyPropertyChanged();
            }
        }

        public double NewStockQuantity
        {
            get { return _newStockQuantity; }
            set
            {
                _newStockQuantity = value;
                OnNotifyPropertyChanged();
            }
        }

        public StockType NewStockType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            AddStock = GenerateAddStockCommand();
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(NewStockPrice):
                        return ErrorForNewStockPrice();
                    case nameof(NewStockQuantity):
                        return ErrorForNewStockQuantity();
                }
                return null;
            }
        }

        private bool CanAddStock() => string.IsNullOrEmpty(
            this[nameof(NewStockPrice)] + this[nameof(NewStockQuantity)]);

        private string ErrorForNewStockPrice()
        {
            if (NewStockPrice <= 0)
            {
                return "Stock price has to be greater than zero.";
            }
            return null;
        }

        private string ErrorForNewStockQuantity()
        {
            if (NewStockQuantity <= 0)
            {
                return "Stock quantity has to be greater than zero.";
            }
            return null;
        }

        private ICommand GenerateAddStockCommand()
        {
            var addStockCommand = new DelegateCommand(() =>
                {
                    Stock stock = null;
                    switch (NewStockType)
                    {
                        case StockType.Bond:
                            stock = new BondStock();
                            break;
                        case StockType.Equity:
                            stock = new EquityStock();
                            break;
                        default:
                            throw new Exception("Unknown stock type: " + NewStockType);
                    }
                    stock.Price = NewStockPrice;
                    stock.Quantity = NewStockQuantity;
                    Funds.Add(stock);
                },
                () => CanAddStock());

            PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(NewStockPrice):
                    case nameof(NewStockQuantity):
                        addStockCommand.RaiseCanExecuteChanged();
                        break;
                }
            };

            return addStockCommand;
        }

        private void OnNotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}