using Sweeper.Bases;
using Sweeper.Controls.Entities;
using Sweeper.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeper.ViewModels.Windows
{
    public class MainViewModel : ViewModelBase
    {
        #region ::Fields & Properties::

        private List<CarouselPage> _pages = new List<CarouselPage>();

        public List<CarouselPage> Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                _pages = value;
                RaisePropertyChanged();
            }
        }

        #endregion


        #region ::Constructors::

        public MainViewModel()
        {
            _pages.Add(new CarouselPage("Overview", new OverviewPage()));
            _pages.Add(new CarouselPage("Diagnostics", new DiagnosticsPage()));
            _pages.Add(new CarouselPage("Performance Monitor", new PerformanceMonitorPage()));
        }

        #endregion
    }
}
