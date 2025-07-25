using MU3.DataStudio;
using MU3.DB;
using MU3.Notes;
using MU3.Util;
using System.Collections.Generic;
using UnityEngine;

namespace MU3.Battle
{
    public class patch_EnemyManager : EnemyManager
    {
        private bool _enable;
        private EnemyBoss _boss;
        private float _frameCur;
        private float _frameState;
        private int _wave;
        private EnemyZakoList _zakoList = new EnemyZakoList();
        private NotesManager ntMgr => gameEngine.notesManager;
        private GameEngine gameEngine => SingletonMonoBehaviour<GameEngine>.instance;
        private int _stateLastID;
        private int _stateID;
        private RandomList _randomList = new RandomList(4);

        public new void update(float frameCur, MU3.Data.MusicData musicData)
        {
            if (!_enable)
            {
                return;
            }
            _frameState += frameCur - _frameCur;
            _frameCur = frameCur;
            int curWave = ntMgr.curWave;
            if (_wave != curWave)
            {
                NotesManager.WaveDetailData waveDetailData = null;
                if (_wave >= 0)
                {
                    waveDetailData = ntMgr.waveDetailDataList[_wave];
                }
                NotesManager.WaveDetailData waveDetailData2 = ntMgr.waveDetailDataList[Mathf.Max(curWave, 0)];
                if (_wave >= 0)
                {
                    foreach (EnemyZako zako in _zakoList)
                    {
                        if (zako.param.wave == _wave)
                        {
                            zako.run();
                        }
                    }
                }
                bool flag = curWave == waveBossID;
                if (flag)
                {
                    _boss.forward();
                }
                else
                {
                    makeZakoSet(curWave, 0);
                }
                if (curWave > 0)
                {
                    gameEngine.battleUI.setWave(curWave);
                    gameEngine.battleUI.setEnemyLife(10000, isDamage: false);
                    AttributeType attrBef = AttributeType.Fire;
                    if (waveDetailData != null)
                    {
                        attrBef = waveDetailData.attr;
                    }
                    AttributeType attr = waveDetailData2.attr;
                    gameEngine.startWaveShiftEffect(attrBef, attr);
                    //Singleton<SoundManager>.instance.play(45);
                    if (flag)
                    {
                        //Singleton<SoundManager>.instance.play(54);
                        int bossVoiceNo = musicData.bossVoiceNo;
                        if (bossVoiceNo <= 1)
                        {
                            //gameEngine.playBossSound(130);
                        }
                        else if (bossVoiceNo == 2)
                        {
                            //gameEngine.playBossSound(1411);
                        }
                        else if (bossVoiceNo >= 3)
                        {
                            //gameEngine.playBossSound(1412);
                        }
                        gameEngine.startOverkillEffect();
                    }
                }
                _wave = curWave;
            }
            else if (_wave >= 0)
            {
                foreach (EnemyZako zako2 in _zakoList)
                {
                    if (zako2.getCurrentState() == EnemyZako.EState.Play && zako2.param.wave < _wave)
                    {
                        zako2.run();
                    }
                }
            }
            if (_stateLastID >= 0)
            {
                _stateID = _stateLastID + 1;
                _stateLastID = -1;
            }
            _boss.update(frameCur);
            for (int num = _zakoList.Count - 1; num >= 0; num--)
            {
                EnemyZako enemyZako = _zakoList[num];
                enemyZako.update(frameCur);
                if (enemyZako.isStateEnd() || enemyZako.getCurrentState() == EnemyZako.EState.Finish)
                {
                    enemyZako.destroy();
                    _zakoList.RemoveAt(num);
                }
            }
        }

        private void makeZakoSet(int wave, int set)
        {
            NotesManager.WaveDetailData wDD = getWDD();
            if (wDD != null)
            {
                _randomList.shuffle();
                for (int i = 0; i < 4; i++)
                {
                    EnemyZako enemyZako = new EnemyZako();
                    EnemyZako.Param param = new EnemyZako.Param();
                    param.wave = wave;
                    param.setNum = set;
                    param.index = i;
                    param.zakoID = ZakoID.ZakoBattle01;
                    param.attr = wDD.attr;
                    param.indexNum = 4;
                    param.arrayNum = _randomList[i];
                    param.playerTransform = gameEngine.player.transform;
                    enemyZako.initialize(param);
                    _zakoList.Add(enemyZako);
                }
                if (wave != 0 || set != 0)
                {
                    //Singleton<SoundManager>.instance.play(69);
                }
            }
        }

        private NotesManager.WaveDetailData getWDD()
        {
            return ntMgr.curWaveDetailData;
        }

        public new void playBossSound(int soundId)
        {
            //_boss.playSound(soundId);
        }

        private new void makeReward(int wave, int set)
        {
            BattleReward battleReward = gameEngine.battleReward;
            List<DestroyReward> list = null;
            BattlePhase battlePhase = ((wave == waveBossID) ? BattlePhase.Boss : BattlePhase.Zako);
            int waveCount = ((wave != waveBossID) ? wave : 0);
            list = battleReward.pickDestroyReward(battlePhase, waveCount, set);
            battleReward.getReward(list);
            //Singleton<SoundManager>.instance.play(92);
            bool flag = false;
            bool flag2 = false;
            foreach (DestroyReward item in list)
            {
                switch (item.kind)
                {
                    case DestroyRewardKindID.Money:
                        flag = true;
                        break;
                    case DestroyRewardKindID.Jewel:
                        flag2 = true;
                        break;
                }
            }
            if (flag)
            {
                //Singleton<SoundManager>.instance.play(71);
            }
            if (flag2)
            {
                //Singleton<SoundManager>.instance.play(23);
            }
        }
    }
}
