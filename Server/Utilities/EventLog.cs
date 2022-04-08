using System;
using System.Diagnostics;

using DiagELog = System.Diagnostics.EventLog;

namespace Server
{
    public static class EventLog
    {
        static EventLog()
        {
            if (!DiagELog.SourceExists("ServUOX"))
            {
                DiagELog.CreateEventSource("ServUOX", "Application");
            }
        }

        public static void Error(int eventID, string text)
        {
            DiagELog.WriteEntry("ServUOX", text, EventLogEntryType.Error, eventID);
        }

        public static void Error(int eventID, string format, params object[] args)
        {
            Error(eventID, string.Format(format, args));
        }

        public static void Warning(int eventID, string text)
        {
            DiagELog.WriteEntry("ServUOX", text, EventLogEntryType.Warning, eventID);
        }

        public static void Warning(int eventID, string format, params object[] args)
        {
            Warning(eventID, string.Format(format, args));
        }

        public static void Inform(int eventID, string text)
        {
            DiagELog.WriteEntry("ServUOX", text, EventLogEntryType.Information, eventID);
        }

        public static void Inform(int eventID, string format, params object[] args)
        {
            Inform(eventID, string.Format(format, args));
        }
    }
}
