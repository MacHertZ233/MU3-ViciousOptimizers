using HarmonyLib;
using MU3;
using UnityEngine;

namespace HideStats_plugin.Patches
{
    [HarmonyPatch(typeof(BattleUI))]
    public class BattleUIPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        static void AwakePostfix(ref UIPlayerInfo ____playerInfo, ref UIEnemyInfo ____enemyInfo, ref UIEnemyDamage ____enemyDamageInfo)
        {
            ____playerInfo.gameObject.SetActive(false);
            ____enemyInfo.gameObject.SetActive(false);
            ____enemyDamageInfo.gameObject.SetActive(false);
        }

        [HarmonyPrefix]
        [HarmonyPatch("Update")]
        static bool UpdatePrefix(ref UIJudge ____judgeInfo)
        {
            ____judgeInfo.execute();
            return false;
        }
    }
}
