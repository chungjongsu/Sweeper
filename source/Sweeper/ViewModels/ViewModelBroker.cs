using Sweeper.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeper.ViewModels
{
    public class ViewModelBroker
    {
        #region ::Singleton Members::

        public static ViewModelBroker _instance = null;

        public static ViewModelBroker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ViewModelBroker();
                }

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        #endregion

        #region ::Fields & Properties::

        private MainViewModel _main = new MainViewModel();

        public MainViewModel Main
        {
            get
            {
                return _main;
            }
            set
            {
                _main = value;
            }
        }

        #endregion
    }
}
