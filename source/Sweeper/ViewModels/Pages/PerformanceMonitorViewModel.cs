using Sweeper.Bases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sweeper.ViewModels.Pages
{
    public class PerformanceMonitorViewModel : ViewModelBase
    {
        private ObservableCollection<double> _graphPoints = new ObservableCollection<double>();

        public ObservableCollection<double> GraphPoints
        {
            get
            {
                return _graphPoints;
            }
            set
            {
                _graphPoints = value;
                RaisePropertyChanged();
            }
        }

        public PerformanceMonitorViewModel()
        {
            Random r = new Random();

            double width = 598;
            double blockSize = width / 20 / 3;

            for (int i=0; i<=29; i++)
            {
                GraphPoints.Add(r.NextDouble() * 100);
            }
        }
    }
}
