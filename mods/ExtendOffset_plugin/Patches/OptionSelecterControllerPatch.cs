using System;
using System.Reflection;
using HarmonyLib;
using MU3;
using UnityEngine;

namespace ExtendOffset_plugin.Patches
{
    [HarmonyPatch(typeof(OptionSelecterController))]
    internal static class OptionSelecterControllerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("setupCpFuncArray")]
        private static void SetupCpFuncArrayPostfix(object __instance)
        {
            FieldInfo field = OffsetRuntime.GetField(
                __instance,
                "cpFuncArray");
            if (field == null)
                return;

            Array array = field.GetValue(__instance) as Array;
            if (array == null)
                return;

            SetMax(
                array,
                OffsetRuntime.GetOptionIndex("Timing"),
                OffsetRuntime.MaxUiIndex);
            SetMax(
                array,
                OffsetRuntime.GetOptionIndex("JudgeAdjustment"),
                OffsetRuntime.MaxUiIndex);
        }

        private static void SetMax(Array array, int index, int max)
        {
            if (index < 0 || index >= array.Length)
                return;

            object boxed = array.GetValue(index);
            if (boxed == null)
                return;

            FieldInfo maxField = boxed.GetType().GetField(
                "max",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);
            if (maxField == null)
                return;

            maxField.SetValue(boxed, max);
            array.SetValue(boxed, index);
        }

        [HarmonyPrefix]
        [HarmonyPatch("changeFieldJudge")]
        private static bool ChangeFieldJudgePrefix(
            object __instance,
            object[] __args)
        {
            int currentParam = OffsetRuntime.ToInt(__args[0]);
            object abortArg = __args.Length > 1 ? __args[1] : null;
            UpdateJudgeDisplay(
                (Component)__instance,
                currentParam,
                abortArg);
            return false;
        }

        private static void UpdateJudgeDisplay(
            Component instance,
            int currentParam,
            object abortArg)
        {
            Transform transform = instance.transform;
            GameObject counterObject = Find(
                transform,
                "NUL_SWH_Option_00/NUL_Select/NUM_Plus_DecimalPoint");
            if (counterObject == null)
                return;

            counterObject.SetActive(true);

            GameObject zeroObject = Find(
                transform,
                "NUL_SWH_Option_00/NUL_Select/NUM_Plus_DecimalPoint/Default_01");
            if (zeroObject != null)
            {
                zeroObject.SetActive(
                    currentParam == OffsetRuntime.ZeroUiIndex);
            }

            float value = OffsetRuntime.TimingForUi(currentParam);
            Component counter = FindComponent(
                counterObject,
                "MU3.CustomUI.MU3UICounter");
            SetProperty(counter, "Counter", value);

            GameObject abortObject = abortArg as GameObject;
            if (abortObject == null && abortArg is string)
                abortObject = Find(transform, (string)abortArg);
            if (abortObject == null)
                return;

            Component changer = FindComponent(
                abortObject,
                "MU3.CustomUI.MU3UIImageChanger");
            SetProperty(
                changer,
                "patternNumber",
                value < 0f ? (object)0f :
                (value > 0f ? (object)2f : (object)1f));
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

        private static void SetProperty(
            Component component,
            string name,
            object value)
        {
            if (component == null)
                return;

            PropertyInfo property = component.GetType().GetProperty(
                name,
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);
            if (property == null)
                return;

            if (value is float && property.PropertyType == typeof(double))
                value = (double)(float)value;

            try
            {
                property.SetValue(component, value, null);
            }
            catch
            {
                // Ignore exceptions when the UI prefab lacks the property.
            }
        }

        private static GameObject Find(Transform root, string path)
        {
            if (root == null || string.IsNullOrEmpty(path))
                return null;

            Transform child = root.Find(path);
            return child == null ? null : child.gameObject;
        }
    }
}