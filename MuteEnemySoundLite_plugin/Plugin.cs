using BepInEx;
using HarmonyLib;
using MuteEnemySoundLite_plugin.Patches;

namespace MuteEnemySoundLite_plugin
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class MuteEnemySoundLitePlugin : BaseUnityPlugin
    {
        const string modGUID = "machertz233.muteenemysoundlite";
        const string modName = "MuteEnemySoundLite";
        const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal BepInEx.Logging.ManualLogSource logger;
        void Awake()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(modName);
            harmony.PatchAll(typeof(MuteEnemySoundLitePlugin));
            harmony.PatchAll(typeof(EnemyManagerPatch));

            logger.LogInfo($"Plugin {modName} ({modGUID}) is loaded!");
        }
    }
}