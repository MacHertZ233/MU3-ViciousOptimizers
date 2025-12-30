using VO;
using MU3.Game;

namespace MU3.Notes
{
    public class patch_NotesManager : NotesManager
    {
        // old method exclusively for GuideSE_Count
        //
        // private List<GuideSEData> _guideSEList = new List<GuideSEData>(3);
        // public extern bool orig_loadScore(SessionInfo sessionInfo, bool isStageDazzling);
        // public bool loadScore(SessionInfo sessionInfo, bool isStageDazzling)
        // {
        //     bool output = orig_loadScore(sessionInfo, isStageDazzling);
        //     foreach (GuideSEData guideSEData in _guideSEList)
        //     {
        //         guideSEData.frame = guideSEData.frame - GameOption.timing;
        //     }
        //     return output;
        // }

        private static float offsetCount = 0.0f;
        private static float offsetAnswer = 0.0f;

        // cannot patch "initialize()" and "orig_initialize()", FUCK!
        // have to find another way instead
        public extern bool orig_loadScore(SessionInfo sessionInfo, bool isStageDazzling);
        public bool loadScore(SessionInfo sessionInfo, bool isStageDazzling)
        {
            parseConfig();
            return orig_loadScore(sessionInfo,isStageDazzling);
        }

        public extern void orig_addGuideSE(GuideSEData data);
        public void addGuideSE(GuideSEData data)
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
            }
            orig_addGuideSE(data);
        }

        private static void parseConfig()
        {
            IniParser reader = new IniParser(".\\mu3-VO.ini");

            offsetCount = float.Parse(reader.ReadValue("GuideSEOffset", "OffsetCount"));
            offsetAnswer = float.Parse(reader.ReadValue("GuideSEOffset", "OffsetAnswer"));
        }
    }
}
