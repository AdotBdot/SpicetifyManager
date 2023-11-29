using System.Collections.Generic;

namespace SpicetifyManager
{
    public struct Settings
    {
        public Settings()
        {
        }

        public void Load()
        {
            if(!Spicetify.Instance.Detected)
                return;

            IniFile configFile = new();
            configFile.LoadFile(Spicetify.Instance.ConfigFilePath);

            SpotifyPath = configFile.ReadString("Setting", "spotify_path");
            PrefsPath = configFile.ReadString("Setting", "prefs_path");
            CurrentTheme = configFile.ReadString("Setting", "current_theme");
            ColorScheme = configFile.ReadString("Setting", "color_scheme");
            InjectCss = configFile.ReadBool("Setting", "inject_css");
            InjectThemeJs = configFile.ReadBool("Setting", "inject_theme_js");
            OverwriteAssets = configFile.ReadBool("Setting", "overwrite_assets");
            CheckSpicetifyUpgrade = configFile.ReadBool("Setting", "check_spicetify_upgrade");
            ReplaceColors = configFile.ReadBool("Setting", "replace_colors");
            SpotifyLaunchFlags = configFile.ReadString("Setting", "spotify_launch_flags");

            DisableUiLogging = configFile.ReadBool("Preprocesses", "disable_ui_logging");
            RemoveRtlRule = configFile.ReadBool("Preprocesses", "remove_rtl_rule");
            ExposeApis = configFile.ReadBool("Preprocesses", "expose_apis");
            DisableUpgradeCheck = configFile.ReadBool("Preprocesses", "disable_upgrade_check");
            DisableSentry = configFile.ReadBool("Preprocesses", "disable_sentry");

            Extensions = configFile.ReadList("AdditionalOptions", "extensions");
            CustomApps = configFile.ReadList("AdditionalOptions", "custom_apps");
            SidebarConfig = configFile.ReadBool("AdditionalOptions", "sidebar_config");
            HomeConfig = configFile.ReadBool("AdditionalOptions", "home_config");
            ExperimentalFeatures = configFile.ReadBool("AdditionalOptions", "experimental_features");

            BackupVersion = configFile.ReadString("Backup", "version");
            WithVersion = configFile.ReadString("Backup", "with");
        }

        public void Save()
        {
            if(!Spicetify.Instance.Detected)
                return;

            IniFile configFile = new();
            configFile.LoadFile(Spicetify.Instance.ConfigFilePath);

            configFile.WriteString("Setting", "spotify_path", SpotifyPath);
            configFile.WriteString("Setting", "prefs_path", PrefsPath);
            configFile.WriteString("Setting", "spotify_launch_flags", SpotifyLaunchFlags);
            configFile.WriteString("Setting", "current_theme", CurrentTheme);
            configFile.WriteString("Setting", "color_scheme", ColorScheme);
            configFile.WriteBool("Setting", "inject_css", InjectCss);
            configFile.WriteBool("Setting", "inject_theme_js", InjectCss);
            configFile.WriteBool("Setting", "overwrite_assets", OverwriteAssets);
            configFile.WriteBool("Setting", "check_spicetify_upgrade", CheckSpicetifyUpgrade);
            configFile.WriteBool("Setting", "replace_colors", ReplaceColors);

            configFile.WriteBool("Preprocesses", "disable_ui_logging", DisableUiLogging);
            configFile.WriteBool("Preprocesses", "remove_rtl_rule", RemoveRtlRule);
            configFile.WriteBool("Preprocesses", "expose_apis", ExposeApis);
            configFile.WriteBool("Preprocesses", "disable_upgrade_check", DisableUpgradeCheck);
            configFile.WriteBool("Preprocesses", "disable_sentry", DisableSentry);

            configFile.WriteList("AdditionalOptions", "extensions", Extensions);
            configFile.WriteList("AdditionalOptions", "custom_apps", CustomApps);
            configFile.WriteBool("AdditionalOptions", "sidebar_config", SidebarConfig);
            configFile.WriteBool("AdditionalOptions", "home_config", HomeConfig);
            configFile.WriteBool("AdditionalOptions", "experimental_features", ExperimentalFeatures);

            configFile.WriteFile(Spicetify.Instance.ConfigFilePath);
        }

        public void RestoreDefault()
        {
            SpotifyLaunchFlags = string.Empty;
            CurrentTheme = string.Empty;
            ColorScheme = string.Empty;
            OverwriteAssets = false;
            InjectCss = true;
            InjectThemeJs = false;
            ReplaceColors = true;
            CheckSpicetifyUpgrade = false;

            ExposeApis = true;
            DisableSentry = true;
            DisableUiLogging = true;
            DisableUpgradeCheck = true;
            RemoveRtlRule = true;

            Extensions = new List<string>();
            CustomApps = new List<string>();
            HomeConfig = true;
            SidebarConfig = true;
            ExperimentalFeatures = true;
        }

        //Settings
        public string SpotifyPath{get; set;} = string.Empty;
        public string PrefsPath{get; set;} = string.Empty;
        public string SpotifyLaunchFlags{get; set;} = string.Empty;
        public string CurrentTheme{get; set;} = string.Empty;
        public string ColorScheme{get; set;} = string.Empty;
        public bool OverwriteAssets{get; set;} = false;
        public bool InjectCss{get; set;} = true;
        public bool InjectThemeJs {get;set;} = false;
        public bool ReplaceColors{get; set;} = true;
        public bool CheckSpicetifyUpgrade{get; set;} = false;

        //Preprocesses
        public bool ExposeApis { get; set; } = true;
        public bool DisableSentry{get; set;} = true;
        public bool DisableUiLogging{get; set;} = true;
        public bool DisableUpgradeCheck{get; set;} = true;
        public bool RemoveRtlRule { get; set; } = true;

        //AdditionalOptions
        public List<string> Extensions{get; set;} = new List<string>();
        public List<string> CustomApps{get; set;} = new List<string>();
        public bool HomeConfig{get; set;} = true;
        public bool SidebarConfig{get; set;} = true;
        public bool ExperimentalFeatures{get; set;} = true;

        //Backup
        public string BackupVersion{get; private set;} = string.Empty;
        public string WithVersion{get; private set;} = string.Empty;
    }
}