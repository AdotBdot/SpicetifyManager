using System;

namespace SpicetifyManager.Source
{
    public class StaticData
    {
        static StaticData()
        {
            UserDirectory = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\.spicetify\");
            CliDirectory = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\spicetify-cli\");

            Spicetify = new Spicetify(UserDirectory, CliDirectory);
            if(Spicetify.Detected)
            {
                Settings = new Settings(Spicetify);
                Settings.LoadConfig();
            }
        }

        public static void Reload()
        {
            Spicetify.ListAll();
            Settings.LoadConfig();
        }

        public static readonly string UserDirectory;
        public static readonly string CliDirectory;

        public static readonly Spicetify Spicetify;
        public static readonly Settings Settings;

        public const string Version = "2.0.0";
    }
}