using HarmonyLib;
using MU3;
using MU3.Battle;
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
        }

        [HarmonyPrefix]
        [HarmonyPatch("playEnemyDamage")]
        static bool PlayEnemyDamagePrefix(Vector3 posWorld, Vector2 posScreenOffset, ColorEnemyDamage color, SizeEnemyDamage size, int damage, float scale)
        {
            return false;
        }
    }
}
