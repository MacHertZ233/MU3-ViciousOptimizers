namespace MU3
{
    internal class patch_UIEnemyInfo : UIEnemyInfo
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
                    //Singleton<SoundManager>.instance.play(52);
                }
            }
        }

        private void setDispMode(DispMode dispMode)
        {
            if (_dispMode != dispMode)
            {
                switch (dispMode)
                {
                    case DispMode.LifeGauge:
                        _enemyLife.isDefeated = false;
                        break;
                    case DispMode.Overkill:
                        _enemyLife.isDefeated = true;
                        break;
                }
                _dispMode = dispMode;
                updateOverDamageDisp();
            }
        }
        private void updateLifeGauge()
        {
            _hpGauge.setValue(0.01f * (float)_lifeValue);
        }
        private void updateOverDamageGauge()
        {
            float overDamageValue = ((_lifeValue <= 0) ? (-0.01f * (float)_lifeValue) : (-1f));
            _overDamage.overDamageValue = overDamageValue;
        }
        private void updateOverDamageDisp()
        {
            _overDamage.isIn = state == State.In && _dispMode == DispMode.Overkill;
        }
    }
}
