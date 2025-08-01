using HarmonyLib;
using MU3;
using MU3.Util;
using MU3.Sound;

namespace ShutTheFxxkUp_plugin.Patches
{
    [HarmonyPatch(typeof(UIEnemyInfo))]
    public class UIEnemyInfoPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UIEnemyInfo.lifeValue), MethodType.Setter)]
        static bool LifeValueSetterPrefix(UIEnemyInfo __instance, int value, ref int ____lifeValue, ref ANM_PLY_EnemyLife_00 ____enemyLife)
        {
            bool flag = value <= 0 && ____lifeValue > 0;
            ____lifeValue = value;
            if (value > 0 || Traverse.Create(__instance).Property("mode").GetValue<UIEnemyInfo.Mode>() == UIEnemyInfo.Mode.Zako)
            {
                Traverse.Create(__instance).Method("setDispMode", 0).GetValue();
                Traverse.Create(__instance).Method("updateLifeGauge").GetValue();
            }
            else
            {
                Traverse.Create(__instance).Method("setDispMode", 1).GetValue();
                Traverse.Create(__instance).Method("updateOverDamageGauge").GetValue();
            }
            if (flag)
            {
                ____enemyLife.isDefeated = true;
                if (BattleUIPatch.playSE) 
                    Singleton<SoundManager>.instance.play(52);
            }

            return false;
        }
    }
}
