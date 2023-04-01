using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IniParser;
using IniParser.Model;

namespace SpicetifyManager
{
    class IniFile
    {
        public IniFile()
        {
            _Parser = new FileIniDataParser();
        }

        public void LoadFile(string configFile)
        {
            try
            {
                _Data = _Parser.ReadFile(configFile);
            }
            catch(FileNotFoundException)
            {
                Logger.Log($"File \"{configFile}\" does not exist.");
            }
        }

        public bool ReadBool(string section, string key)
        {
            if(!_Data[section].ContainsKey(key))
                return false;

            return ReadString(section, key) == "1";
        }

        public void WriteBool(string section, string key, bool value)
        {
            _Data[section][key] = (value == true ? "1" : "0");
        }

        public List<string> ReadList(string section, string key)
        {
            if(!_Data[section].ContainsKey(key))
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
                s = string.Empty;

            _Data[section][key] = s;
        }

        public string ReadString(string section, string key)
        {
            if(!_Data[section].ContainsKey(key))
                return string.Empty;
            return _Data[section][key];
        }

        public void WriteString(string section, string key, string value)
        {
            _Data[section][key] = value;
        }

        private string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        public void WriteFile(string configFile)
        {
            _Parser.WriteFile(configFile, _Data);
        }

        private FileIniDataParser _Parser;
        private IniData _Data;
    }
}