using MU3.Battle;
using UnityEngine;
using VO;

namespace MU3
{
    public class patch_BattleUI : BattleUI
    {
        private UIPlayerInfo _playerInfo;
        private UIEnemyInfo _enemyInfo;
        private UIRetireInfo _retireInfo;
        private UICardInfo _cardInfo;
        private UICombo _comboInfo;
        private UIEnemyDamage _enemyDamageInfo;
        private extern void orig_Awake();

        private static int config = 0;
        private void Awake()
        {
            orig_Awake();

            parseConfig();

            _playerInfo.gameObject.SetActive((config & 32) != 0);
            _enemyInfo.gameObject.SetActive((config & 16) != 0);
            _retireInfo.gameObject.SetActive((config & 8) != 0);
            _cardInfo.gameObject.SetActive((config & 4) != 0);
            _comboInfo.gameObject.SetActive((config & 2) != 0);
        }

        public new void playEnemyDamage(Vector3 posWorld, Vector2 posScreenOffset, ColorEnemyDamage color, SizeEnemyDamage size, int damage, float scale)
        {
            if ((config & 1) != 0)
                _enemyDamageInfo.play(posWorld, posScreenOffset, (UIEnemyDamage.Color)color, (UIEnemyDamage.Size)size, damage, scale);

        }

        private static void parseConfig()
        {
            IniParser reader = new IniParser(".\\mu3-VO.ini");
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            config = 0;

            list.Add("ShowPlayerInfo");
            list.Add("ShowEnemyInfo");
            list.Add("ShowRetireInfo");
            list.Add("ShowCardInfo");
            list.Add("ShowComboInfo");
            list.Add("ShowDamageNumbers");

            foreach (string key in list)
                config = (config << 1) + int.Parse(reader.ReadValue("CleanBattlefield", key));

            list.Clear();
        }
    }
}
