using BepInEx;
using BepInEx.Logging;
using ExtendOffset_plugin.Patches;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtendOffset_plugin
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class ExtendOffsetPlugin : BaseUnityPlugin
    {
        const string modGUID = "machertz233.extendoffset";
        const string modName = "ExtendOffset";
        const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal ManualLogSource logger;

        void Awake()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            harmony.PatchAll(typeof(ExtendOffsetPlugin));
            harmony.PatchAll(typeof(GameOptionPatch));
            harmony.PatchAll(typeof(UserOptionPatch));
            harmony.PatchAll(typeof(OptionSelecterControllerPatch));
            harmony.PatchAll(typeof(OptionMiniSummaryControllerPatch));

            logger.LogInfo($"Plugin {modName} is loaded!");
        }
    }
}
