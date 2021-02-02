using Sweeper.Bases;
using Sweeper.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sweeper.ViewModels.Pages
{
    public class PerformanceMonitorViewModel : ViewModelBase
    {
        private ObservableCollection<double> _graphPoints = new ObservableCollection<double>();

        private Timer _timer = new Timer();

        private PerformanceCounter _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");


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

        private ICommand _setPointsCommand;

        public ICommand SetPointsCommand
        {
            get
            {
                return (_setPointsCommand) ?? (_setPointsCommand = new DelegateCommand(SetPoints));
            }
        }

        private ICommand _startCommand;

        public ICommand StartCommand
        {
            get
            {
                return (_startCommand) ?? (_startCommand = new DelegateCommand(Start));
            }
        }

        private ICommand _sropCommand;

        public ICommand StopCommand
        {
            get
            {
                return (_sropCommand) ?? (_sropCommand = new DelegateCommand(Stop));
            }
        }

        public PerformanceMonitorViewModel()
        {
            SetPoints();
        }

        private void SetPoints()
        {
            ObservableCollection<double> points = new ObservableCollection<double>();

            Random r = new Random();

            for (int i = 0; i <= 60; i++)
            {
                points.Add(r.NextDouble() * 100);
            }

            GraphPoints = points;
        }

        private void Start()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += new ElapsedEventHandler((sender, e) =>
            {
                List<double> temp = GraphPoints.ToList();
                temp.Add(_cpuCounter.NextValue());

                if (temp.Count > 61)
                {
                    temp.RemoveRange(0, temp.Count - 61);
                }

                GraphPoints = new ObservableCollection<double>(temp);
            });
            _timer.Start();
        }

        private void Stop()
        {
            if (_timer != null && _timer.Enabled == true)
            {
                _timer.Stop();
            }
        }
    }
}
