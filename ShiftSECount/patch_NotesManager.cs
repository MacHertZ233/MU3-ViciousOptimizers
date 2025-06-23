using MU3.Battle;
using MU3.Data;
using MU3.Game;
using MU3.Reader;
using MU3.Sys;
using MU3.Tutorial;
using MU3.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using UnityEngine;
using static MU3.Notes.NotesManager;

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
