using MU3.Battle;
using UnityEngine;

namespace MU3
{
    public class patch_BattleUI : BattleUI
    {
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            System.Text.StringBuilder retVal,
            int size,
            string filePath);

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

            if ((config & 32) == 0) _playerInfo.gameObject.SetActive(false);
            if ((config & 16) == 0) _enemyInfo.gameObject.SetActive(false);
            if ((config & 8) == 0) _retireInfo.gameObject.SetActive(false);
            if ((config & 4) == 0) _cardInfo.gameObject.SetActive(false);
            if ((config & 2) == 0) _comboInfo.gameObject.SetActive(false);
        }

        public new void playEnemyDamage(Vector3 posWorld, Vector2 posScreenOffset, ColorEnemyDamage color, SizeEnemyDamage size, int damage, float scale)
        {
            if ((config & 1) != 0)
                _enemyDamageInfo.play(posWorld, posScreenOffset, (UIEnemyDamage.Color)color, (UIEnemyDamage.Size)size, damage, scale);

        }

        private static void parseConfig()
        {
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            config = 0;

            list.Add("ShowPlayerInfo");
            list.Add("ShowEnemyInfo");
            list.Add("ShowRetireInfo");
            list.Add("ShowCardInfo");
            list.Add("ShowComboInfo");
            list.Add("ShowDamageNumbers");

            foreach (string key in list)
            {
                GetPrivateProfileString("CleanBattlefield", key, "", temp, 255, ".\\mu3-VO.ini");
                config = config << 1 + int.Parse(temp.ToString());
                temp.Clear();
            }
            list.Clear();
        }
    }
}
