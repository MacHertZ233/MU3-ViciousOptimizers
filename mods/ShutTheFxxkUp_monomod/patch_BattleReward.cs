// excessive modification?

using MU3.Util;
using System.Collections.Generic;

namespace MU3.Battle
{
    public class patch_BattleReward : BattleReward
    {
        private int _destroyRewardCount;
        private List<DestroyReward> _destroyRewards = new List<DestroyReward>();
        public new void getReward(List<DestroyReward> rewardList)
        {
            _destroyRewards.AddRange(rewardList);
            _destroyRewardCount++;
            Reward reward = calcRewards(_destroyRewards);
            SingletonMonoBehaviour<GameEngine>.instance.battleUI.getReward(_destroyRewardCount, reward.jewelBox, reward.moneyBox);
            //Singleton<SoundManager>.instance.play(92);
        }
    }
}
