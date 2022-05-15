using System;

namespace SpicetifyManager
{
    public class Logger
    {
        public static event EventHandler<LogEventArgs> Logged;

        public static void Log(string text, bool intend = false)
        {
            Logged?.Invoke(null, intend == true ? new LogEventArgs("  " + text) : new LogEventArgs(text));
        }
    }

    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string text)
        {
            Text = text;
        }

        public string Text{get; set;}
    }
}