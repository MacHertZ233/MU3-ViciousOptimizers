using MU3.Battle;
using MU3.Sound;
using MU3.Util;

namespace MU3
{
    public class patch_UIEnemyInfo : UIEnemyInfo
    {
        private enum DispMode
        {
            LifeGauge,
            Overkill,
            Max
        }

        private DispMode _dispMode;
        private ANM_PLY_EnemyLife_00 _enemyLife;
        private HPGauge _hpGauge;
        private ANM_PLY_OverKill_NUM_00 _overDamage;
        private int _lifeValue;

        public new int lifeValue
        {
            get
            {
                return _lifeValue;
            }
            set
            {
                bool flag = value <= 0 && _lifeValue > 0;
                _lifeValue = value;
                if (value > 0 || mode == Mode.Zako)
                {
                    setDispMode(DispMode.LifeGauge);
                    updateLifeGauge();
                }
                else
                {
                    setDispMode(DispMode.Overkill);
                    updateOverDamageGauge();
                }
                if (flag)
                {
                    _enemyLife.isDefeated = true;
                    if (patch_BattleUI.playSE)
                        Singleton<SoundManager>.instance.play(52);
                }
            }
        }

        private extern void orig_setDispMode(DispMode dispMode);
        private void setDispMode(DispMode dispMode)
        {
            orig_setDispMode(dispMode);
        }
        private extern void orig_updateLifeGauge();
        private void updateLifeGauge()
        {
            orig_updateLifeGauge();
        }
        private extern void orig_updateOverDamageGauge();
        private void updateOverDamageGauge()
        {
            orig_updateOverDamageGauge();
        }
    }
}
