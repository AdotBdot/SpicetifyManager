using System;
using System.Collections.Generic;
using System.Linq;

namespace SpicetifyManager
{
    public class ConfigFileReader : ConfigFileLoader
    {
        public string ReadString(string section, string key)
        {
            if(!Data[section].ContainsKey(key))
                return string.Empty;
            return Data[section][key];
        }

        public bool ReadBool(string section, string key)
        {
            if(!Data[section].ContainsKey(key))
                return false;

            return ReadString(section, key) == "1" ? true : false;
        }

        public List<string> ReadList(string section, string key)
        {
            if(!Data[section].ContainsKey(key))
                return new List<string>();

            string l = RemoveWhitespace(ReadString(section, key));
            List<string> returnValue = new List<string>();

            while(l.Contains("|"))
            {
                string value = l.Substring(0, l.IndexOf("|", StringComparison.Ordinal));
                returnValue.Add(value);
                l = l.Replace(value + "|", string.Empty);
            }

            returnValue.Add(l);

            return returnValue;
        }

        private string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
    }
}