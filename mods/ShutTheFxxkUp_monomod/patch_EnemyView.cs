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
            // Singleton<SoundManager>.instance.play(19);
        }
    }
}
