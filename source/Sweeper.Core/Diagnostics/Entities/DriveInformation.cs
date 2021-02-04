using Sweeper.Core.Drawings;
using Sweeper.Core.Drawings.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sweeper.Core.Diagnostics.Entities
{
    public class DriveInformation
    {
        #region ::Properties::

        public ImageSource DriveIcon { get; set; }

        public string Name { get; set; }

        public string Format { get; set; }

        public string Type { get; set; }

        public long TotalFreeSpace { get; set; }

        public long TotalSize { get; set; }

        public long UsedSpace { get; set; }

        public int UsagePercentage { get; set; }

        public string Information { get; set; }

        #endregion

        #region ::Constructors::

        public DriveInformation()
        {

        }

        public DriveInformation(DriveInfo drive)
        {
            DriveIcon = FolderManager.GetImageSource(drive.RootDirectory.FullName, new Size(300, 300), ItemState.Undefined);
            Name = drive.Name;
            Format = drive.DriveFormat;
            Type = drive.DriveType.ToString();
            TotalFreeSpace = drive.TotalFreeSpace;
            TotalSize = drive.TotalSize;
            UsedSpace = drive.TotalSize - drive.TotalFreeSpace;
            UsagePercentage = 100 - (int)Math.Round((double)drive.TotalFreeSpace / (double)drive.TotalSize * 100);
            Information = $"{Name} ({Format}, {Type})\r\n" +
                $" * Total Space : {FileManager.GetFileSize(TotalSize)}\r\n" +
                $" * Free Space : {FileManager.GetFileSize(TotalFreeSpace)}\r\n" +
                $" * Used Space : {FileManager.GetFileSize(UsedSpace)} ({UsagePercentage}%)";
        }

        #endregion
    }
}
