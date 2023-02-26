using System;
using System.Reflection;

namespace SpicetifyManager
{
    public class Logger
    {
        public static event EventHandler<LogEventArgs> Logged;

        public static void Log(string text, bool indent = false)
        {
            Logged?.Invoke(null, indent ? new LogEventArgs(INDENT + text) : new LogEventArgs(text));
            Console.WriteLine(indent ? INDENT + text : text);
        }

        private const string INDENT = "   ";
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