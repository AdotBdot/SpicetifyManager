using System;

namespace SpicetifyManager
{
    public class Logger
    {
        public static event EventHandler<LogEventArgs> Logged;

        public static void Log(string text, bool indent = false)
        {
            Logged?.Invoke(null, indent == true ? new LogEventArgs("   " + text) : new LogEventArgs(text));
            Console.WriteLine(indent == true ? "   " + text : text);
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