using BepInEx;
using HarmonyLib;
using ShutTheFxxkUp_plugin.Patches;

namespace ShutTheFxxkUp_plugin
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class ShutTheFxxkUpPlugin : BaseUnityPlugin
    {
        const string modGUID = "machertz233.shutthefxxkup";
        const string modName = "ShutTheFxxkUp";
        const string modVersion = "1.1.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal BepInEx.Logging.ManualLogSource logger;
        void Awake()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(modName);
            harmony.PatchAll(typeof(ShutTheFxxkUpPlugin));
            harmony.PatchAll(typeof(BattleUIPatch));
            harmony.PatchAll(typeof(BattleRewardPatch));
            harmony.PatchAll(typeof(EnemyManagerPatch));
            harmony.PatchAll(typeof(EnemyViewPatch));
            harmony.PatchAll(typeof(UIEnemyInfoPatch));

            logger.LogInfo($"Plugin {modName} ({modGUID}) is loaded!");
        }
    }
}
