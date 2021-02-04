using Sweeper.Core.Drawings.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sweeper.Core.Drawings
{
    public static class FileManager
    {
        public static ImageSource GetImageSource(string filename)
        {
            try
            {
                return FileManager.GetImageSource(filename, new Size(16, 16));
            }
            catch
            {
                throw;
            }
        }

        public static ImageSource GetImageSource(string filename, Size size)
        {
            try
            {
                using (var icon = ShellManager.GetIcon(Path.GetExtension(filename), ItemType.File, IconSize.Small, ItemState.Undefined))
                {
                    return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight((int)size.Width, (int)size.Height));
                }
            }
            catch
            {
                throw;
            }
        }
    }

}
