using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FundManager.Models
{
    public class Stock : INotifyPropertyChanged
    {
        private static readonly IReadOnlyDictionary<StockType, double> _costRatios =
            new ReadOnlyDictionary<StockType, double>(new Dictionary<StockType, double>
            {
                [StockType.Bond] = 0.02,
                [StockType.Equity] = 0.005
            });

        public FundCollection Funds { get; set; }
        public double MarketValue { get { return Price * Quantity; } }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }

        public double TransactionCost
        {
            get
            {
                var ratio = _costRatios[Type];
                return MarketValue * ratio;
            }
        }

        public StockType Type { get; set; }

        public double Weight
        {
            get
            {
                if (Funds == null)
                {
                    return 0;
                }

                var total = Funds.GetTotalValue(Type);
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