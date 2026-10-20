using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ExtendOffset_monomod
{
    internal static class OffsetConfig
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath);

        public static double ReadRange()
        {
            string path = FindIni();
            if (path == null)
                return OffsetRuntime.DefaultRange;

            StringBuilder buffer = new StringBuilder(64);
            GetPrivateProfileString(
                "ExtendOffset",
                "OffsetRange",
                "2.0",
                buffer,
                buffer.Capacity,
                path);

            return ParseRange(buffer.ToString());
        }

        public static double ParseRange(string raw)
        {
            if (string.IsNullOrEmpty(raw))
                return OffsetRuntime.DefaultRange;

            double value;
            if (!double.TryParse(
                    raw.Trim(),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out value))
                return OffsetRuntime.DefaultRange;

            if (double.IsNaN(value) || double.IsInfinity(value))
                return OffsetRuntime.DefaultRange;

            value = Math.Round(value, 1, MidpointRounding.AwayFromZero);
            if (value < OffsetRuntime.DefaultRange)
                return OffsetRuntime.DefaultRange;
            if (value > OffsetRuntime.MaxRange)
                value = OffsetRuntime.MaxRange;

            return value;
        }

        private static string FindIni()
        {
            string currentDirectory = GetCurrentDirectorySafe();

            string baseDirectory = null;
            try
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            catch
            {
            }

            string[] candidates = new string[]
            {
                CombineSafe(currentDirectory, "mu3-VO.ini"),
                CombineSafe(baseDirectory, "mu3-VO.ini"),
                CombineSafe(baseDirectory, "package\\mu3-VO.ini"),
                CombineSafe(baseDirectory, "..\\..\\mu3-VO.ini"),
                "mu3-VO.ini",
                ".\\mu3-VO.ini"
            };

            for (int i = 0; i < candidates.Length; i++)
            {
                string path = candidates[i];
                if (string.IsNullOrEmpty(path))
                    continue;

                try
                {
                    if (File.Exists(path))
                        return path;
                }
                catch
                {
                }
            }

            return null;
        }

        private static string GetCurrentDirectorySafe()
        {
            try
            {
                return Directory.GetCurrentDirectory();
            }
            catch
            {
                return null;
            }
        }

        private static string CombineSafe(string directory, string file)
        {
            if (string.IsNullOrEmpty(directory))
                return null;

            try
            {
                return Path.Combine(directory, file);
            }
            catch
            {
                return null;
            }
        }
    }
}
