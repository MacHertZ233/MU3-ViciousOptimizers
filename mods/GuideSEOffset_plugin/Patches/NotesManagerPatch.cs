using HarmonyLib;
using MU3.Notes;
using MU3.Game;
using static MU3.Notes.NotesManager;
using System.Collections.Generic;
using System.Net;
using VO;

namespace GuideSEOffset_plugin.Patches
{
    [HarmonyPatch(typeof(NotesManager))]
    public class NotesManagerPatch
    {
        private static float offsetCount = 0.0f;
        private static float offsetAnswer = 0.0f;

        // old method exclusively for GuideSE_Count
        //
        // [HarmonyPostfix]
        // [HarmonyPatch("loadScore")]
        // static void LoadScorePostfix(NotesManager __instance, SessionInfo sessionInfo, bool isStageDazzling)
        // {
        //     foreach (GuideSEData guideSEData in Traverse.Create(__instance).Field("_guideSEList").GetValue<List<GuideSEData>>())
        //     {
        //         {
        //             guideSEData.frame -= GameOption.timing;
        //         }
        //     }
        // }

        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        static void AwakePostfix()
        {
            parseConfig();
        }

        [HarmonyPrefix]
        [HarmonyPatch("addGuideSE")]
        static bool AddGuideSEPrefix(ref GuideSEData data)
        {
            if (data.type < NotesManagerSE.GuideSE_MAX)
            {
                data.frame -= GameOption.timing; // counteracts the effect of offset A

                if (data.type == NotesManagerSE.GuideSE_Count)
                {
                    data.frame += offsetCount;
                }
                else
                {
                    data.frame += offsetAnswer;
                }
                //Traverse.Create(__instance).Field("_guideSEList").GetValue<List<GuideSEData>>().Add(data);
            }
            return true;
        }

        private static void parseConfig()
        {
            IniParser reader = new IniParser(".\\mu3-VO.ini");

            offsetCount = float.Parse(reader.ReadValue("GuideSEOffset", "OffsetCount"));
            offsetAnswer = float.Parse(reader.ReadValue("GuideSEOffset", "OffsetAnswer"));
        }
    }
}
