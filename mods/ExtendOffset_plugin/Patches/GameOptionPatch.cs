using HarmonyLib;
using MU3.Notes;

namespace ExtendOffset_plugin.Patches
{
    [HarmonyPatch(typeof(GameOption), "calcTiming")]
    internal static class GameOptionPatch
    {
        [HarmonyPrefix]
        private static bool Prefix(object[] __args, ref float __result)
        {
            __result = OffsetRuntime.TimingForCode(
                OffsetRuntime.ToInt(__args[0]));
            return false;
        }
    }
}