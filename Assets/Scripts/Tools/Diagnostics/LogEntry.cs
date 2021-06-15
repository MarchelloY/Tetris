using UnityEngine;

namespace Diagnostics
{
    public readonly struct LogEntry
    {
        public string LogCategory { get; }
        public string Message { get; }
        public LogType LogType { get; }
        public string Color { get; }

        public LogEntry(string logCategory, string message, LogType logType,
            string color = TextColors.WHITE)
        {
            LogCategory = logCategory;
            Message = message;
            Color = color;
            LogType = logType;
        }
    }
}