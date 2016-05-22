using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FundManager.Models
{
    public abstract class Stock : INotifyPropertyChanged
    {
        public FundCollection Funds { get; set; }
        public bool Highlight { get { return MarketValue < 0 || TransactionCost > Tolerance; } }
        public double MarketValue { get { return Price * Quantity; } }
        public string Name { get; set; }
        public double Price { get; set; }
        public abstract double Ratio { get; }
        public double Quantity { get; set; }
        public double TransactionCost { get { return MarketValue * Ratio; } }
        public abstract double Tolerance { get; }
        public abstract StockType Type { get; }

        public double Weight
        {
            get
            {
                if (Funds == null)
                {
                    return 0;
                }

                var total = Funds.TotalValue;
                if (total == 0)
                {
                    return 0;
                }

                return 100 * MarketValue / total;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void TriggerWeightNotification()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weight)));
        }
    }
}