using MU3.Battle;
using UnityEngine;

namespace MU3
{
    public class patch_BattleUI : BattleUI
    {
        private UIPlayerInfo _playerInfo;
        private UIEnemyInfo _enemyInfo;
        private extern void orig_Awake();
        private void Awake()
        {
            orig_Awake();
            _playerInfo.gameObject.SetActive(false);
            _enemyInfo.gameObject.SetActive(false);
        }

        public new void playEnemyDamage(Vector3 posWorld, Vector2 posScreenOffset, ColorEnemyDamage color, SizeEnemyDamage size, int damage, float scale)
        {
            //_enemyDamageInfo.play(posWorld, posScreenOffset, (UIEnemyDamage.Color)color, (UIEnemyDamage.Size)size, damage, scale);
        }
    }
}
