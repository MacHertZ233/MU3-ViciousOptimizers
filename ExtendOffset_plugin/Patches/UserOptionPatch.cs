using HarmonyLib;
using MU3.User;
using static MU3.User.UserOption;

namespace ExtendOffset_plugin.Patches
{
    [HarmonyPatch(typeof(UserOption))]
    internal class UserOptionPatch
    {
        public new enum eTimingPatched
        {
            n100, n99, n98, n97, n96, n95, n94, n93, n92, n91,
            n90, n89, n88, n87, n86, n85, n84, n83, n82, n81,
            n80, n79, n78, n77, n76, n75, n74, n73, n72, n71,
            n70, n69, n68, n67, n66, n65, n64, n63, n62, n61,
            n60, n59, n58, n57, n56, n55, n54, n53, n52, n51,
            n50, n49, n48, n47, n46, n45, n44, n43, n42, n41,
            n40, n39, n38, n37, n36, n35, n34, n33, n32, n31,
            n30, n29, n28, n27, n26, n25, n24, n23, n22, n21,
            n20, n19, n18, n17, n16, n15, n14, n13, n12, n11,
            n10, n9, n8, n7, n6, n5, n4, n3, n2, n1,
            p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,
            p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
            p21, p22, p23, p24, p25, p26, p27, p28, p29, p30,
            p31, p32, p33, p34, p35, p36, p37, p38, p39, p40,
            p41, p42, p43, p44, p45, p46, p47, p48, p49, p50,
            p51, p52, p53, p54, p55, p56, p57, p58, p59, p60,
            p61, p62, p63, p64, p65, p66, p67, p68, p69, p70,
            p71, p72, p73, p74, p75, p76, p77, p78, p79, p80,
            p81, p82, p83, p84, p85, p86, p87, p88, p89, p90,
            p91, p92, p93, p94, p95, p96, p97, p98, p99, p100,
            MAX = 200, Default = 100
        }

        
        [HarmonyPatch(typeof(DataSet))]
        internal class DataSetPatch
        {
            [HarmonyPrefix]
            [HarmonyPatch("Timing", MethodType.Setter)]
            static bool TimingSetterPrefix(ref UserOption.eTiming value, ref DataSet __instance)
            {
                if ((int)value > (int)eTimingPatched.p100)
                {
                    value = (UserOption.eTiming)(int)eTimingPatched.p100;
                }
                else if ((int)value < (int)eTimingPatched.n100)
                {
                    value = (UserOption.eTiming)(int)eTimingPatched.n100;
                }
                Traverse.Create(__instance).Field("timing").SetValue(value);
                return false;
            }

            [HarmonyPrefix]
            [HarmonyPatch("JudgeAdjustment", MethodType.Setter)]
            static bool JudgeAdjustmentSetterPrefix(ref UserOption.eTiming value, ref DataSet __instance)
            {
                if ((int)value > (int)eTimingPatched.p100)
                {
                    value = (UserOption.eTiming)(int)eTimingPatched.p100;
                }
                else if ((int)value < (int)eTimingPatched.n100)
                {
                    value = (UserOption.eTiming)(int)eTimingPatched.n100;
                }
                Traverse.Create(__instance).Field("judgeAdjustment").SetValue(value);
                return false;
            }

            [HarmonyPrefix]
            [HarmonyPatch("isMax")]
            static bool IsMaxSetterPrefix(ref UserOption.OptionName id, ref bool __result, ref DataSet __instance)
            {
                if (id == OptionName.JudgeAdjustment || id == OptionName.Timing)
                {
                    __result = Traverse.Create(__instance).Field("judgeAdjustment").GetValue<int>() == (int)eTimingPatched.p100;
                    return false;
                }
                return true;
            }
        }

    }
}
