using ModernWpf.Controls;
using SpicetifyManager.Source;

namespace SpicetifyManager.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            if(StaticData.Spicetify.Detected)
                InitControls();
        }

        public void InitControls()
        {
            Prefs.Text = StaticData.Settings.PrefsPath;
            SpotifyPath.Text = StaticData.Settings.SpotifyPath;
            LaunchFlags.Text = StaticData.Settings.SpotifyLaunchFlags;

            OverwriteAssets.IsChecked = StaticData.Settings.OverwriteAssets;
            CheckSpicetifyUpgrade.IsChecked = StaticData.Settings.CheckSpicetifyUpgrade;
            InjectCss.IsChecked = StaticData.Settings.InjectCss;
            ReplaceColors.IsChecked = StaticData.Settings.ReplaceColors;
            HomeConfig.IsChecked = StaticData.Settings.HomeConfig;
            SidebarConfig.IsChecked = StaticData.Settings.SidebarConfig;
            ExperimentalFeatures.IsChecked = StaticData.Settings.ExperimentalFeatures;

            DisableSentry.IsChecked = StaticData.Settings.DisableSentry;
            DisableUILogging.IsChecked = StaticData.Settings.DisableUiLogging;
            RemoveRTLRules.IsChecked = StaticData.Settings.RemoveRtlRule;
            ExposeAPIs.IsChecked = StaticData.Settings.ExposeApis;
            DisableUpgradeCheck.IsChecked = StaticData.Settings.DisableUpgradeCheck;
        }

        public void ReadInput()
        {
            StaticData.Settings.PrefsPath = Prefs.Text;
            StaticData.Settings.SpotifyPath = SpotifyPath.Text;
            StaticData.Settings.SpotifyLaunchFlags = LaunchFlags.Text;

            StaticData.Settings.OverwriteAssets = (bool)OverwriteAssets.IsChecked;
            StaticData.Settings.CheckSpicetifyUpgrade = (bool)CheckSpicetifyUpgrade.IsChecked;
            StaticData.Settings.InjectCss = (bool)InjectCss.IsChecked;
            StaticData.Settings.ReplaceColors = (bool)ReplaceColors.IsChecked;
            StaticData.Settings.HomeConfig = (bool)HomeConfig.IsChecked;
            StaticData.Settings.SidebarConfig = (bool)SidebarConfig.IsChecked;
            StaticData.Settings.ExperimentalFeatures = (bool)ExperimentalFeatures.IsChecked;

            StaticData.Settings.DisableSentry = (bool)DisableSentry.IsChecked;
            StaticData.Settings.DisableUiLogging = (bool)DisableUILogging.IsChecked;
            StaticData.Settings.RemoveRtlRule = (bool)RemoveRTLRules.IsChecked;
            StaticData.Settings.ExposeApis = (bool)ExposeAPIs.IsChecked;
            StaticData.Settings.DisableUpgradeCheck = (bool)DisableUpgradeCheck.IsChecked;
        }
    }
}