using ModernWpf.Controls;
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

            if(Spicetify.Instance.Detected)
                InitControls();
        }

        public void InitControls()
        {
            //Init PluginsList
            PluginsList.ItemsSource = Spicetify.Instance.ExtensionsList;

            PluginsList.SelectedItems.Clear();
            foreach(var item in PluginsList.Items)
            {
                if(Spicetify.Instance.Settings.Extensions.Contains(item.ToString()))
                    PluginsList.SelectedItems.Add(item);
            }

            //Init CustomAppsList
            CustomAppsList.ItemsSource = Spicetify.Instance.CustomAppsList;

            CustomAppsList.SelectedItems.Clear();
            foreach(var item in CustomAppsList.Items)
            {
                if(Spicetify.Instance.Settings.CustomApps.Contains(item.ToString()))
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

            Spicetify.Instance.Settings.Extensions = extsList;

            IList selectedApps = CustomAppsList.SelectedItems;
            List<string> appsList = new();

            foreach(var item in selectedApps)
            {
                appsList.Add(item.ToString());
            }

            Spicetify.Instance.Settings.CustomApps = appsList;
        }
    }
}