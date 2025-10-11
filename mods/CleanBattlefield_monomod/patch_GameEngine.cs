using MU3.DataStudio;

namespace MU3.Battle
{
    public class patch_GameEngine : GameEngine
    {
        public extern void orig_startWaveShiftEffect(AttributeType attrBef, AttributeType attrAft);
        public void startWaveShiftEffect(AttributeType attrBef, AttributeType attrAft)
        {
            if ((patch_BattleUI.config & 128) != 0) orig_startWaveShiftEffect(attrBef, attrAft);
        }

        public extern void orig_startOverkillEffect();
        public void startOverkillEffect()
        {
            if ((patch_BattleUI.config & 128) != 0) orig_startOverkillEffect();
        }
    }
}
