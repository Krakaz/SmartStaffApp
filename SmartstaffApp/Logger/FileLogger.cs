using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace SmartstaffApp.Logger
{
    public class FileLogger : ILogger
    {
        private string filePath;
        private static object _lock = new object();
        public FileLogger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    var directory = Path.GetDirectoryName(filePath);
                    bool exists = Directory.Exists(directory);

                    if (!exists)
                        Directory.CreateDirectory(directory);

                    File.AppendAllText(filePath, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss: ") + formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
