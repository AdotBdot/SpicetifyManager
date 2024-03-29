﻿using System.Threading.Tasks;
using System.Windows;
using ModernWpf.Controls;
using SpicetifyManager.Source;

namespace SpicetifyManager.Pages
{
    /// <summary>
    /// Interaction logic for ManagePage.xaml
    /// </summary>
    public partial class ManagePage : Page
    {
        public ManagePage()
        {
            InitializeComponent();
        }

        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.Backup());
        }
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear the backup?", "Confirmation", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
                Task.Run(() => Spicetify.Instance.Clear());
        }
        private void DevToolsBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.EnableDevTools());
        }
        private void RestartBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.Restart());
        }
        private void RestoreBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.Restore());
        }
        private void UpgradeOnly_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.Upgrade());
        }
        private void FullUpgrade_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.FullUpgrade());
        }
    }
}