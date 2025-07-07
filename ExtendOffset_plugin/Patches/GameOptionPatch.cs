using HarmonyLib;
using MU3.Notes;
using MU3.User;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExtendOffset_plugin.Patches
{
    [HarmonyPatch(typeof(GameOption))]
    class GameOptionPatch
    {
        public static ReadOnlyCollection<float> timingTbl = Array.AsReadOnly<float>((from i in Enumerable.Range(0, 201)
                                                                                     select -10f + (float)i * 0.1f).ToArray<float>());
        [HarmonyPrefix]
        [HarmonyPatch("calcTiming")]
        static bool calcTimingPrefix(ref float __result, ref UserOption.eTiming eTiming)
        {
            __result = timingTbl[(int)eTiming];
            return false;
        }
    }
}
