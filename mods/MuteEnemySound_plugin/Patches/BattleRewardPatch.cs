// excessive modification?

using HarmonyLib;
using MU3.Battle;
using MU3.Util;
using System.Collections.Generic;

namespace MuteEnemySound_plugin.Patches
{
    [HarmonyPatch(typeof(BattleReward))]
    public class BattleRewardPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("getReward")]
        static bool GetRewardPrefix(BattleReward __instance, List<DestroyReward> rewardList)
        {
            List<DestroyReward> _destroyRewards = Traverse.Create(__instance).Field("_destroyRewards").GetValue<List<DestroyReward>>();
            int _destroyRewardCount = Traverse.Create(__instance).Field("_destroyRewardCount").GetValue<int>();

            _destroyRewards.AddRange(rewardList);
            _destroyRewardCount++;
            Reward reward = BattleReward.calcRewards(_destroyRewards);
            SingletonMonoBehaviour<GameEngine>.instance.battleUI.getReward(_destroyRewardCount, reward.jewelBox, reward.moneyBox);
            //Singleton<SoundManager>.instance.play(92);

            return false;
        }
    }
}
