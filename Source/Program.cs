using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Octokit;
using Application = System.Windows.Forms.Application;

namespace SpicetifyManager
{
    namespace My
    {
        public static class Colors
        {
            public static Color GetBg(int elevationStep)
            {
                switch(elevationStep)
                {
                case 0:
                    return Color.FromArgb(18, 18, 18);
                case 1:
                    return Color.FromArgb(29, 29, 29);
                case 2:
                    return Color.FromArgb(33, 33, 33);
                case 3:
                    return Color.FromArgb(36, 36, 36);
                case 4:
                    return Color.FromArgb(38, 38, 38);
                case 5:
                    return Color.FromArgb(44, 44, 44);
                case 6:
                    return Color.FromArgb(45, 45, 45);
                case 7:
                    return Color.FromArgb(50, 50, 50);
                case 8:
                    return Color.FromArgb(53, 53, 53);
                case 9:
                    return Color.FromArgb(55, 55, 55);
                default:
                    return Color.FromArgb(18, 18, 18);
                }
            }

            public static Color Primary = Color.FromArgb(234, 82, 58);
            public static Color TxtLight = SystemColors.ControlLightLight;
            public static Color TxtDark = SystemColors.ControlText;
        }

        public static class Fonts
        {
            public static PrivateFontCollection Pfc = new PrivateFontCollection();

            public static void LoadFonts()
            {
                LoadFontFromResx(SpicetifyManager.Properties.Resources.OpenSans_Regular);
                LoadFontFromResx(SpicetifyManager.Properties.Resources.OpenSans_SemiBold);
            }

            [System.Runtime.InteropServices.DllImport("gdi32.dll")]
            private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
                IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

            private static void LoadFontFromResx(byte[] font)
            {
                byte[] fontData = font;
                IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
                System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                uint dummy = 0;
                Pfc.AddMemoryFont(fontPtr, font.Length);
                AddFontMemResourceEx(fontPtr, (uint)font.Length, IntPtr.Zero, ref dummy);
                System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            }
        }

        public static class Version
        {
            public static async Task<string> GetLastTag(string owner, string repoName)
            {
                GitHubClient git = new GitHubClient(new ProductHeaderValue("Tag"));
                var repo = await git.Repository.Get(owner, repoName);
                var tags = await git.Repository.GetAllTags(repo.Id);

                return tags[0].Name;
            }

            public static readonly string CurrentVersion = "v1.3.2";
        }
    }

    internal static class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static void ToogleConsole()
        {
            if(IsWindowVisible(GetConsoleWindow()))
                ShowWindow(GetConsoleWindow(), SW_HIDE);
            else
                ShowWindow(GetConsoleWindow(), SW_SHOW);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ShowWindow(GetConsoleWindow(), SW_HIDE);

            My.Fonts.LoadFonts();

            string userDirectory = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\.spicetify\");
            string cliDirectory = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\spicetify-cli\");

            Spicetify spicetify = new Spicetify(userDirectory, cliDirectory);
            Settings settings = new Settings(spicetify);

            spicetify.ListAll();
            settings.LoadConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(settings, spicetify));
        }
    }
}