using HarmonyLib;
using MU3;
using VO;

namespace ShutTheFxxkUp_plugin.Patches
{
    [HarmonyPatch(typeof(BattleUI))]
    public class BattleUIPatch
    {
        public static bool playSE = false;

        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        static void AwakePostfix()
        {
            parseConfig();
        }

        private static void parseConfig()
        {
            IniParser reader = new IniParser(".\\mu3-VO.ini");
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            int config = 0;

            list.Add("PlaySoundEffects");

            foreach (string key in list)
                config = (config << 1) + int.Parse(reader.ReadValue("ShutTheFxxkUp", key));

            list.Clear();

            playSE = (config & 1) != 0;
        }
    }
}
