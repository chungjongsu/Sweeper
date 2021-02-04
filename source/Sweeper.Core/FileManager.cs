using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeper.Core
{
    public static class FileManager
    {
        public static string GetFileSize(long length)
        {
            double byteCount = length;

            string size = "0 Byte";

            if (byteCount >= 1099511627776.0)
                size = string.Format("{0:##.##}", byteCount / 1099511627776.0) + " TB";
            else if (byteCount >= 1073741824.0)
                size = string.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = string.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = string.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Byte";

            return size;
        }

        /// <summary>
        /// Writes a text file in the specified path.
        /// </summary>
        /// <param name="filePath">Path where the text file will be saved.</param>
        /// <param name="text">Text to be writed.</param>
        /// <param name="encoding">The text encoding to use.</param>
        public static void WriteTextFile(string filePath, string text, Encoding encoding)
        {
            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                StreamWriter objSaveFile = new StreamWriter(stream, encoding);
                objSaveFile.Write(text);
                objSaveFile.Close();
                objSaveFile.Dispose();
            }
        }

        /// <summary>
        /// Reads a text file in the specified path.
        /// </summary>
        /// <param name="filePath">Path where the text file will be read.</param>
        /// <param name="encoding">The text encoding to use.</param>
        /// <returns>Text</returns>
        public static string ReadTextFile(string filePath, Encoding encoding)
        {
            string temp = string.Empty;
            using (StreamReader objReadFile = new StreamReader(filePath, encoding))
            {
                temp = objReadFile.ReadToEnd();
                objReadFile.Close();
                objReadFile.Dispose();
            }
            return temp;
        }
    }
}
