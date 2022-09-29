using System.Diagnostics;
using System.Windows.Navigation;
using ModernWpf.Controls;

namespace SpicetifyManager.Pages
{
    /// <summary>
    /// Interaction logic for SpicetifyNotDetectedPage.xaml
    /// </summary>
    public partial class SpicetifyNotDetectedPage : Page
    {
        public SpicetifyNotDetectedPage()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://spicetify.app/docs/getting-started")
            {
                UseShellExecute = true
            });
            e.Handled = true;
        }
    }
}