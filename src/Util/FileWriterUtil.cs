using NowPlayingMonitor.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NowPlayingMonitor
{
    public class FileWriterUtil
    {
        public enum Mode
        {
            CreateNew = 1,
            Create = 2,
            Open = 3,
            OpenOrCreate = 4,
            Truncate = 5,
            Append = 6
        }

        public static void WriteToFile(string filePath, string content, Mode mode = Mode.CreateNew)
        {
            try
            {
                WriteToFileWithRetry(filePath, content, mode, 8, 500);
            }
            catch(IOException) 
            {
                throw;
            }
        }

        public static void WriteToFileWithRetry(string filePath, string content, Mode mode, int maxRetries, 
            int delayMilisecond)
        {
            if (String.IsNullOrEmpty(filePath) || String.IsNullOrEmpty(content))
                return;

            PathUtil.CreateParentDirectory(filePath);

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    TryWriteToFile(filePath, content, mode);
                    return;
                }
                catch (IOException)
                {
                    Thread.Sleep(delayMilisecond); 
                }
            }

            throw new IOException($"Failed to write in {filePath} after trying {maxRetries} time");
        }

        protected static void TryWriteToFile(string filePath, string content, Mode mode)
        {
            using var fileStream = new FileStream(filePath, (FileMode)mode, FileAccess.Write, FileShare.None);
            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(content);
            }
            return;
        }

    }
}
