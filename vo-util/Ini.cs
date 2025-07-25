using System;

namespace VO
{
	public class IniParser
	{
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            System.Text.StringBuilder retVal,
            int size,
            string filePath);

        string iniPath;

        public IniParser(string path)
		{
            this.iniPath = path;
		}

        public string ReadValue(string section, string key, string def = "")
        {
            System.Text.StringBuilder retVal = new System.Text.StringBuilder(255);
            GetPrivateProfileString(section, key, def, retVal, retVal.Capacity, iniPath);
            return retVal.ToString();
        }
    }
}

