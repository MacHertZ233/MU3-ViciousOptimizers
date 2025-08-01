using HarmonyLib;
using MU3.Battle;
using MU3.Sound;
using MU3.Util;

namespace ShutTheFxxkUp_plugin.Patches
{
    [HarmonyPatch(typeof(EnemyView))]
    public class EnemyViewPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("down")]
        static bool downPrefix(EnemyView __instance)
        {
            
            __instance.damage(); 
            Traverse.Create(__instance).Method("createEffect", AssetAssign.NoteEffect.Type.explosionBoss).GetValue();
            if (BattleUIPatch.playSE)
                Singleton<SoundManager>.instance.play(19);

            return false;
        }
    }
}