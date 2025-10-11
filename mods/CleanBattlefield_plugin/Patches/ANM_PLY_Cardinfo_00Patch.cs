using HarmonyLib;
using MU3;

namespace CleanBattlefield_plugin.Patches
{
    [HarmonyPatch(typeof(ANM_PLY_Cardinfo_00))]
    public class ANM_PLY_Cardinfo_00Patch
    {
        private static bool show = false;

        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        static void AwakePostfix(ref ANM_PLY_Cardinfo_00 __instance)
        {
            int config = BattleUIPatch.config;
            show = (config & 8) != 0;
        }

        [HarmonyPrefix]
        [HarmonyPatch("state", MethodType.Setter)]
        static bool statePrefix(ref ANM_PLY_Cardinfo_00 __instance, ref ANM_PLY_Cardinfo_00.State value)
        {
            __instance.gameObject.SetActive(show);
            return show;
        }

        [HarmonyPrefix]
        [HarmonyPatch("Show", MethodType.Setter)]
        static bool ShowPrefix(ref ANM_PLY_Cardinfo_00 __instance, ref bool value)
        {
            __instance.gameObject.SetActive(show);
            return show;
        }
    }
}
