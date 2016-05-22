using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FundManager.Models
{
    public class StockSummary : INotifyPropertyChanged
    {
        private int _count;
        private double _totalValue;

        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }

        public StockSummary TotalSummary { get; }

        public double TotalValue
        {
            get { return _totalValue; }
            set
            {
                _totalValue = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalWeight));
            }
        }

        public double TotalWeight
        {
            get
            {
                if (TotalSummary == null)
                {
                    return 100;
                }
                if (TotalSummary.TotalValue == 0)
                {
                    return 0;
                }
                return 100 * TotalValue / TotalSummary.TotalValue;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public StockSummary() { }

        public StockSummary(StockSummary totalSummary)
        {
            TotalSummary = totalSummary;
            TotalSummary.PropertyChanged += TotalSummary_PropertyChanged;
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TotalSummary_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TotalValue))
            {
                OnPropertyChanged(nameof(TotalWeight));
            }
        }
    }
}
