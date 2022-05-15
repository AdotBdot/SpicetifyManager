using System.Collections.Generic;

namespace SpicetifyManager
{
    internal class ConfigFileWriter : ConfigFileLoader
    {
        public void WriteString(string section, string key, string value)
        {
            Data[section][key] = value;
        }

        public void WriteBool(string section, string key, bool value)
        {
            Data[section][key] = (value == true ? "1" : "0");
        }

        public void WriteList(string section, string key, List<string> value)
        {
            string s;

            if(value.Count != 0)
            {
                s = value[0];
                for(int i = 1; i < value.Count; i++)
                {
                    s += "|";
                    s += value[i];
                }
            }
            else
            {
                s = string.Empty;
            }

            Data[section][key] = s;
        }

        public void WriteFile(string configFile)
        {
            Parser.WriteFile(configFile, Data);
        }
    }
}