using System.Collections.Generic;

namespace SpicetifyManager
{
    public class Settings
    {
        public Settings(Spicetify spicetify)
        {
            _Spicetify = spicetify;
            _Reader = new ConfigFileReader();
            _Writer = new ConfigFileWriter();
        }

        public void LoadConfig()
        {
            if(!_Spicetify.Detected)
                return;

            _Reader.LoadFile(_Spicetify.ConfigFile);

            ReadSetting();
            ReadPreprocesses();
            ReadAdditionalOptions();
            ReadBackup();
        }

        public void SaveThemes()
        {
            _Writer.LoadFile(_Spicetify.ConfigFile);

            _Writer.WriteString("Setting", "current_theme", CurrentTheme);
            _Writer.WriteString("Setting", "color_scheme", ColorScheme);

            _Writer.WriteFile(_Spicetify.ConfigFile);
        }

        public void SavePlugins()
        {
            _Writer.LoadFile(_Spicetify.ConfigFile);

            _Writer.WriteList("AdditionalOptions", "extensions", ExtensionsList);
            _Writer.WriteList("AdditionalOptions", "custom_apps", CustomAppsList);

            _Writer.WriteFile(_Spicetify.ConfigFile);
        }

        public void SaveSettings()
        {
            _Writer.LoadFile(_Spicetify.ConfigFile);

            _Writer.WriteString("Setting", "prefs_path", PrefsPath);
            _Writer.WriteBool("Setting", "overwrite_assets", OverwriteAssets);
            _Writer.WriteBool("Setting", "check_spicetify_upgrade", CheckSpicetifyUpgrade);
            _Writer.WriteString("Setting", "spotify_path", SpotifyPath);
            _Writer.WriteBool("Setting", "inject_css", InjectCss);
            _Writer.WriteBool("Setting", "replace_colors", ReplaceColors);
            _Writer.WriteString("Setting", "spotify_launch_flags", SpotifyLaunchFlags);

            _Writer.WriteBool("Preprocesses", "disable_sentry", DisableSentry);
            _Writer.WriteBool("Preprocesses", "disable_ui_logging", DisableUiLogging);
            _Writer.WriteBool("Preprocesses", "remove_rtl_rule", RemoveRtlRule);
            _Writer.WriteBool("Preprocesses", "expose_apis", ExposeApis);
            _Writer.WriteBool("Preprocesses", "disable_upgrade_check", DisableUpgradeCheck);

            _Writer.WriteBool("AdditionalOptions", "home_config", HomeConfig);
            _Writer.WriteBool("AdditionalOptions", "sidebar_config", SidebarConfig);
            _Writer.WriteBool("AdditionalOptions", "experimental_features", ExperimentalFeatures);

            _Writer.WriteFile(_Spicetify.ConfigFile);
        }


        private void ReadSetting()
        {
            PrefsPath = _Reader.ReadString("Setting", "prefs_path");
            OverwriteAssets = _Reader.ReadBool("Setting", "overwrite_assets");
            CheckSpicetifyUpgrade = _Reader.ReadBool("Setting", "check_spicetify_upgrade");
            SpotifyPath = _Reader.ReadString("Setting", "spotify_path");
            CurrentTheme = _Reader.ReadString("Setting", "current_theme");
            ColorScheme = _Reader.ReadString("Setting", "color_scheme");
            InjectCss = _Reader.ReadBool("Setting", "inject_css");
            ReplaceColors = _Reader.ReadBool("Setting", "replace_colors");
            SpotifyLaunchFlags = _Reader.ReadString("Setting", "spotify_launch_flags");
        }

        private void ReadPreprocesses()
        {
            DisableSentry = _Reader.ReadBool("Preprocesses", "disable_sentry");
            DisableUiLogging = _Reader.ReadBool("Preprocesses", "disable_ui_logging");
            RemoveRtlRule = _Reader.ReadBool("Preprocesses", "remove_rtl_rule");
            ExposeApis = _Reader.ReadBool("Preprocesses", "expose_apis");
            DisableUpgradeCheck = _Reader.ReadBool("Preprocesses", "disable_upgrade_check");
        }

        private void ReadAdditionalOptions()
        {
            HomeConfig = _Reader.ReadBool("AdditionalOptions", "home_config");
            ExtensionsList = _Reader.ReadList("AdditionalOptions", "extensions");
            CustomAppsList = _Reader.ReadList("AdditionalOptions", "custom_apps");
            SidebarConfig = _Reader.ReadBool("AdditionalOptions", "sidebar_config");
            ExperimentalFeatures = _Reader.ReadBool("AdditionalOptions", "experimental_features");
        }

        private void ReadBackup()
        {
            SpotifyVersion = _Reader.ReadString("Backup", "version");
        }

        private Spicetify _Spicetify;
        private ConfigFileReader _Reader;
        private ConfigFileWriter _Writer;

        //Settings
        public string PrefsPath;
        public bool OverwriteAssets{get; set;}
        public bool CheckSpicetifyUpgrade{get; set;}
        public string SpotifyPath;
        public string CurrentTheme;
        public string ColorScheme;
        public bool InjectCss{get; set;}
        public bool ReplaceColors{get; set;}
        public string SpotifyLaunchFlags;

        //Preprocesses
        public bool DisableSentry{get; set;}
        public bool DisableUiLogging{get; set;}
        public bool RemoveRtlRule{get; set;}
        public bool ExposeApis{get; set;}
        public bool DisableUpgradeCheck{get; set;}

        //AdditionalOptions
        public bool HomeConfig{get; set;}
        public List<string> ExtensionsList;
        public List<string> CustomAppsList;
        public bool SidebarConfig{get; set;}
        public bool ExperimentalFeatures{get; set;}

        //Backup
        public string SpotifyVersion;
    }
}