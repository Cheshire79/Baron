using UnityEngine;

namespace Uniject.Impl
{
    public class UnityLogger : ILogger
    {
        public string prefix { get; set; }

        #region ILogger implementation

        public void Log(string message)
        {
            if (null != prefix)
            {
                Debug.Log(prefix);
            }
            Debug.Log(message);
            
        }

        public void Log(string message, object[] args)
        {
            Log(string.Format(message, args));
        }
        
        public void LogError(string message)
        {
            if (null != prefix)
            {
                Debug.LogError(prefix);
            }
            Debug.LogError(message);

        }

        public void LogError(string message, object[] args)
        {
            LogError(string.Format(message, args));
        }

        public void LogWarning(string message)
        {
            if (null != prefix)
            {
                Debug.LogWarning(prefix);
            }
            Debug.LogWarning(message);

        }

        public void LogWarning(string message, object[] args)
        {
            LogWarning(string.Format(message, args));
        }

        #endregion
    }
}