using Sweeper.Core.Diagnostics.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeper.Core.Diagnostics
{
    public static class DriveManager
    {
        public static string GetSystemDrive()
        {
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            systemPath = Path.GetPathRoot(systemPath);

            return systemPath;
        }

        public static List<DriveInformation> GetDrives()
        {
            List<DriveInformation> result = new List<DriveInformation>();

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    result.Add(new DriveInformation(drive));
                }
            }

            return result;
        }
    }
}
