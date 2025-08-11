using BepInEx;
using HarmonyLib;
using CleanBattlefield_plugin.Patches;

namespace CleanBattlefield_plugin
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class CleanBattlefieldPlugin : BaseUnityPlugin
    {
        const string modGUID = "machertz233.CleanBattlefield";
        const string modName = "CleanBattlefield";
        const string modVersion = "1.1.2";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal BepInEx.Logging.ManualLogSource logger;

        void Awake()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(modName);

            harmony.PatchAll(typeof(CleanBattlefieldPlugin));
            harmony.PatchAll(typeof(BattleUIPatch));
            harmony.PatchAll(typeof(ANM_PLY_Cardinfo_00Patch));
            harmony.PatchAll(typeof(GameEnginePatch));

            logger.LogInfo($"Plugin {modName} ({modGUID}) is loaded!");
        }
    }
}
