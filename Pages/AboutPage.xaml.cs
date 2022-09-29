using System.Windows;
using ModernWpf.Controls;
using SpicetifyManager.Source;

namespace SpicetifyManager.Pages
{
    /// <summary>
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            if(StaticData.Spicetify.Detected)
                InitControls();
        }

        public void InitControls()
        {
            AppVersionLabel.Content = StaticData.Version;
            SpicetifyVersionLabel.Content = StaticData.Spicetify.Version;
            BackupVersionLabel.Content = StaticData.Settings.BackupVersion;
        }

        private void CheckVersionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CheckManagerVersion();
            CheckSpicetifyVersion();
        }

        private async void CheckManagerVersion()
        {
            var latestTag = await VersionChecker.GetLastTag("AdotBdot", "SpicetifyManager");
            var text = (latestTag == StaticData.Version) ? "You are up to date." : "Version " + latestTag + " available.";
            AppVersionStateLabel.Content = text;
        }

        private async void CheckSpicetifyVersion()
        {
            var latestTag = await VersionChecker.GetLastTag("spicetify", "spicetify-cli");
            var text = (latestTag == "v" + StaticData.Spicetify.Version) ? "You are up to date." : "Version " + latestTag + " available.";
            SpicetifyVersionStateLabel.Content = text;
        }
    }
}