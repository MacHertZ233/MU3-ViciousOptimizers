using MU3.Game;
using System.Collections.Generic;

namespace MU3.Notes
{
    public class patch_NotesManager : NotesManager
    {
        private List<GuideSEData> _guideSEList = new List<GuideSEData>(3);
        public extern bool orig_loadScore(SessionInfo sessionInfo, bool isStageDazzling);
        public bool loadScore(SessionInfo sessionInfo, bool isStageDazzling)
        {
            bool output = orig_loadScore(sessionInfo, isStageDazzling);
            foreach (GuideSEData guideSEData in _guideSEList)
            {
                guideSEData.frame = guideSEData.frame - GameOption.timing;
            }
            return output;
        }
    }
}
