using VO;

namespace MU3
{
    public class patch_BattleUI : BattleUI
    {
        public static bool playSE = false;
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
        private extern void orig_Awake();
        private void Awake()
        {
            orig_Awake();
            parseConfig();
        }
    }

}
