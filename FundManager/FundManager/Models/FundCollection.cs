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
        private IDictionary<StockType, StockSummary> _stockRegistry = new Dictionary<StockType, StockSummary>();

        public StockSummary BondSummary { get { return GetSummary(StockType.Bond); } }
        public StockSummary EquitySummary { get { return GetSummary(StockType.Equity); } }
        public StockSummary TotalSummary { get; set; } = new StockSummary();
        public double TotalValue { get { return _stockRegistry.Values.Select(s => s.TotalValue).Sum(); } }

        public FundCollection()
        {
            CollectionChanged += FundCollection_CollectionChanged;
        }

        public double GetTotalValue(StockType type)
        {
            return GetSummary(type)?.TotalValue ?? 0;
        }

        private void FundCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                RegisterStocks(args.NewItems);
            }
        }

        private StockSummary GetSummary(StockType type)
        {
            if (!_stockRegistry.Keys.Contains(type))
            {
                _stockRegistry.Add(type, new StockSummary(TotalSummary));
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

            UpdateSummary(summary, stock);
            UpdateSummary(TotalSummary, stock);

            stock.Name = stock.Type.ToString() + summary.Count;
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

        private static void UpdateSummary(StockSummary summary, Stock stock)
        {
            summary.TotalValue += stock.MarketValue;
            summary.Count++;
        }
    }
}