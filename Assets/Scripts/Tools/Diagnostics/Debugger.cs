using System;
using UnityEngine;
using Utils;

namespace Diagnostics
{
    public static class Debugger
    {
        private static event Action<LogEntry> _onLogReceived;

        public static void Log(string logCategory, string message,
            string color = TextColors.WHITE)
        {
            _onLogReceived.SafeInvoke(new LogEntry(logCategory, message, LogType.Log, color));
        }

        public static void LogWarning(string logCategory, string message,
            string color = TextColors.YELLOW)
        {
            _onLogReceived.SafeInvoke(new LogEntry(logCategory, message, LogType.Warning, color));
        }

        public static void LogError(string logCategory, string message,
            string color = TextColors.RED)
        {
            _onLogReceived.SafeInvoke(new LogEntry(logCategory, message, LogType.Error, color));
        }

        public static void LogError(string logCategory, Exception exception,
            string color = TextColors.RED)
        {
            var errorText = exception.GetFullMessage();

            _onLogReceived.SafeInvoke(new LogEntry(logCategory, errorText, LogType.Error, color));
        }

        public static void LogError(string logCategory, string message, Exception exception,
            string color = TextColors.RED)
        {
            var errorText = exception != null
                ? $"{message}\nError = {exception.GetFullMessage()}"
                : message;

            _onLogReceived.SafeInvoke(new LogEntry(logCategory, errorText, LogType.Error, color));
        }

        public static void LogException(string logCategory, Exception exception,
            string color = TextColors.BROWN)
        {
            var errorText = exception.GetFullMessage();

            _onLogReceived.SafeInvoke(new LogEntry(logCategory, errorText, LogType.Exception, color));
        }

        public static void SubscribeOnLogReceived(Action<LogEntry> action)
        {
            UnSubscribeOnLogReceived(action);
            _onLogReceived += action;
        }

        private static void UnSubscribeOnLogReceived(Action<LogEntry> action)
        {
            _onLogReceived -= action;
        }
    }
}