using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Sweeper.Styles
{
    public partial class SweeperWindow : ResourceDictionary
    {
        #region ::Fields & Properties::

        Window _window;

        #endregion

        #region ::Constructors::

        public SweeperWindow()
        {

        }

        #endregion

        #region ::Methods::

        private bool GetWindow()
        {
            if (_window == null || _window != null)
            {
                _window = System.Windows.Application.Current.MainWindow;
                return true;
            }
            else
                return false;
        }

        #endregion

        #region ::Event Subscribers::

        private void OnMinimize(object sender, RoutedEventArgs e)
        {
            if (GetWindow())
                _window.WindowState = WindowState.Minimized;
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            if (GetWindow())
                _window.Close();
        }

        #endregion
    }
}
