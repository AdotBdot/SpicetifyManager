using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SpicetifyManager.Source;

namespace SpicetifyManager
{
    public sealed class Spicetify
    {
        private Spicetify()
        {
            if(!File.Exists("config.ini"))
            {
                using(StreamWriter file = File.CreateText("config.ini"))
                {
                    file.WriteLine("[SpicetifyManagerConfig]");
                    file.WriteLine("UserDirectory = %APPDATA%\\spicetify");
                    file.WriteLine("CliDirectory  = %LOCALAPPDATA%\\spicetify");
                }
            }

            Detected = DetectSpicetify();

            if(Detected)
            {
                ConfigFilePath = ReadConfigPath();
                Version = ReadVersion();
                Settings = new Settings();
                ListAll();
            }
        }

        private static Spicetify? _Instance;
        private static readonly object _Lock = new();

        public static Spicetify Instance
        {
            get
            {
                lock(_Lock)
                {
                    if(_Instance == null)
                    {
                        _Instance = new Spicetify();
                        _Instance.Settings.Load();
                    }

                    return _Instance;
                }
            }
        }

        public bool DetectSpicetify()
        {
            Logger.Log("Detecting Spicetify...");
            bool detected = true;

            //READING FROM CONFIG
            IniFile configFile = new();
            configFile.LoadFile("config.ini");

            UserDirectory = Environment.ExpandEnvironmentVariables(configFile.ReadString("SpicetifyManagerConfig", "UserDirectory"));
            CliDirectory = Environment.ExpandEnvironmentVariables(configFile.ReadString("SpicetifyManagerConfig", "CliDirectory"));

            try
            {
                if(!Directory.Exists(CliDirectory))
                {
                    Logger.Log($"Directory \"{CliDirectory}\" does not exist.");
                    detected = false;
                }
                else if(!File.Exists(CliDirectory + "\\spicetify.exe"))
                {
                    Logger.Log($"File \"{CliDirectory}\\spicetify.exe\" does not exist.");
                    detected = false;
                }

                if(!Directory.Exists(UserDirectory))
                {
                    Logger.Log($"Directory \"{UserDirectory}\" does not exist.");
                    detected = false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Logger.Log($"Unexpected exception: {e.Message}");
                detected = false;
            }

            //READING FROM PATH
            string pathDirectory = Regex.Match(Environment.GetEnvironmentVariable("Path"), @"[^;]+SPICETIFY?[^;]+", RegexOptions.IgnoreCase).Value;
            if(detected == false)
            {
                Logger.Log($"Changing client directory from: {CliDirectory} to: {pathDirectory}");
                CliDirectory = pathDirectory;
            }

            detected = true;
            try
            {
                if(!Directory.Exists(CliDirectory))
                {
                    Logger.Log($"Directory \"{CliDirectory}\" does not exist.");
                    detected = false;
                }
                else if(!File.Exists(CliDirectory + "\\spicetify.exe"))
                {
                    Logger.Log($"File \"{CliDirectory}\\spicetify.exe\" does not exist.");
                    detected = false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Logger.Log($"Unexpected exception: {e.Message}");
                detected = false;
            }

            //Validating path directory
            if( detected && CliDirectory != pathDirectory)
                Logger.Log($"Directories mismatch:\n" +
                           $"  Directory in PATH: \"{pathDirectory}\"\n" +
                           $"  Directory in config: \"{CliDirectory}\"\n" +
                           $"  Application may not work properly.");

            //todo: READING FROM DIRECT EVOKE

            if(detected)
                Logger.Log("Spicetify has been detected successfully.");
            else
                Logger.Log("Spicetify has not been detected.");

            return detected;
        }

        public List<string>? GetThemeColors(string themeName)
        {
            if(!Detected)
                return null;

            string[] lines;

            try
            {
                lines = File.ReadAllLines(UserDirectory + "\\Themes\\" + themeName + "\\color.ini");
            }
            catch(FileNotFoundException)
            {
                return null;
            }
            catch(DirectoryNotFoundException e)
            {
                try
                {
                    lines = File.ReadAllLines(CliDirectory + "\\Themes\\" + themeName + "\\color.ini");
                }
                catch(FileNotFoundException)
                {
                    return null;
                }
                catch(DirectoryNotFoundException)
                {
                    return null;
                }
                catch(Exception exception)
                {
                    Console.WriteLine("Handled: " + e);
                    Console.WriteLine("Unexpected exception:" + exception);
                    return null;
                }
            }
            catch(Exception e)
            {
                Logger.Log($"Unexpected exception: {e.Message}");
                return null;
            }

            List<string> themeColors = new();

            foreach(string line in lines)
            {
                if(line.StartsWith("["))
                {
                    string color = line;
                    color = color.Replace("[", "").Replace("]", "");
                    themeColors.Add(color);
                }
            }

            return themeColors;
        }

        public void OpenConfigFile()
        {
            if(ConfigFilePath == null)
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = ConfigFilePath,
                UseShellExecute = true,
            });

            Logger.Log($"open {ConfigFilePath}");
        }

