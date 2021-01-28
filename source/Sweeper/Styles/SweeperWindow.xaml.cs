using System.Windows;

namespace Sweeper.Styles
{
    public partial class SweeperWindow : ResourceDictionary
    {
        Window _window;

        public SweeperWindow()
        {

        }

        private bool GetWindow()
        {
            if (_window == null || _window != null)
            {
                _window = Application.Current.MainWindow;
                return true;
            }
            else
                return false;
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            if (GetWindow())
                _window.WindowState = WindowState.Minimized;
        }

        private void Maximize(object sender, RoutedEventArgs e)
        {
            if (GetWindow())
            {
                if (_window.WindowState == WindowState.Maximized)
                    _window.WindowState = WindowState.Normal;
                else
                    _window.WindowState = WindowState.Maximized;
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            if (GetWindow())
                _window.Close();
        }
    }
}
