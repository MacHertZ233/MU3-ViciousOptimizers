using HarmonyLib;
using MU3;
using MU3.Battle;
using MU3.DataStudio;
using MU3.DB;
using MU3.Notes;
using MU3.Sound;
using MU3.Util;
using System.Collections.Generic;
using UnityEngine;
using static MU3.Battle.EnemyManager;

namespace ShutTheFxxkUp_plugin.Patches
{
    [HarmonyPatch(typeof(EnemyManager))]
    public class EnemyManagerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("update")]
        static bool UpdatePrefix(EnemyManager __instance, ref float frameCur, MU3.Data.MusicData musicData,
            ref bool  ____enable, ref EnemyBoss  ____boss, ref float  ____frameCur, ref float  ____frameState, ref int  ____wave, ref EnemyZakoList  ____zakoList,
            ref int ____stateLastID, ref int ____stateID)
        {
            if (! ____enable)
            {
                return false;
            }

            NotesManager ntMgr = Traverse.Create(__instance).Property<NotesManager>("ntMgr").Value;
            GameEngine gameEngine = Traverse.Create(__instance).Property<GameEngine>("gameEngine").Value;
            
             ____frameState += frameCur -  ____frameCur;
             ____frameCur = frameCur;
            int curWave = ntMgr.curWave;
            if ( ____wave != curWave)
            {
                NotesManager.WaveDetailData waveDetailData = null;
                if ( ____wave >= 0)
                {
                    waveDetailData = ntMgr.waveDetailDataList[ ____wave];
                }
                NotesManager.WaveDetailData waveDetailData2 = ntMgr.waveDetailDataList[Mathf.Max(curWave, 0)];
                if ( ____wave >= 0)
                {
                    foreach (EnemyZako zako in  ____zakoList)
                    {
                        if (zako.param.wave ==  ____wave)
                        {
                            zako.run();
                        }
                    }
                }
                bool flag = curWave == __instance.waveBossID;
                if (flag)
                {
                     ____boss.forward();
                }
                else
                {
                    Traverse.Create(__instance).Method("makeZakoSet", curWave, 0).GetValue();
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
                    if(BattleUIPatch.playSE)
                        Singleton<SoundManager>.instance.play(45);
                    if (flag)
                    {
                        if (BattleUIPatch.playSE)
                            Singleton<SoundManager>.instance.play(54);
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
                 ____wave = curWave;
            }
            else if ( ____wave >= 0)
            {
                foreach (EnemyZako zako2 in  ____zakoList)
                {
                    if (zako2.getCurrentState() == EnemyZako.EState.Play && zako2.param.wave <  ____wave)
                    {
                        zako2.run();
                    }
                }
            }
            if (____stateLastID >= 0)
            {
                ____stateID = ____stateLastID + 1;
                ____stateLastID = -1;
            }
             ____boss.update(frameCur);
            for (int num =  ____zakoList.Count - 1; num >= 0; num--)
            {
                EnemyZako enemyZako =  ____zakoList[num];
                enemyZako.update(frameCur);
                if (enemyZako.isStateEnd() || enemyZako.getCurrentState() == EnemyZako.EState.Finish)
                {
                    enemyZako.destroy();
                     ____zakoList.RemoveAt(num);
                }
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("makeZakoSet")]
        static bool MakeZakoSetPrefix(EnemyManager __instance, int wave, int set,
            ref RandomList ____randomList, ref EnemyZakoList ____zakoList)
        {
            GameEngine gameEngine = Traverse.Create(__instance).Property<GameEngine>("gameEngine").Value;
            NotesManager.WaveDetailData wDD = Traverse.Create(__instance).Method("getWDD").GetValue<NotesManager.WaveDetailData>();

            if (wDD != null)
            {
                ____randomList.shuffle();
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
                    param.arrayNum = ____randomList[i];
                    param.playerTransform = gameEngine.player.transform;
                    enemyZako.initialize(param);
                    ____zakoList.Add(enemyZako);
                }
                if (wave != 0 || set != 0)
                {
                    //Singleton<SoundManager>.instance.play(69);
                }
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("playBossSound")]
        static bool PlayBossSoundPrefix(EnemyManager __instance, int soundId)
        {
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("makeReward")]
        static bool MakeRewardPrefix(EnemyManager __instance, int wave, int set)
        {
            GameEngine gameEngine = Traverse.Create(__instance).Property<GameEngine>("gameEngine").Value;

            BattleReward battleReward = gameEngine.battleReward;
            List<DestroyReward> list = null;
            BattlePhase battlePhase = ((wave ==  __instance.waveBossID) ? BattlePhase.Boss : BattlePhase.Zako);
            int waveCount = ((wave != __instance.waveBossID) ? wave : 0);
            list = battleReward.pickDestroyReward(battlePhase, waveCount, set);
            battleReward.getReward(list);
            if (BattleUIPatch.playSE)
                Singleton<SoundManager>.instance.play(92);
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
            if (flag && BattleUIPatch.playSE)
            {
                Singleton<SoundManager>.instance.play(71);
            }
            if (flag2 && BattleUIPatch.playSE)
            {
                Singleton<SoundManager>.instance.play(23);
            }

            return false;
        }
       }
}
