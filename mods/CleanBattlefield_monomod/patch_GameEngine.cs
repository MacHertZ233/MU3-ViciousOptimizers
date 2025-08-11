using MU3.DataStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MU3.Battle
{
    public class patch_GameEngine : GameEngine
    {
        public extern void orig_startWaveShiftEffect(AttributeType attrBef, AttributeType attrAft);
        public void startWaveShiftEffect(AttributeType attrBef, AttributeType attrAft)
        {
            if ((patch_BattleUI.config & 64) != 0) orig_startWaveShiftEffect(attrBef, attrAft);
        }

        public extern void orig_startOverkillEffect();
        public void startOverkillEffect()
        {
            if ((patch_BattleUI.config & 64) != 0) orig_startOverkillEffect();
        }
    }
}
