using ModernWpf.Controls;
using SpicetifyManager.Source;
using System.Collections;
using System.Collections.Generic;

namespace SpicetifyManager.Pages
{
    /// <summary>
    /// Interaction logic for PluginsPage.xaml
    /// </summary>
    public partial class PluginsPage : Page
    {
        public PluginsPage()
        {
            InitializeComponent();

            InitPluginsList();
            InitCustomAppsList();
        }

        public void InitPluginsList()
        {
            PluginsList.ItemsSource = StaticData.Spicetify.GetExtensions();

            PluginsList.SelectedItems.Clear();
            foreach(var item in PluginsList.Items)
            {
                if(StaticData.Settings.ExtensionsList.Contains(item.ToString()))
                    PluginsList.SelectedItems.Add(item);
            }
        }

        public void InitCustomAppsList()
        {
            CustomAppsList.ItemsSource = StaticData.Spicetify.GetCustomApps();

            CustomAppsList.SelectedItems.Clear();
            foreach(var item in CustomAppsList.Items)
            {
                if(StaticData.Settings.CustomAppsList.Contains(item.ToString()))
                    CustomAppsList.SelectedItems.Add(item);
            }
        }

        public void ReadInput()
        {
            IList selectedPlugins = PluginsList.SelectedItems;
            List<string> extsList = new();

            foreach(var item in selectedPlugins)
            {
                extsList.Add(item.ToString());
            }

            StaticData.Settings.ExtensionsList = extsList;

            IList selectedApps = CustomAppsList.SelectedItems;
            List<string> appsList = new();

            foreach(var item in selectedApps)
            {
                appsList.Add(item.ToString());
            }

            StaticData.Settings.CustomAppsList = appsList;
        }
    }
}