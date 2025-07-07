using BepInEx;
using HarmonyLib;
using ShiftSECount_plugin.Patches;

namespace ShiftSECount_plugin
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class ShiftSECountPlugin : BaseUnityPlugin
    {
        const string modGUID = "machertz233.shiftsecount";
        const string modName = "ShiftSECount";
        const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal BepInEx.Logging.ManualLogSource logger;
        void Awake()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            harmony.PatchAll(typeof(ShiftSECountPlugin));
            harmony.PatchAll(typeof(NotesManagerPatch));

            logger.LogInfo($"Plugin {modName} is loaded!");
        }
    }
}
