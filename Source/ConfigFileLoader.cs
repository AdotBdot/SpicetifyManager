using IniParser;
using IniParser.Model;

namespace SpicetifyManager
{
    public class ConfigFileLoader
    {
        public ConfigFileLoader()
        {
            Parser = new FileIniDataParser();
        }

        public void LoadFile(string configFile)
        {
            Data = Parser.ReadFile(configFile);
        }

        protected FileIniDataParser Parser;
        protected IniData Data;
    }
}