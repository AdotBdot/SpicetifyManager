using System.Diagnostics;
using System.Windows.Navigation;
using ModernWpf.Controls;

namespace SpicetifyManager.Pages
{
    /// <summary>
    /// Interaction logic for InstallPage.xaml
    /// </summary>
    public partial class InstallPage : Page
    {
        public InstallPage()
        {
            InitializeComponent();
        }

        private void Spicetify_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://spicetify.app/docs/getting-started")
            {
                UseShellExecute = true
            });
            e.Handled = true;
        }

        private void Issues_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/AdotBdot/SpicetifyManager/issues")
            {
                UseShellExecute = true
            });
            e.Handled = true;
        }
    }
}