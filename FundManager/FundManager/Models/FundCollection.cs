using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace FundManager.Models
{
    public class FundCollection : ObservableCollection<Stock>
    {
        private class StockSummary
        {
            public int Count { get; set; }
            public double TotalValue { get; set; }
        }

        private IDictionary<StockType, StockSummary> _stockRegistry = new Dictionary<StockType, StockSummary>();

        public double TotalValue { get { return _stockRegistry.Values.Select(s => s.TotalValue).Sum(); } }

        public FundCollection()
        {
            CollectionChanged += FundCollection_CollectionChanged;
        }

        public double GetTotalValue(StockType type)
        {
            return GetRegistry(type)?.TotalValue ?? 0;
        }

        private void FundCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                RegisterStocks(args.NewItems);
            }
        }

        private StockSummary GetRegistry(StockType type)
        {
            return _stockRegistry.Keys.Contains(type)
                ? _stockRegistry[type]
                : null;
        }

        private StockSummary GetSummary(StockType type)
        {
            if (!_stockRegistry.Keys.Contains(type))
            {
                _stockRegistry.Add(type, new StockSummary());
            }

            return _stockRegistry[type];
        }

        private void NotifyStocks(Stock stock)
        {
            foreach(var s in this)
            {
                if (s != stock)
                {
                    s.TriggerWeightNotification();
                }
            }
        }

        private void Register(Stock stock)
        {
            var summary = GetSummary(stock.Type);

            summary.TotalValue += stock.MarketValue;
            int id = ++summary.Count;

            stock.Name = stock.Type.ToString() + id;
            stock.Funds = this;

            NotifyStocks(stock);
        }

        private void RegisterStocks(IList newItems)
        {
            foreach (var item in newItems)
            {
                var stock = item as Stock;
                if (stock != null)
                {
                    Register(stock);
                }
            }
        }
    }
}