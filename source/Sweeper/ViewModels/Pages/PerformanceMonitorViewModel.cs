using Sweeper.Bases;
using Sweeper.Commands;
using Sweeper.Core.Diagnostics;
using Sweeper.Core.Diagnostics.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
        #region ::Fields & Properties::

        private PerformanceMonitor _monitor = new PerformanceMonitor();

        private ObservableCollection<double> _cpuGraphPoints = new ObservableCollection<double>();
        private ObservableCollection<double> _memoryGraphPoints = new ObservableCollection<double>();

        public List<DriveInformation> _drives = DriveManager.GetDrives();

        public ObservableCollection<double> CpuGraphPoints
        {
            get
            {
                return _cpuGraphPoints;
            }
            set
            {
                _cpuGraphPoints = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<double> MemoryGraphPoints
        {
            get
            {
                return _memoryGraphPoints;
            }
            set
            {
                _memoryGraphPoints = value;
                RaisePropertyChanged();
            }
        }

        public List<DriveInformation> Drives
        {
            get
            {
                return _drives;
            }
            set
            {
                _drives = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _refreshDrivesCommand;

        public ICommand RefreshDrivesCommand
        {
            get
            {
                return (_refreshDrivesCommand) ?? (_refreshDrivesCommand = new DelegateCommand(RefreshDrives));
            }
        }

        private ICommand _optimizeCommand;

        public ICommand OptimizeCommand
        {
            get
            {
                return (_optimizeCommand) ?? (_optimizeCommand = new DelegateCommand(Optimize));
            }
        }

        #endregion

        #region ::Constructors::

        public PerformanceMonitorViewModel()
        {
            _monitor.Timer.Elapsed += OnTimerElapsed;
            _monitor.Start();
        }

        #endregion

        #region ::Command Actions::

        private void RefreshDrives()
        {
            Drives = DriveManager.GetDrives();
        }

        private void Optimize()
        {

        }

        #endregion

        #region ::Event Subscribers::

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            List<double> cpuTemp = CpuGraphPoints.ToList();
            List<double> memoryTemp = MemoryGraphPoints.ToList();
            cpuTemp.Add(_monitor.GetCPURate());
            memoryTemp.Add(_monitor.GetMemoryRate());

            if (cpuTemp.Count > 61)
            {
                cpuTemp.RemoveRange(0, cpuTemp.Count - 61);
            }

            if (memoryTemp.Count > 61)
            {
                memoryTemp.RemoveRange(0, memoryTemp.Count - 61);
            }

            CpuGraphPoints = new ObservableCollection<double>(cpuTemp);
            MemoryGraphPoints = new ObservableCollection<double>(memoryTemp);
        }

        #endregion
    }
}
