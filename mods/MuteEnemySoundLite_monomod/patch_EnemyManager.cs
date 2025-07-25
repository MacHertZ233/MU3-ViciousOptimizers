using MU3.Notes;
using MU3.Util;

namespace MU3.Battle
{
    public class patch_EnemyManager : EnemyManager
    {
        private EnemyZakoList _zakoList = new EnemyZakoList();
        private NotesManager ntMgr => gameEngine.notesManager;
        private GameEngine gameEngine => SingletonMonoBehaviour<GameEngine>.instance;
        private RandomList _randomList = new RandomList(4);

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
    }
}
