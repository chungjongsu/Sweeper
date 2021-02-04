using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Sweeper.Core.Diagnostics
{
    public class PerformanceMonitor : IDisposable
    {
        #region ::Fields & Properties::

        private PerformanceCounter _cpuCounter;
        private PerformanceCounter _memoryCounter;
        private PerformanceCounter _diskIOCounter;
        public Timer Timer { get; set; }

        #endregion

        #region ::Constructors::

        public PerformanceMonitor()
        {
            // Initialize counters.
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            _memoryCounter = new PerformanceCounter("Memory", "Available KBytes", true);

            string drive = DriveManager.GetSystemDrive();
            string[] instanceNameArray = new PerformanceCounterCategory("PhysicalDisk").GetInstanceNames();
            string instanceName = instanceNameArray.FirstOrDefault(s => s.IndexOf(drive) > -1);
            _diskIOCounter = new PerformanceCounter("PhysicalDisk", "% Idle Time", instanceName, true);

            // Initialize timer.
            Timer = new Timer(1000);
        }

        #endregion

        #region ::Timer::

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }

        #endregion

        #region ::Methods::

        public float GetCPURate()
        {
            float rate = _cpuCounter.NextValue();
            rate = Math.Min(100f, Math.Max(0f, rate));

            return rate;
        }

        public float GetMemoryRate()
        {
            using (ManagementClass mc = new ManagementClass("Win32_OperatingSystem"))
            {
                using (ManagementObject o = mc.GetInstances().Cast<ManagementObject>().FirstOrDefault())
                {
                    float physicalMemorySize = float.Parse(o["TotalVisibleMemorySize"].ToString());
                    float rate = ((physicalMemorySize - _memoryCounter.NextValue()) / physicalMemorySize) * 100;
                    rate = Math.Min(100f, Math.Max(0f, rate));

                    return rate;
                }
            }
        }

        public float GetDiskIORate()
        {
            float rate = 100 - _diskIOCounter.NextValue();
            rate = Math.Min(100f, Math.Max(0f, rate));

            return rate;
        }

        #endregion

        #region ::IDisposable Members::

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _cpuCounter.Dispose();
                    _memoryCounter.Dispose();
                    _diskIOCounter.Dispose();
                    Timer.Stop();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~PerformanceMonitor()
        // {
        //     // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
