using Sweeper.Controls.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sweeper.Controls
{
    /// <summary>
    /// CarouselControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CarouselControl : UserControl, INotifyPropertyChanged
    {
        #region ::Fields & Properties::

        private int _currentIndex = 0;

        // Dependency property for CarouselPageCollection.
        public static readonly DependencyProperty PagesProperty = DependencyProperty.Register("Pages", typeof(List<CarouselPage>), typeof(CarouselControl), new PropertyMetadata());

        public List<CarouselPage> Pages
        {
            get
            {
                return (List<CarouselPage>)GetValue(PagesProperty);
            }
            set
            {
                SetValue(PagesProperty, value);
                RaisePropertyChanged();
            }
        }

        #endregion

        #region ::Constructors::

        public CarouselControl()
        {
            InitializeComponent();

            // Add a loaded event subscriber to use content frame navigator.
            this.Loaded += new RoutedEventHandler((sender, e) =>
            {
                // Initialize content frame.
                List<CarouselPage> pages = Pages;

                if (pages != null && pages.Count != 0)
                {
                    contentFrame.Navigate(pages[0].Content);
                }
            });
        }

        #endregion

        #region ::Animations::

        private void PlayContentTransitionAnimation()
        {
            // Initialize animation settings.
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0.0;
            animation.To = 1.0;
            animation.AccelerationRatio = 1;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            animation.FillBehavior = FillBehavior.Stop;

            contentFrame.BeginAnimation(OpacityProperty, animation);
        }

        #endregion

        #region ::Event Subscribers::

        private void OnGridMouseEnter(object sender, MouseEventArgs e)
        {
            // Set Visibility.
            previousButton.Visibility = Visibility.Visible;
            nextButton.Visibility = Visibility.Visible;

            // Initialize animation settings.
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0.0;
            animation.To = 1.0;
            animation.AccelerationRatio = 1;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            animation.FillBehavior = FillBehavior.Stop;

            // Begin animation.
            previousButton.BeginAnimation(OpacityProperty, animation);
            nextButton.BeginAnimation(OpacityProperty, animation);
        }

        private void OnGridMouseLeave(object sender, MouseEventArgs e)
        {
            // Initialize animation settings.
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 1.0;
            animation.To = 0.0;
            animation.AccelerationRatio = 1;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            animation.FillBehavior = FillBehavior.Stop;

            // EVENT : Called when the animation is completed.
            animation.Completed += new EventHandler((sender, e) =>
            {
                previousButton.Visibility = Visibility.Hidden;
                nextButton.Visibility = Visibility.Hidden;
            });

            // Begin animation.
            previousButton.BeginAnimation(OpacityProperty, animation);
            nextButton.BeginAnimation(OpacityProperty, animation);
        }

        private void OnPreviousClick(object sender, RoutedEventArgs e)
        {
            List<CarouselPage> pages = Pages;
            
            // Handle some exceptions.
            if (pages == null)
            {
                return;
            }

            if (pages.Count <= 1)
            {
                return;
            }

            if (_currentIndex < 0 || _currentIndex >= pages.Count)
            {
                return;
            }

            // Set current index.
            if (_currentIndex == 0)
            {
                _currentIndex = pages.Count - 1;
            }
            else
            {
                _currentIndex--;
            }

            // Set content.
            contentFrame.Navigate(pages[_currentIndex].Content);
            PlayContentTransitionAnimation();
        }

        private void OnNextClick(object sender, RoutedEventArgs e)
        {
            List<CarouselPage> pages = Pages;

            // Handle some exceptions.
            if (pages == null)
            {
                return;
            }

            if (pages.Count <= 1)
            {
                return;
            }

            if (_currentIndex < 0 || _currentIndex >= pages.Count)
            {
                return;
            }

            // Set current index.
            if (_currentIndex == pages.Count - 1)
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex++;
            }

            // Set content.
            contentFrame.Navigate(pages[_currentIndex].Content);
            PlayContentTransitionAnimation();
        }

        #endregion

        #region ::INotifyPropertyChanged Members::

        /// <summary>
        /// 속성 값이 변경되었을 때 호출되는 이벤트입니다.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 속성 값이 변경되었음을 알립니다.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            // take a copy to prevent thread issues.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
