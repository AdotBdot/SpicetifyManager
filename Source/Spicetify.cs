using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using SpicetifyManager.Source;

namespace SpicetifyManager
{
    public class Spicetify
    {
        public Spicetify(string userDirectory, string cliDirectory)
        {
            _UserDirectory = userDirectory;
            _CliDirectory = cliDirectory;

            Detected = DetectSpicetify();
            ConfigFile = ReadConfigPath();
            Version = ReadVersion();

            ListAll();
        }

        public bool DetectSpicetify()
        {
            try
            {
                return File.Exists(_CliDirectory + "spicetify.exe");
            }
            catch(DirectoryNotFoundException)
            {
                return false;
            }
            catch(FileNotFoundException)
            {
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<string> GetColors(string themeName)
        {
            if(!Detected)
                return new List<string>();

            string[] lines;

            try
            {
                lines = File.ReadAllLines(_UserDirectory + "Themes\\" + themeName + "\\color.ini");
            }
            catch(FileNotFoundException)
            {
                return new List<string>();
            }
            catch(DirectoryNotFoundException e)
            {
                try
                {
                    lines = File.ReadAllLines(_UserDirectory + "..\\spicetify-cli\\Themes\\" + themeName + "\\color.ini");
                }
                catch(FileNotFoundException)
                {
                    return new List<string>();
                }
                catch(DirectoryNotFoundException)
                {
                    return new List<string>();
                }
                catch(Exception exception)
                {
                    Console.WriteLine("Handled: " + e);
                    Console.WriteLine("Unhandled exception:" + exception);
                    return new List<string>();
                }
            }
            catch(Exception e)
            {
                Logger.Log("Unhandled exception:" + e);
                return new List<string>();
            }

            List<string> returnValue = new();

            foreach(string line in lines)
            {
                if(line.StartsWith("["))
                {
                    string color = line;
                    color = color.Replace("[", "").Replace("]", "");
                    returnValue.Add(color);
                }
            }

            return returnValue;
        }
        public List<string> GetCustomApps()
        {
            return _CustomApps;
        }
        public List<string> GetExtensions()
        {
            return _Extensions;
        }
        public List<string> GetThemes()
        {
            return _Themes;
        }

        public void OpenConfigFile()
        {
            if (!Detected)
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = ReadConfigPath(),
                UseShellExecute = true,
            });
        }
        public void OpenCustomAppsFolder()
        {
            if (!Detected)
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = _UserDirectory + "CustomApps\\",
                UseShellExecute = true,
                Verb = "open"
            });

            Logger.Log(_UserDirectory + "CustomApps\\");
        }
        public void OpenExtensionsFolder()
        {
            if (!Detected)
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = _UserDirectory + "Extensions\\",
                UseShellExecute = true,
                Verb = "open"
            });

            Logger.Log(_UserDirectory + "Extensions\\");
        }
        public void OpenThemeFolder()
        {
            if(!Detected)
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = _UserDirectory + "Themes\\",
                UseShellExecute = true,
                Verb = "open"
            });

            Logger.Log(_UserDirectory + "Themes\\");
        }

        public async Task Apply()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "apply");
        }
        public async Task Backup()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "backup");
        }
        public async Task Clear()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "-q clear");
        }
        public async Task EnableDevTools()
        {
            if (!Detected)
                return;

            ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "enable-devtools");
        }
        public async Task Restart()
        {
            if (!Detected)
                return;

            ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "restart");
        }
        public async Task Restore()
        {
            if (!Detected)
                return;

            ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "restore");
        }
        public async Task Update()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "update");
        }
        public async Task Upgrade()
        {
            if(!Detected)
                return;

            ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "upgrade");
        }
        public async Task FullUpgrade()
        {
            if(!Detected)
                return;

            Logger.Log("Running Upgrade Sequence...");
            await Upgrade();
            await Restore();
            await Backup();
            await Apply();
            Logger.Log("Fully Upgraded!");
        }

        private string ReadConfigPath()
        {
            if (!Detected)
                return string.Empty;

            List<string> results = ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "-c");

            return results[0];
        }
        private string ReadVersion()
        {
            if(!Detected)
                return "Not detected";

            List<string> results = ProcessInvoker.Invoke(_CliDirectory + "spicetify.exe", "-v");
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
            if (!Detected)
            {
                _CustomApps = new List<string>();
                return;
            }

            _CustomApps = new List<string>();

            //.spicetify
            string[] userApps = Directory.GetDirectories(_UserDirectory + "CustomApps");
            foreach (string app in userApps)
            {
                _CustomApps.Add(app.Substring(app.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }

            //spicetify-cli
            string[] buildInApps = Directory.GetDirectories(_CliDirectory + "\\CustomApps");
            foreach (string app in buildInApps)
            {
                _CustomApps.Add(app.Substring(app.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }
        }
        private void ListExtensions()
        {
            if (!Detected)
            {
                _Extensions = new List<string>();
                return;
            }

            _Extensions = new List<string>();

            //.spicetify
            string[] userExt = Directory.GetFiles(_UserDirectory + "Extensions");
            foreach (string ext in userExt)
            {
                _Extensions.Add(ext.Substring(ext.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }

            //spicetify-cli
            string[] builtInExt = Directory.GetFiles(_CliDirectory + "Extensions");
            foreach (string ext in builtInExt)
            {
                _Extensions.Add(ext.Substring(ext.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }
        }
        private void ListThemes()
        {
            if(!Detected)
            {
                _Themes = new List<string>();
                return;
            }

            _Themes = new List<string> { "(none)" };

            //.spicetify
            string[] userThemes = Directory.GetDirectories(_UserDirectory + "Themes");
            foreach(string theme in userThemes)
            {
                _Themes.Add(theme.Substring(theme.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }

            //spicetify-cli
            string[] buildInThemes = Directory.GetDirectories(_CliDirectory + "Themes");
            foreach(string theme in buildInThemes)
            {
                _Themes.Add(theme.Substring(theme.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }
        }

        public string Version;
        public readonly string ConfigFile;
        public readonly bool Detected;

        private List<string> _CustomApps;
        private List<string> _Extensions;
        private List<string> _Themes;

        private readonly string _UserDirectory;
        private readonly string _CliDirectory;
    }
}