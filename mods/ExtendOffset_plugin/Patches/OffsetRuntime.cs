using System;
using System.Globalization;
using System.Reflection;
using MU3.User;

namespace ExtendOffset_plugin
{
    internal static class OffsetRuntime
    {
        public const double DefaultRange = 10.0;
        public const double MaxRange = 100.0;

        public static double Range = DefaultRange;
        public static int Steps = 20;
        public static int Span = 0;

        public static float[] CodeLut = BuildCodeLut(20, 0);

        public static float[] UiLut = BuildUiLut(20);

        public static int MaxUiIndex
        {
            get { return Steps * 2; }
        }

        public static int ZeroUiIndex
        {
            get { return Steps; }
        }

        public static int ZeroCode
        {
            get { return 20; }
        }

        public static int MaxPositiveCode
        {
            get { return 40 + Span; }
        }

        public static int MaxStorageCode
        {
            get { return 40 + Span * 2; }
        }

        public static void Configure(double range)
        {
            if (double.IsNaN(range) || double.IsInfinity(range))
                range = DefaultRange;

            range = Math.Round(range, 1, MidpointRounding.AwayFromZero);
            if (range < DefaultRange)
                range = DefaultRange;
            if (range > MaxRange)
                range = MaxRange;

            Steps = (int)Math.Round(
                range * 10.0,
                MidpointRounding.AwayFromZero);
            if (Steps < 20)
                Steps = 20;

            Span = Steps - 20;
            Range = Steps / 10.0;

            CodeLut = BuildCodeLut(Steps, Span);
            UiLut = BuildUiLut(Steps);
        }

        private static float[] BuildCodeLut(int steps, int span)
        {
            float[] table = new float[steps * 2 + 1];

            for (int code = 0; code <= 40; code++)
                table[code] = (float)(-2.0 + 0.1 * code);

            for (int i = 1; i <= span; i++)
            {
                table[40 + i] = (float)(2.0 + 0.1 * i);
                table[40 + span + i] = (float)(-2.0 - 0.1 * i);
            }

            return table;
        }

        private static float[] BuildUiLut(int steps)
        {
            float[] table = new float[steps * 2 + 1];
            double range = steps / 10.0;

            for (int i = 0; i < table.Length; i++)
                table[i] = (float)(-range + i * 0.1);

            return table;
        }

        public static float TimingForCode(int code)
        {
            code = ClampCode(code);
            return CodeLut[code];
        }

        public static float TimingForUi(int ui)
        {
            if (ui < 0)
                ui = 0;
            if (ui > MaxUiIndex)
                ui = MaxUiIndex;
            return UiLut[ui];
        }

        public static int CodeToUi(int code)
        {
            code = ClampCode(code);

            if (code <= MaxPositiveCode)
                return code + Span;

            return MaxStorageCode - code;
        }

        public static int UiToCode(int ui)
        {
            if (ui < 0)
                ui = 0;
            if (ui > MaxUiIndex)
                ui = MaxUiIndex;

            if (ui >= Span)
                return ui - Span;

            // 负向扩展
            return MaxStorageCode - ui;
        }

        public static int ClampCode(int code)
        {
            if (code < 0)
                return 0;
            if (code > MaxStorageCode)
                return MaxStorageCode;
            return code;
        }

        public static int ToInt(object value)
        {
            if (value == null)
                return 20;

            try
            {
                return Convert.ToInt32(
                    value,
                    CultureInfo.InvariantCulture);
            }
            catch
            {
                return 20;
            }
        }

        public static string OptionNameOf(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public static int GetOptionIndex(string name)
        {
            try
            {
                return Convert.ToInt32(
                    Enum.Parse(typeof(UserOption.OptionName), name),
                    CultureInfo.InvariantCulture);
            }
            catch
            {
                return -1;
            }
        }

        public static bool IsTimingName(string name)
        {
            return name == "Timing" || name == "JudgeAdjustment";
        }

        public static FieldInfo GetField(object instance, string name)
        {
            if (instance == null)
                return null;

            Type type = instance.GetType();
            while (type != null)
            {
                FieldInfo field = type.GetField(
                    name,
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic);
                if (field != null)
                    return field;

                type = type.BaseType;
            }

            return null;
        }

        public static int GetCode(object dataSet, string fieldName)
        {
            FieldInfo field = GetField(dataSet, fieldName);
            if (field == null)
                return 20;

            object value = field.GetValue(dataSet);
            if (value == null)
                return 20;

            try
            {
                return Convert.ToInt32(
                    value,
                    CultureInfo.InvariantCulture);
            }
            catch
            {
                return 20;
            }
        }

        public static void SetCode(object dataSet, string fieldName, int code)
        {
            FieldInfo field = GetField(dataSet, fieldName);
            if (field == null)
                return;

            field.SetValue(
                dataSet,
                Enum.ToObject(field.FieldType, ClampCode(code)));
        }
    }
}