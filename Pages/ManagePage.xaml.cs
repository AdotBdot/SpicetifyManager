using System.Threading.Tasks;
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

        private void UpgradeOnly_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Upgrade());
        }

        private void FullUpgrade_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.FullUpgrade());
        }

        private void RestartBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Restart());
        }

        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Backup());
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear the backup?", "Confirmation", MessageBoxButton.YesNoCancel);
            if(result == MessageBoxResult.Yes)
                Task.Run(() => StaticData.Spicetify.Clear());
        }

        private void RestoreBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Restore());
        }
    }
}