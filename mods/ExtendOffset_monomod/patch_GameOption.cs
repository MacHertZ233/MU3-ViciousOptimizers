using System.Reflection;
using ExtendOffset_monomod;
using MU3.Notes;

namespace MU3.Notes
{
    public class patch_GameOption : GameOption
    {
        private static int GetCode(string fieldName)
        {
            PropertyInfo opProperty = typeof(GameOption).GetProperty(
                "op",
                BindingFlags.Public | BindingFlags.Static);

            if (opProperty == null)
                return 20;

            object op = opProperty.GetValue(null, null);
            return OffsetRuntime.GetCode(op, fieldName);
        }

        public static float get_timing()
        {
            return OffsetRuntime.TimingForCode(GetCode("timing"));
        }

        public static float get_timingMsec()
        {
            return 1000f * get_timing() / 60f;
        }

        public static float get_judgeAdjustment()
        {
            return OffsetRuntime.TimingForCode(
                GetCode("judgeAdjustment"));
        }

        public static float get_judgeAdjustmentMsec()
        {
            return 1000f * get_judgeAdjustment() / 60f;
        }
    }
}
