using System.Reflection;
using HarmonyLib;
using MU3;
using UnityEngine;

namespace ExtendOffset_plugin.Patches
{
    [HarmonyPatch(typeof(OptionMiniSummaryController))]
    internal static class OptionMiniSummaryControllerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("setParam")]
        private static bool SetParamPrefix(
            object __instance,
            object[] __args)
        {
            string name = OffsetRuntime.OptionNameOf(__args[0]);
            if (!OffsetRuntime.IsTimingName(name))
                return true;

            int index = OffsetRuntime.GetOptionIndex(name);
            FieldInfo field = OffsetRuntime.GetField(
                __instance,
                "arrayIcon");
            if (field == null)
                return false;

            GameObject[] icons =
                field.GetValue(__instance) as GameObject[];
            if (icons == null || index < 0 || index >= icons.Length)
                return false;
            if (icons[index] == null)
                return false;

            Transform counterTransform = icons[index].transform.Find(
                "NUL_Option_mini_Icon/NUM_DecimalPoint_1keta");
            if (counterTransform == null)
                return false;

            Component counter = FindComponent(
                counterTransform.gameObject,
                "MU3.CustomUI.MU3UICounter");
            if (counter != null)
            {
                PropertyInfo property = counter.GetType().GetProperty(
                    "Counter",
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic);
                if (property != null)
                {
                    property.SetValue(
                        counter,
                        (double)OffsetRuntime.TimingForCode(
                            OffsetRuntime.ToInt(__args[1])),
                        null);
                }
            }
            return false;
        }

        private static Component FindComponent(
            GameObject gameObject,
            string fullName)
        {
            Component[] components = gameObject.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] != null &&
                    components[i].GetType().FullName == fullName)
                {
                    return components[i];
                }
            }
            return null;
        }
    }
}