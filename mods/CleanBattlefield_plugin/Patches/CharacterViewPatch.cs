using HarmonyLib;
using MU3.Battle;
using UnityEngine;

namespace CleanBattlefield_plugin.Patches
{
    [HarmonyPatch(typeof(CharacterView))]
    public class CharacterViewPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("createEffect")]
        public static bool createEffectPrefix(ref GameObject __result, AssetAssign.NoteEffect.Type eff, bool isSetPos = true)
        {
            if ((eff == AssetAssign.NoteEffect.Type.explosionZako) && (BattleUIPatch.config & 64) == 0)
            {
                __result = null;
                return false;
            }
            return true;
        }
    }
}
