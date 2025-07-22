using BepInEx;
using HarmonyLib;
using HideStats_plugin.Patches;

namespace HideStats_plugin
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class HideStatsPlugin : BaseUnityPlugin
    {
        const string modGUID = "machertz233.hidestats";
        const string modName = "HideStats";
        const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal BepInEx.Logging.ManualLogSource logger;

        void Awake()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(modName);
            harmony.PatchAll(typeof(HideStatsPlugin));
            harmony.PatchAll(typeof(BattleUIPatch));

            logger.LogInfo($"Plugin {modName} ({modGUID}) is loaded!");
        }
    }
}
