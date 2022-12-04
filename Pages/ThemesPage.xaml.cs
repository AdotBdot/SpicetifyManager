using System.Windows.Controls;

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

            if(Spicetify.Instance.Detected)
                InitControls();
        }

        public void InitControls()
        {
            ThemesListBox.ItemsSource = Spicetify.Instance.ThemesList;

            if(Spicetify.Instance.Settings.CurrentTheme == string.Empty)
                ThemesListBox.SelectedIndex = ThemesListBox.Items.IndexOf("(none)");
            else
                ThemesListBox.SelectedIndex = ThemesListBox.Items.IndexOf(Spicetify.Instance.Settings.CurrentTheme);

            ColorSchemesListBox.ItemsSource = Spicetify.Instance.GetThemeColors(Spicetify.Instance.Settings.CurrentTheme);

            if(Spicetify.Instance.Settings.ColorScheme == string.Empty)
                ColorSchemesListBox.SelectedItem = null;
            else
                ColorSchemesListBox.SelectedIndex = ColorSchemesListBox.Items.IndexOf(Spicetify.Instance.Settings.ColorScheme);
        }

        private void ThemesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorSchemesListBox.ItemsSource = Spicetify.Instance.GetThemeColors(ThemesListBox.SelectedItem.ToString());
        }

        public void ReadInput()
        {
            if(ThemesListBox.SelectedItem.ToString() == "(none)")
                Spicetify.Instance.Settings.CurrentTheme = "";
            else
                Spicetify.Instance.Settings.CurrentTheme = ThemesListBox.SelectedItem.ToString();

            if(ColorSchemesListBox.SelectedItem == null)
                Spicetify.Instance.Settings.ColorScheme = "";
            else
                Spicetify.Instance.Settings.ColorScheme = ColorSchemesListBox.SelectedItem.ToString();
        }
    }
}