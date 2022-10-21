using System;

namespace SpicetifyManager.Source
{
    public class StaticData
    {
        static StaticData()
        {
            Spicetify = new Spicetify();
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

        public static readonly Spicetify Spicetify;
        public static readonly Settings Settings;

        public const string Version = "2.0.1";
    }
}