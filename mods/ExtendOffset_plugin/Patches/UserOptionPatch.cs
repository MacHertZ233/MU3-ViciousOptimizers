using HarmonyLib;
using MU3.User;

namespace ExtendOffset_plugin.Patches
{
    [HarmonyPatch(typeof(UserOption.DataSet))]
    internal static class UserOptionPatch
    {
        private static string FieldName(string optionName)
        {
            return optionName == "Timing"
                ? "timing"
                : "judgeAdjustment";
        }

        [HarmonyPrefix]
        [HarmonyPatch("set_Timing")]
        private static bool SetTimingPrefix(
            object __instance,
            object[] __args)
        {
            OffsetRuntime.SetCode(
                __instance,
                "timing",
                OffsetRuntime.ToInt(__args[0]));
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("set_JudgeAdjustment")]
        private static bool SetJudgeAdjustmentPrefix(
            object __instance,
            object[] __args)
        {
            OffsetRuntime.SetCode(
                __instance,
                "judgeAdjustment",
                OffsetRuntime.ToInt(__args[0]));
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("getData")]
        private static bool GetDataPrefix(
            object __instance,
            object[] __args,
            ref int __result)
        {
            string name = OffsetRuntime.OptionNameOf(__args[0]);
            if (!OffsetRuntime.IsTimingName(name))
                return true;

            __result = OffsetRuntime.CodeToUi(
                OffsetRuntime.GetCode(
                    __instance,
                    FieldName(name)));
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("setData")]
        private static bool SetDataPrefix(
            object __instance,
            object[] __args)
        {
            string name = OffsetRuntime.OptionNameOf(__args[0]);
            if (!OffsetRuntime.IsTimingName(name))
                return true;

            int ui = OffsetRuntime.ToInt(__args[1]);
            OffsetRuntime.SetCode(
                __instance,
                FieldName(name),
                OffsetRuntime.UiToCode(ui));
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("isMax")]
        private static bool IsMaxPrefix(
            object __instance,
            object[] __args,
            ref bool __result)
        {
            string name = OffsetRuntime.OptionNameOf(__args[0]);
            if (!OffsetRuntime.IsTimingName(name))
                return true;

            int code = OffsetRuntime.GetCode(
                __instance,
                FieldName(name));

            __result = code == OffsetRuntime.MaxPositiveCode;
            return false;
        }
    }
}