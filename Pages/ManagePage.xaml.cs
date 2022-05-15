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

        private void UpgradeOnly_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Upgrade());
        }

        private void FullUpgrade_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.FullUpgrade());
        }

        private void RestartBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Restart());
        }

        private void BackupBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Backup());
        }

        private void ClearBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Clear());
        }

        private void RestoreBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Restore());
        }
    }
}