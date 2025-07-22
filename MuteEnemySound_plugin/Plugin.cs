using BepInEx;
using HarmonyLib;
using MuteEnemySound_plugin.Patches;

namespace MuteEnemySound_plugin
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class MuteEnemySoundPlugin : BaseUnityPlugin
    {
        const string modGUID = "machertz233.muteenemysound";
        const string modName = "MuteEnemySound";
        const string modVersion = "1.0.1";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal BepInEx.Logging.ManualLogSource logger;
        void Awake()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            harmony.PatchAll(typeof(MuteEnemySoundPlugin));
            harmony.PatchAll(typeof(BattleRewardPatch));
            harmony.PatchAll(typeof(EnemyManagerPatch));
            harmony.PatchAll(typeof(EnemyViewPatch));
            harmony.PatchAll(typeof(UIEnemyInfoPatch));

            logger.LogInfo($"Plugin {modName} is loaded!");
        }
    }
}
