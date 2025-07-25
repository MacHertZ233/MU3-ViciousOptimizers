using HarmonyLib;
using MU3;
using MU3.Battle;
using UnityEngine;

namespace CleanBattlefield_plugin.Patches
{
    [HarmonyPatch(typeof(BattleUI))]
    public class BattleUIPatch
    {
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            System.Text.StringBuilder retVal,
            int size,
            string filePath);

        private static int config = 0;

        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        static void AwakePostfix(ref UIPlayerInfo ____playerInfo, ref UIEnemyInfo ____enemyInfo, ref UIEnemyDamage ____enemyDamageInfo, ref UIRetireInfo ____retireInfo, ref UICardInfo ____cardInfo, ref UICombo ____comboInfo)
        {
            parseConfig();
            System.Console.WriteLine("Config value: " + config);   
            if ((config & 32) == 0) ____playerInfo.gameObject.SetActive(false);
            if ((config & 16) == 0) ____enemyInfo.gameObject.SetActive(false);
            if ((config & 8) == 0) ____retireInfo.gameObject.SetActive(false);
            if ((config & 4) == 0) ____cardInfo.gameObject.SetActive(false);
            if ((config & 2) == 0) ____comboInfo.gameObject.SetActive(false);
            
        }

        [HarmonyPrefix]
        [HarmonyPatch("playEnemyDamage")]
        static bool PlayEnemyDamagePrefix(Vector3 posWorld, Vector2 posScreenOffset, ColorEnemyDamage color, SizeEnemyDamage size, int damage, float scale)
        {
            if ((config & 1) == 0) return false;
            return true;
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
                System.Console.WriteLine("Parsing: " + key);
                GetPrivateProfileString("CleanBattlefield", key, "", temp, 255, ".\\mu3-VO.ini");
                System.Console.WriteLine("Parsed value: " + temp.ToString());
                config = (config << 1) + int.Parse(temp.ToString());
                temp.Clear();
            }
            list.Clear();
        }
    }
}
