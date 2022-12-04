using ModernWpf.Controls;

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
            InitControls();
        }

        public void InitControls()
        {
            SpotifyPath.Text = Spicetify.Instance.Settings.SpotifyPath;
            Prefs.Text = Spicetify.Instance.Settings.PrefsPath;
            LaunchFlags.Text = Spicetify.Instance.Settings.SpotifyLaunchFlags;

            OverwriteAssets.IsChecked = Spicetify.Instance.Settings.OverwriteAssets;
            InjectCss.IsChecked = Spicetify.Instance.Settings.InjectCss;
            ReplaceColors.IsChecked = Spicetify.Instance.Settings.ReplaceColors;
            CheckSpicetifyUpgrade.IsChecked = Spicetify.Instance.Settings.CheckSpicetifyUpgrade;
            HomeConfig.IsChecked = Spicetify.Instance.Settings.HomeConfig;
            SidebarConfig.IsChecked = Spicetify.Instance.Settings.SidebarConfig;
            ExperimentalFeatures.IsChecked = Spicetify.Instance.Settings.ExperimentalFeatures;

            ExposeAPIs.IsChecked = Spicetify.Instance.Settings.ExposeApis;
            DisableSentry.IsChecked = Spicetify.Instance.Settings.DisableSentry;
            DisableUILogging.IsChecked = Spicetify.Instance.Settings.DisableUiLogging;
            DisableUpgradeCheck.IsChecked = Spicetify.Instance.Settings.DisableUpgradeCheck;
            RemoveRTLRules.IsChecked = Spicetify.Instance.Settings.RemoveRtlRule;
        }

        public string SpotifyPathProperty
        {
            get {return Spicetify.Instance.Settings.SpotifyPath;}
            set {Spicetify.Instance.Settings.SpotifyPath = value;}
        }

        public string PrefsPathProperty
        {
            get {return Spicetify.Instance.Settings.PrefsPath;}
            set {Spicetify.Instance.Settings.PrefsPath = value;}
        }

        public string SpotifyLaunchFlagsProperty
        {
            get {return Spicetify.Instance.Settings.SpotifyLaunchFlags;}
            set {Spicetify.Instance.Settings.SpotifyLaunchFlags = value;}
        }

        public bool OverwriteAssetsProperty
        {
            get {return Spicetify.Instance.Settings.OverwriteAssets;}
            set {Spicetify.Instance.Settings.OverwriteAssets = value;}
        }

        public bool InjectCssProperty
        {
            get {return Spicetify.Instance.Settings.InjectCss;}
            set {Spicetify.Instance.Settings.InjectCss = value;}
        }

        public bool ReplaceColorsProperty
        {
            get {return Spicetify.Instance.Settings.ReplaceColors;}
            set {Spicetify.Instance.Settings.ReplaceColors = value;}
        }

        public bool CheckSpicetifyUpgradeProperty
        {
            get {return Spicetify.Instance.Settings.CheckSpicetifyUpgrade;}
            set {Spicetify.Instance.Settings.CheckSpicetifyUpgrade = value;}
        }

        public bool HomeConfigProperty
        {
            get {return Spicetify.Instance.Settings.HomeConfig;}
            set {Spicetify.Instance.Settings.HomeConfig = value;}
        }

        public bool SidebarConfigProperty
        {
            get {return Spicetify.Instance.Settings.SidebarConfig;}
            set {Spicetify.Instance.Settings.SidebarConfig = value;}
        }

        public bool ExperimentalFeaturesProperty
        {
            get {return Spicetify.Instance.Settings.ExperimentalFeatures;}
            set {Spicetify.Instance.Settings.ExperimentalFeatures = value;}
        }

        public bool ExposeApisProperty
        {
            get {return Spicetify.Instance.Settings.ExposeApis;}
            set {Spicetify.Instance.Settings.ExposeApis = value;}
        }

        public bool DisableSentryProperty
        {
            get {return Spicetify.Instance.Settings.DisableSentry;}
            set {Spicetify.Instance.Settings.DisableSentry = value;}
        }

        public bool DisableUiLoggingProperty
        {
            get {return Spicetify.Instance.Settings.DisableUiLogging;}
            set {Spicetify.Instance.Settings.DisableUiLogging = value;}
        }

        public bool DisableUpgradeCheckProperty
        {
            get {return Spicetify.Instance.Settings.DisableUpgradeCheck;}
            set {Spicetify.Instance.Settings.DisableUpgradeCheck = value;}
        }

        public bool RemoveRtlRuleProperty
        {
            get {return Spicetify.Instance.Settings.RemoveRtlRule;}
            set {Spicetify.Instance.Settings.RemoveRtlRule = value;}
        }
    }
}