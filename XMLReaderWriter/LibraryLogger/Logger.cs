using System;
using System.Globalization;
using System.IO;
using XMLReaderWriter.Resources;

namespace XMLReaderWriter.LibraryLogger
{    /// <summary>
     /// A class that provides logging using package
     /// </summary>
    public static class Logger
    {
        private static readonly string FilePath = LogerPath.FileToLogName + DateTime.Today.Date.ToString("dd-MM-yyyy") + LogerPath.FileToLogExtention; 

        /// <summary>
        /// loggs information information
        /// </summary>
        /// <param name="paramObjects">information that will logged</param>
        public static void Log(params object[] paramObjects)
        {
            if (paramObjects == null) return;
            try
            {
                var dir = Path.GetDirectoryName(FilePath);
                if (dir == null) return;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                using (var stream = File.Exists(FilePath)
                        ? File.OpenWrite(FilePath)
                        : File.Create(FilePath))
                {
                    stream.Position = stream.Length;
                    using (var writer = new StreamWriter(stream))
                    {
                        foreach (var param in paramObjects)
                        {
                            writer.Write(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss : "));
                            writer.WriteLine(param);
                        }

                        writer.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
