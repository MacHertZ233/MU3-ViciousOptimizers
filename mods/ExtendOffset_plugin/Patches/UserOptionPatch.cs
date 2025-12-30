using HarmonyLib;
using MU3.User;
using static MU3.User.UserOption;

namespace ExtendOffset_plugin.Patches
{
    [HarmonyPatch(typeof(UserOption))]
    class UserOptionPatch
    {
        // Suppose there's a modded eTiming enum
        // from n100 = 0 to p100 = 200
        
        [HarmonyPatch(typeof(DataSet))]
        public class DataSetPatch
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(DataSet), MethodType.Constructor)]
            static void ConstructorPostfix(ref DataSet __instance)
            {
                Traverse.Create(__instance).Field("timing").SetValue(100);
                Traverse.Create(__instance).Field("judgeAdjustment").SetValue(100);
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(DataSet), nameof(DataSet.Timing), MethodType.Setter)]
            static bool TimingSetterPrefix(ref UserOption.eTiming value, ref DataSet __instance)
            {
                if ((int)value > 200)
                {
                    Traverse.Create(__instance).Field("timing").SetValue(200);
                }
                else if ((int)value < 0)
                {
                    Traverse.Create(__instance).Field("timing").SetValue(0);
                }
                else
                    Traverse.Create(__instance).Field("timing").SetValue((int)value);

                return false;
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(DataSet), nameof(DataSet.JudgeAdjustment), MethodType.Setter)]
            static bool JudgeAdjustmentSetterPrefix(ref UserOption.eTiming value, ref DataSet __instance)
            {
                if ((int)value > 200)
                {
                    Traverse.Create(__instance).Field("judgeAdjustment").SetValue(200);
                }
                else if ((int)value < 0)
                {
                    Traverse.Create(__instance).Field("judgeAdjustment").SetValue(0);
                }
                else
                    Traverse.Create(__instance).Field("judgeAdjustment").SetValue((int)value);
                
                return false;
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(DataSet), "isMax")]
            static bool IsMaxPrefix(ref UserOption.OptionName id, ref bool __result, ref DataSet __instance)
            {
                if (id == OptionName.JudgeAdjustment || id == OptionName.Timing)
                {
                    __result = Traverse.Create(__instance).Field("judgeAdjustment").GetValue<int>() == 200;
                    return false;
                }
                return true;
            }
        }

    }
}
