using System;
using System.Reflection;
using MonoMod.RuntimeDetour;
using MU3;

namespace CleanBattlefield_monomod
{
    public static class detour_ANM_PLY_Cardinfo_00
    {
        private static bool show = false;

        private static Hook stateHook;
        private static Hook showHook;

        public static void Install()
        {
            int config = patch_BattleUI.config;
            show = (config & 8) != 0;

            MethodInfo stateSetter = typeof(ANM_PLY_Cardinfo_00).GetProperty("state").GetSetMethod(true);
            stateHook = new Hook(stateSetter, typeof(detour_ANM_PLY_Cardinfo_00).GetMethod(nameof(StateSetterDetour), BindingFlags.Static | BindingFlags.NonPublic));

            MethodInfo showSetter = typeof(ANM_PLY_Cardinfo_00).GetProperty("Show").GetSetMethod(true);
            showHook = new Hook(showSetter, typeof(detour_ANM_PLY_Cardinfo_00).GetMethod(nameof(ShowSetterDetour), BindingFlags.Static | BindingFlags.NonPublic));
        }

        private static void StateSetterDetour(Action<ANM_PLY_Cardinfo_00, ANM_PLY_Cardinfo_00.State> orig, ANM_PLY_Cardinfo_00 self, ANM_PLY_Cardinfo_00.State value)
        {
            orig(self, value);

            if (self != null && self.gameObject != null)
            {
                self.gameObject.SetActive(show);
            }
        }

        private static void ShowSetterDetour(Action<ANM_PLY_Cardinfo_00, bool> orig, ANM_PLY_Cardinfo_00 self, bool value)
        {
            orig(self, value);

            if (self != null && self.gameObject != null)
            {
                self.gameObject.SetActive(show);
            }

        }
    }
}

