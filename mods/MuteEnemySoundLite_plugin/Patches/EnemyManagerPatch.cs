using HarmonyLib;
using MU3;
using MU3.Battle;
using MU3.Notes;
using static MU3.Battle.EnemyManager;

namespace MuteEnemySoundLite_plugin.Patches
{
    [HarmonyPatch(typeof(EnemyManager))]
    public class EnemyManagerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("makeZakoSet")]
        static bool MakeZakoSetPrefix(EnemyManager __instance, int wave, int set,
            ref RandomList ____randomList, ref EnemyZakoList ____zakoList)
        {
            GameEngine gameEngine = Traverse.Create(__instance).Property<GameEngine>("gameEngine").Value;
            NotesManager.WaveDetailData wDD = Traverse.Create(__instance).Method("getWDD").GetValue<NotesManager.WaveDetailData>();

            if (wDD != null)
            {
                ____randomList.shuffle();
                for (int i = 0; i < 4; i++)
                {
                    EnemyZako enemyZako = new EnemyZako();
                    EnemyZako.Param param = new EnemyZako.Param();
                    param.wave = wave;
                    param.setNum = set;
                    param.index = i;
                    param.zakoID = ZakoID.ZakoBattle01;
                    param.attr = wDD.attr;
                    param.indexNum = 4;
                    param.arrayNum = ____randomList[i];
                    param.playerTransform = gameEngine.player.transform;
                    enemyZako.initialize(param);
                    ____zakoList.Add(enemyZako);
                }
                if (wave != 0 || set != 0)
                {
                    //Singleton<SoundManager>.instance.play(69);
                }
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("playBossSound")]
        static bool PlayBossSoundPrefix(EnemyManager __instance, int soundId)
        {
            return false;
        }
    }
}
