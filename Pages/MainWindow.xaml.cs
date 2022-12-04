﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
            if(Spicetify.Instance.Detected)
                Navigate("Start");
            else
            {
                ApplyBtn.IsEnabled = false;
                SaveBtn.IsEnabled = false;
                UpdateBtn.IsEnabled = false;
                OpenBtn.IsEnabled = false;
                ReloadBtn.IsEnabled = false;
                ThemesBtn.IsEnabled = false;
                PluginsBtn.IsEnabled = false;
                ManageBtn.IsEnabled = false;
                SettingsBtn.IsEnabled = false;
                AboutBtn.IsEnabled = false;
                LogsBtn.IsEnabled = false;
                Navigate("SpicetifyNotDetectedPage");
            }

        }

        //Navigation
        public readonly Dictionary<string, Page> Pages = new()
        {
            {"Start", new StartPage()},
            {"Themes", new ThemesPage()},
            {"Plugins", new PluginsPage()},
            {"Manage", new ManagePage()},
            {"Settings", new SettingsPage()},
            {"About", new AboutPage()},
            {"Logs", new LogsPage()},
            {"SpicetifyNotDetected", new SpicetifyNotDetectedPage()}
        };

        private void Navigate(string navItemTag)
        {
            ContentFrame.Navigate(Spicetify.Instance.Detected ? Pages[navItemTag] : Pages["SpicetifyNotDetected"]);
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if(ContentFrame.Content == null)
                return;

            KeyValuePair<string, Page> item = Pages.FirstOrDefault(page => page.Value == e.Content);

            foreach(Button button in NavBar.Children.OfType<Button>())
            {
                if(button.Name.Equals(item.Key + "Btn"))
                    button.Style = (Style)FindResource("NavBtnSelected");
                else
                    button.Style = (Style)FindResource("NavBtn");
            }
        }

        private void Reload()
        {
            Spicetify.Instance.ListAll();
            Spicetify.Instance.Settings.Load();

            ((SettingsPage)Pages["Settings"]).InitControls();
            ((ThemesPage)Pages["Themes"]).InitControls();
            ((PluginsPage)Pages["Plugins"]).InitControls();
            ((AboutPage)Pages["About"]).InitControls();
        }

        //Bottom Panel
        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.Apply());
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ((ThemesPage)Pages["Themes"]).ReadInput();
            ((PluginsPage)Pages["Plugins"]).ReadInput();

            Spicetify.Instance.Settings.Save();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            ((ThemesPage)Pages["Themes"]).ReadInput();
            Spicetify.Instance.Settings.Save();

            Task.Run(() => Spicetify.Instance.Update());
        }

        private void ThemesFolder_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.OpenThemeFolder());
        }

        private void PluginsFolder_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.OpenExtensionsFolder());
        }

        private void CustomAppsFolder_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.OpenCustomAppsFolder());
        }

        private void ConfigFile_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Spicetify.Instance.OpenConfigFile());
        }

        private void ReloadBtn_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        //NavBar Buttons
        private void ThemesBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Themes");
        }

        private void PluginsBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Plugins");
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Settings");
        }

        private void ManageBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Manage");
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("About");
        }

        private void LogsBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigate("Logs");
        }
    }
}