        public void OpenCustomAppsFolder()
        {
            if(!Detected)
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = UserDirectory + "\\CustomApps\\",
                UseShellExecute = true,
                Verb = "open"
            });

            Logger.Log($"open {UserDirectory}\\CustomApps\\");
        }

        public void OpenExtensionsFolder()
        {
            if(!Detected)
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = UserDirectory + "\\Extensions\\",
                UseShellExecute = true,
                Verb = "open"
            });

            Logger.Log($"open {UserDirectory}\\Extensions\\");
        }

        public void OpenThemeFolder()
        {
            if(!Detected)
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = UserDirectory + "\\Themes\\",
                UseShellExecute = true,
                Verb = "open"
            });

            Logger.Log($"open {UserDirectory}\\Themes\\");
        }

        public async Task Apply()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "apply");
        }

        public async Task Backup()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "backup");
        }

        public async Task Clear()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "-q clear");
        }

        public async Task EnableDevTools()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "enable-devtools");
        }

        public async Task Restart()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "restart");
        }

        public async Task Restore()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "restore");
        }

        public async Task Update()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "update");
        }

        public async Task Upgrade()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "upgrade");
        }

        public async Task RestoreBackupApply()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "restore backup apply");
        }

        public async Task FullUpgrade()
        {
            if(!Detected)
                return;

            Logger.Log("Running upgrade sequence...");
            await Upgrade();
            await RestoreBackupApply();
        }

        private string ReadConfigPath()
        {
            List<string> results = ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "-c");
            return results[0];
        }

        private string ReadVersion()
        {
            List<string> results = ProcessInvoker.Invoke(CliDirectory + "\\spicetify.exe", "-v");
            return results[0];
        }

        public void ListAll()
        {
            ListThemes();
            ListExtensions();
            ListCustomApps();
        }

        private void ListCustomApps()
        {
            if(!Detected)
                return;

            CustomAppsList = new List<string>();

            //.spicetify
            string[] userApps = Directory.GetDirectories(UserDirectory + "\\CustomApps");
            foreach(string app in userApps)
            {
                CustomAppsList.Add(app.Substring(app.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }

            //spicetify-cli
            string[] builtInApps = Directory.GetDirectories(CliDirectory + "\\CustomApps");
            foreach(string app in builtInApps)
            {
                CustomAppsList.Add(app.Substring(app.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }

            Logger.Log($"Found {userApps.Length + builtInApps.Length} custom apps.");
        }

        private void ListExtensions()
        {
            if(!Detected)
                return;

            ExtensionsList = new List<string>();

            //.spicetify
            string[] userExt = Directory.GetFiles(UserDirectory + "\\Extensions");
            foreach(string ext in userExt)
            {
                ExtensionsList.Add(ext.Substring(ext.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }

            //spicetify-cli
            string[] builtInExt = Directory.GetFiles(CliDirectory + "\\Extensions");
            foreach(string ext in builtInExt)
            {
                ExtensionsList.Add(ext.Substring(ext.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }

            Logger.Log($"Found {userExt.Length + builtInExt.Length} extensions.");
        }

        private void ListThemes()
        {
            if(!Detected)
                return;

            ThemesList = new List<string> { "(none)" };

            //.spicetify
            string[] userThemes = Directory.GetDirectories(UserDirectory + "\\Themes");
            foreach(string theme in userThemes)
            {
                ThemesList.Add(theme.Substring(theme.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }

            //spicetify-cli
            string[] builtInThemes = Directory.GetDirectories(CliDirectory + "\\Themes");
            foreach(string theme in builtInThemes)
            {
                ThemesList.Add(theme.Substring(theme.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }

            Logger.Log($"Found {userThemes.Length + builtInThemes.Length} themes.");
        }

        public readonly bool Detected;
        public string? Version { get; private set; }
        public readonly string? ConfigFilePath;
        public string UserDirectory { get; private set; }
        public string CliDirectory { get; private set; }

        public Settings Settings;
        public List<string>? CustomAppsList { get; private set; }
        public List<string>? ExtensionsList { get; private set; }
        public List<string>? ThemesList { get; private set; }
    }
}