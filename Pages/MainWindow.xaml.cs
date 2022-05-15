using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using SpicetifyManager.Source;
using Page = ModernWpf.Controls.Page;

namespace SpicetifyManager.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigate(StaticData.Spicetify.Detected ? "Nav_StartPage" : "Nav_SpicetifyNotDetectedPage");
        }

        //Navigation
        public readonly Dictionary<string, Page> Pages = new()
        {
            { "Nav_StartPage", new StartPage() },
            { "Nav_Themes", new ThemesPage() },
            { "Nav_Plugins", new PluginsPage() },
            { "Nav_Manage", new ManagePage() },
            { "Nav_Settings", new SettingsPage() },
            { "Nav_About", new AboutPage() },
            { "Nav_Logs", new LogsPage() },
            { "Nav_ReloadBtn", new Page() },
            { "Nav_SpicetifyNotDetectedPage", new SpicetifyNotDetectedPage() }
        };

        private void Navigate(string navItemTag)
        {
            ContentFrame.Navigate(StaticData.Spicetify.Detected ? Pages[navItemTag] : Pages["Nav_SpicetifyNotDetectedPage"]);
        }

        private void ContentFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            if(ContentFrame.Content != null)
            {
                KeyValuePair<string, Page> item = Pages.FirstOrDefault(p => p.Value == e.Content);

                foreach(Button b in NavBar.Children.OfType<Button>())
                {
                    if(b.Tag.Equals(item.Key + "Btn"))
                        b.Style = (Style)FindResource("NavBtnSelected");
                    else
                        b.Style = (Style)FindResource("NavBtn");
                }
            }
        }

        //Buttons
        private void ApplyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.Apply());
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ((ThemesPage)Pages["Nav_Themes"]).ReadInput();
            ((PluginsPage)Pages["Nav_Plugins"]).ReadInput();
            ((SettingsPage)Pages["Nav_Settings"]).ReadInput();

            Task.Run(SaveAll);
        }

        //Other
        private void SaveAll()
        {
            StaticData.Settings.SaveThemes();
            StaticData.Settings.SavePlugins();
            StaticData.Settings.SaveSettings();
        }

        private void Reload()
        {
            StaticData.Reload();

            ((ThemesPage)Pages["Nav_Themes"]).InitThemesListBox();
            ((ThemesPage)Pages["Nav_Themes"]).InitColorSchemesBox();
            ((PluginsPage)Pages["Nav_Plugins"]).InitPluginsList();
            ((PluginsPage)Pages["Nav_Plugins"]).InitCustomAppsList();
            ((SettingsPage)Pages["Nav_Settings"]).InitControls();
            ((AboutPage)Pages["Nav_About"]).InitControls();
        }

        //Bottom Panel
        private void UpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ((ThemesPage)Pages["Nav_Themes"]).ReadInput();
            StaticData.Settings.SaveThemes();

            Task.Run(() => StaticData.Spicetify.Update());
        }

        private void ThemesFolder_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.OpenThemeFolder());
        }

        private void PluginsFolder_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.OpenExtensionsFolder());
        }

        private void CustomAppsFolder_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.OpenCustomAppsFolder());
        }

        private void ConfigFile_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => StaticData.Spicetify.OpenConfigFile());
        }

        private void ReloadBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        //NavBar Buttons
        private void ThemesBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Nav_Themes");
        }

        private void PluginsBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Nav_Plugins");
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Nav_Settings");
        }

        private void ManageBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Nav_Manage");
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Nav_About");
        }

        private void LogsBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Nav_Logs");
        }
    }
}