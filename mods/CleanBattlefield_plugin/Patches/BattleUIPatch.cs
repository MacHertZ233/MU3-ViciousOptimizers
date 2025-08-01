using HarmonyLib;
using MU3;
using MU3.Battle;
using UnityEngine;
using VO;

namespace CleanBattlefield_plugin.Patches
{
    [HarmonyPatch(typeof(BattleUI))]
    public class BattleUIPatch
    {
        public static int config = 0;

        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        static void AwakePostfix(ref UIPlayerInfo ____playerInfo, ref UIEnemyInfo ____enemyInfo, ref UIEnemyDamage ____enemyDamageInfo, ref UIRetireInfo ____retireInfo, ref UICardInfo ____cardInfo, ref UICombo ____comboInfo)
        {
            parseConfig();

            ____playerInfo.gameObject.SetActive((config & 32) != 0);
            ____enemyInfo.gameObject.SetActive((config & 16) != 0);
            ____retireInfo.gameObject.SetActive((config & 8) != 0);
            //____cardInfo.gameObject.SetActive((config & 4) != 0);
            ____comboInfo.gameObject.SetActive((config & 2) != 0);

        }

        [HarmonyPrefix]
        [HarmonyPatch("playEnemyDamage")]
        static bool PlayEnemyDamagePrefix(Vector3 posWorld, Vector2 posScreenOffset, ColorEnemyDamage color, SizeEnemyDamage size, int damage, float scale)
        {
            return (config & 1) != 0;
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
