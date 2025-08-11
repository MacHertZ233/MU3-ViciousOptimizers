using MU3.Battle;
using HarmonyLib;

namespace CleanBattlefield_plugin.Patches
{
    [HarmonyPatch(typeof(GameEngine))]
    public class GameEnginePatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("startWaveShiftEffect")]
        public static bool startWaveShiftEffectPrefix()
        {
            return (BattleUIPatch.config & 64) != 0;
        }

        [HarmonyPrefix]
        [HarmonyPatch("startOverkillEffect")]
        public static bool startOverkillEffectPrefix()
        {
            return (BattleUIPatch.config & 64) != 0;
        }
    }
}
