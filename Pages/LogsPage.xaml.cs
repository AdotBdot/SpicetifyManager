using System;
using System.Windows;
using Page = ModernWpf.Controls.Page;

namespace SpicetifyManager.Pages
{
    /// <summary>
    /// Interaction logic for LogsPage.xaml
    /// </summary>
    public partial class LogsPage : Page
    {
        public LogsPage()
        {
            InitializeComponent();
            Logger.Logged += Logger_Logged;
        }

        private void Logger_Logged(object? sender, LogEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => LogBox.AppendText(e.Text + "\n")));
        }

        private void LogBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            LogBox.Focus();
            LogBox.CaretIndex = LogBox.Text.Length;
            LogBox.ScrollToEnd();
        }
    }
}