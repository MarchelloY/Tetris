using DefaultNamespace;
using Diagnostics;
using UnityEngine;

namespace Tools.Managers
{
    public class LogManager : Singleton<LogManager>
    {
        private bool _isInitialized;

        public override void Init()
        {
            if (!_isInitialized)
            {
                Debugger.SubscribeOnLogReceived(LogToUnity);
                _isInitialized = true;
            }
        }

        private void LogToUnity(LogEntry logEntry)
        {
            var message = CreateLogString(logEntry);
            switch (logEntry.LogType)
            {
                case LogType.Log:
                    UnityEngine.Debug.Log(message);
                    break;
                case LogType.Warning:
                    UnityEngine.Debug.LogWarning(message);
                    break;
                case LogType.Error:
                    break;
                case LogType.Assert:
                    break;
                case LogType.Exception:
                    break;
                default:
                    UnityEngine.Debug.LogError(message);
                    break;
            }
        }

        private string CreateLogString(LogEntry logEntry)
        {
            var coloredMessage = $"<color={logEntry.Color}>{logEntry.LogCategory}: {logEntry.Message}</color>";

            return coloredMessage;
        }
    }
}