using BepInEx;
using HarmonyLib;
using GuideSEOffset_plugin.Patches;

namespace GuideSEOffset_plugin
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class GuideSEOffsetPlugin : BaseUnityPlugin
    {
        const string modGUID = "machertz233.guideseoffset";
        const string modName = "GuideSEOffset";
        const string modVersion = "1.1.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal BepInEx.Logging.ManualLogSource logger;
        void Awake()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(modName);
            harmony.PatchAll(typeof(GuideSEOffsetPlugin));
            harmony.PatchAll(typeof(NotesManagerPatch));

            logger.LogInfo($"Plugin {modName} ({modGUID}) is loaded!");
        }
    }
}
