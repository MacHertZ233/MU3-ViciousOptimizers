using UnityEngine;

namespace MU3.Battle
{
    public class patch_CharacterView : CharacterView
    {
        public patch_CharacterView(InitParam initParam, GameObject gameObject) : base(initParam, gameObject) { }

        protected extern GameObject orig_createEffect(AssetAssign.NoteEffect.Type eff, bool isSetPos = true);
        protected new GameObject createEffect(AssetAssign.NoteEffect.Type eff, bool isSetPos = true)
        {
            if ((eff == AssetAssign.NoteEffect.Type.explosionZako) && (patch_BattleUI.config & 64) == 0)
            {
                return null;
            }
            return orig_createEffect(eff, isSetPos);
        }
    }
}
