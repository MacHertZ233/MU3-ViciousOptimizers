using HarmonyLib;
using MU3.Notes;
using MU3.Game;
using static MU3.Notes.NotesManager;
using System.Collections.Generic;

namespace ShiftSECount_plugin.Patches
{
    [HarmonyPatch(typeof(NotesManager))]
    internal class NotesManagerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("loadScore")]
        static void LoadScorePostfix(NotesManager __instance, SessionInfo sessionInfo, bool isStageDazzling)
        {
            foreach (GuideSEData guideSEData in Traverse.Create(__instance).Field("_guideSEList").GetValue<List<GuideSEData>>())
            {
                {
                    guideSEData.frame -= GameOption.timing;
                }
            }
        }
    }
}
