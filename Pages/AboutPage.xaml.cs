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
            if(Spicetify.Instance.Detected)
                InitControls();
        }

        public void InitControls()
        {
            SpicetifyVersionLabel.Content = Spicetify.Instance.Version;
            BackupVersionLabel.Content = Spicetify.Instance.Settings.BackupVersion + " with " + Spicetify.Instance.Settings.WithVersion;
        }

        private void CheckVersionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CheckManagerVersion();
            CheckSpicetifyVersion();
        }

        private async void CheckManagerVersion()
        {
            Logger.Log("Checking manager version...");
            var latestTag = await VersionChecker.GetLatestTag("AdotBdot", "SpicetifyManager");
            var text = latestTag == (string)FindResource("AppVersion") ? "You are up to date." : "Version " + latestTag + " available.";
            AppVersionStateLabel.Content = text;
        }

        private async void CheckSpicetifyVersion()
        {
            Logger.Log("Checking spicetify version...");
            var latestTag = await VersionChecker.GetLatestTag("spicetify", "spicetify-cli");
            var text = (latestTag == "v" + Spicetify.Instance.Version) ? "You are up to date." : "Version " + latestTag + " available.";
            SpicetifyVersionStateLabel.Content = text;
        }
    }
}