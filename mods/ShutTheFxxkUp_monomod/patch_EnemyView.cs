using MU3.Sound;
using MU3.Util;
using UnityEngine;

namespace MU3.Battle
{
    public class patch_EnemyView : EnemyView
    {
        public patch_EnemyView(InitParam initParam, GameObject gameObject) : base(initParam, gameObject)
        {
            _initParam = initParam;
        }

        public new void down()
        {
            damage();
            createEffect(AssetAssign.NoteEffect.Type.explosionBoss);
            if (patch_BattleUI.playSE)
                Singleton<SoundManager>.instance.play(19);
        }
    }
}
