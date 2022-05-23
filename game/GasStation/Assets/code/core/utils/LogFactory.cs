using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace gasstation.code.core.logging
{
    public class GeneralLogHandler : ILogHandler
    {
        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            Debug.unityLogger.logHandler.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            Debug.unityLogger.LogException(exception, context);
        }
    }

    class LogFactory
    {
        public static Logger MAIN_LOGGER;

        public static void Init()
        {
            LogFactory.MAIN_LOGGER = new Logger(new GeneralLogHandler());
            LogFactory.INFO("Start main logger");
        }

        public static void INFO(string data)
        {
            LogFactory.MAIN_LOGGER.Log($"INFO :: {data}");
        }

        public static void DEBUG(string data)
        {
            LogFactory.MAIN_LOGGER.Log($"DEBUG :: {data}");
        }

    }
}
