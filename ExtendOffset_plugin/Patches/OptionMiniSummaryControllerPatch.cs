using HarmonyLib;
using MU3;
using MU3.CustomUI;
using MU3.User;
using UnityEngine;

namespace ExtendOffset_plugin.Patches
{
    [HarmonyPatch(typeof(OptionMiniSummaryController))]
    internal class OptionMiniSummaryControllerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("setParam")]
        static bool SetParamPrefix(OptionMiniSummaryController __instance, UserOption.OptionName id, int value)
        {
            if (id == UserOption.OptionName.Timing || id == UserOption.OptionName.JudgeAdjustment)
            {
                GameObject[] arrayIcon = Traverse.Create(__instance).Field("arrayIcon").GetValue<GameObject[]>();
                arrayIcon[(int)id].transform.Find("NUL_Option_mini_Icon/NUM_DecimalPoint_1keta").GetComponent<MU3UICounter>().Counter = -10.0 + 0.1 * (double)value;
                return false;
            }
            return true;
        }
    }
}
