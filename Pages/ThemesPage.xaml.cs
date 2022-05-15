using System.Windows.Controls;
using SpicetifyManager.Source;

namespace SpicetifyManager.Pages
{
    /// <summary>
    /// Interaction logic for ThemesPage.xaml
    /// </summary>
    public partial class ThemesPage
    {
        public ThemesPage()
        {
            InitializeComponent();

            InitThemesListBox();
            InitColorSchemesBox();
        }

        public void InitThemesListBox()
        {
            ThemesListBox.ItemsSource = StaticData.Spicetify.GetThemes();

            if(StaticData.Settings.CurrentTheme == string.Empty)
                ThemesListBox.SelectedIndex = ThemesListBox.Items.IndexOf("(none)");
            else
                ThemesListBox.SelectedIndex = ThemesListBox.Items.IndexOf(StaticData.Settings.CurrentTheme);
        }

        public void InitColorSchemesBox()
        {
            ColorSchemesListBox.ItemsSource = StaticData.Spicetify.GetColors(StaticData.Settings.CurrentTheme);

            if(StaticData.Settings.ColorScheme == string.Empty)
                ColorSchemesListBox.SelectedItem = null;
            else
                ColorSchemesListBox.SelectedIndex = ColorSchemesListBox.Items.IndexOf(StaticData.Settings.ColorScheme);
        }

        private void ThemesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorSchemesListBox.ItemsSource = StaticData.Spicetify.GetColors(ThemesListBox.SelectedItem.ToString());
        }

        public void ReadInput()
        {
            if(ThemesListBox.SelectedItem.ToString() == "(none)")
                StaticData.Settings.CurrentTheme = "";
            else
                StaticData.Settings.CurrentTheme = ThemesListBox.SelectedItem.ToString();

            if(ColorSchemesListBox.SelectedItem == null)
                StaticData.Settings.CurrentTheme = "";
            else
                StaticData.Settings.ColorScheme = ColorSchemesListBox.SelectedItem.ToString();
        }
    }
}