using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sweeper.Controls
{
    /// <summary>
    /// PolygonalGraphBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PolygonalGraphBox : UserControl, INotifyPropertyChanged
    {
        #region ::Fields & Properties::

        // Dependency property for LineBrush.
        public static readonly DependencyProperty LineBrushProperty = DependencyProperty.Register("LineBrush", typeof(SolidColorBrush), typeof(PolygonalGraphBox),
            new UIPropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0)), new PropertyChangedCallback(OnPropertyChanged)));

        public SolidColorBrush LineBrush
        {
            get
            {
                return (SolidColorBrush)GetValue(LineBrushProperty);
            }
            set
            {
                SetValue(LineBrushProperty, value);
                RaisePropertyChanged();
            }
        }

        // Dependency property for FillBrush.
        public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register("FillBrush", typeof(SolidColorBrush), typeof(PolygonalGraphBox),
            new UIPropertyMetadata(new SolidColorBrush(Color.FromRgb(250, 250, 250)), new PropertyChangedCallback(OnPropertyChanged)));

        public SolidColorBrush FillBrush
        {
            get
            {
                return (SolidColorBrush)GetValue(FillBrushProperty);
            }
            set
            {
                SetValue(FillBrushProperty, value);
                RaisePropertyChanged();
            }
        }

        // Dependency property for GridBrush.
        public static readonly DependencyProperty GridBrushProperty = DependencyProperty.Register("GridBrush", typeof(SolidColorBrush), typeof(PolygonalGraphBox),
            new UIPropertyMetadata(new SolidColorBrush(Color.FromRgb(230, 230, 230)), new PropertyChangedCallback(OnPropertyChanged)));

        public SolidColorBrush GridBrush
        {
            get
            {
                return (SolidColorBrush)GetValue(GridBrushProperty);
            }
            set
            {
                SetValue(GridBrushProperty, value);
                RaisePropertyChanged();
            }
        }

        // Dependency property for ShowGrid.
        public static readonly DependencyProperty ShowGridProperty = DependencyProperty.Register("ShowGrid", typeof(bool), typeof(PolygonalGraphBox),
            new UIPropertyMetadata(true, new PropertyChangedCallback(OnShowGridChanged)));

        public bool ShowGrid
        {
            get
            {
                return (bool)GetValue(ShowGridProperty);
            }
            set
            {
                SetValue(ShowGridProperty, value);
                RaisePropertyChanged();
            }
        }

        // Dependency property for Points.
        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register("Points", typeof(ObservableCollection<double>), typeof(PolygonalGraphBox),
            new FrameworkPropertyMetadata(new ObservableCollection<double>(), (FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault), OnPointsChanged));

        public ObservableCollection<double> Points
        {
            get
            {
                return (ObservableCollection<double>)GetValue(PointsProperty);
            }
            set
            {
                SetValue(PointsProperty, value);
                RaisePropertyChanged();
            }
        }

        #endregion

        #region ::Constructors::

        public PolygonalGraphBox()
        {
            InitializeComponent();
            DrawGraph();

            SizeChanged += OnSizeChanged;
        }

        #endregion

        #region ::Methods::

        private void DrawGraph()
        {
            graphCanvas.Children.Clear();
            DrawBackground();
            DrawPoints();
        }

        private void DrawBackground()
        {
            if (ShowGrid)
            {
                double width = (ActualWidth - 2) / 20;
                double height = (ActualHeight - 2) / 10;

                if (width < 0)
                {
                    width = 10;
                }

                if (height < 0)
                {
                    height = 10;
                }

                // Initialize drawing.
                RectangleGeometry geometry = new RectangleGeometry(new Rect(0, 0, 100, 100));
                Pen pen = new Pen(GridBrush, 1.0);
                GeometryDrawing drawing = new GeometryDrawing(Background, pen, geometry);

                // Initialize brush.
                DrawingBrush brush = new DrawingBrush(drawing);
                brush.TileMode = TileMode.Tile;
                brush.Viewport = new Rect(0, 0, width, height);
                brush.ViewportUnits = BrushMappingMode.Absolute;

                graphCanvas.Background = brush;
            }
            else
            {
                graphCanvas.Background = Background;
            }
        }

        private void DrawPoints()
        {
            if (Points != null && Points.Count >= 1)
            {
                List<double> points = Points.ToList();

                if (points.Count > 61)
                {
                    points.RemoveRange(0, points.Count - 61);
                }
                else if (points.Count < 61)
                {
                    List<double> fixedPoints = new List<double>();

                    for (int i = 0; i < 61 - points.Count; i++)
                    {
                        fixedPoints.Add(0);
                    }

                    fixedPoints.AddRange(points);

                    points = fixedPoints;
                }

                double canvasWidth = ActualWidth - 2;
                double canvasHeight = ActualHeight - 2;
                double blockWidth = canvasWidth / 60;
                double blockHeight = canvasHeight / 100;
                double collisionLimitBlockSize = 1;

                // Calculate actual point.
                PointCollection pointCollection = new PointCollection(61);

                for (int i = 0; i < points.Count; i++)
                {
                    pointCollection.Add(new Point(blockWidth * i, canvasHeight - (blockHeight * points[i])));

                    // Processing line collision adjustment.
                    if ((int)Math.Round(pointCollection[i].Y) >= (int)Math.Round(canvasHeight - collisionLimitBlockSize))
                    {
                        Point point = pointCollection[i];
                        point.Y = canvasHeight;
                        pointCollection[i] = point;
                    }
                    else if ((int)Math.Round(pointCollection[i].Y) <= collisionLimitBlockSize)
                    {
                        Point point = pointCollection[i];
                        point.Y = 0;
                        pointCollection[i] = point;
                    }
                }

                Polyline polyline = new Polyline();
                polyline.VerticalAlignment = VerticalAlignment.Center;
                polyline.Stroke = LineBrush;
                polyline.StrokeThickness = 1;
                polyline.Points = pointCollection;

                graphCanvas.Children.Add(polyline);
            }
        }

        #endregion

        #region ::Event Subscribers::

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawGraph();
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolygonalGraphBox control = d as PolygonalGraphBox;
            control.DrawGraph();
        }

        private static void OnShowGridChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolygonalGraphBox control = d as PolygonalGraphBox;
            control.DrawBackground();
        }

        private static void OnPointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PolygonalGraphBox;

            if (e.NewValue != null)
            {
                control.DrawGraph();
            }
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
