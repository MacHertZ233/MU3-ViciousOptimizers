using System;
using System.Reflection;
using ExtendOffset_monomod;
using MU3;
using UnityEngine;

namespace MU3
{
    public class patch_OptionSelecterController : OptionSelecterController
    {
        private OptionSelecterController.chengeParamFuncArray[] cpFuncArray;

        private extern void orig_setupCpFuncArray();

        private void setupCpFuncArray()
        {
            orig_setupCpFuncArray();

            if (cpFuncArray == null)
                return;

            int timing = OffsetRuntime.GetOptionIndex("Timing");
            int judge = OffsetRuntime.GetOptionIndex("JudgeAdjustment");
            if (timing >= 0 && timing < cpFuncArray.Length)
                cpFuncArray[timing].max = OffsetRuntime.MaxUiIndex;
            if (judge >= 0 && judge < cpFuncArray.Length)
                cpFuncArray[judge].max = OffsetRuntime.MaxUiIndex;
        }

        private void changeParamJudgeTiming(int currentParam)
        {
            UpdateJudgeDisplay(
                currentParam,
                FindPreview("PAT_TimingA", "TimingA"));
        }

        private void changeParamJudgeAdjustment(int currentParam)
        {
            UpdateJudgeDisplay(
                currentParam,
                FindPreview("PAT_TimingB", "TimingB"));
        }

        private void UpdateJudgeDisplay(
            int currentParam,
            GameObject abortObject)
        {
            GameObject counterObject = Find(
                "NUL_SWH_Option_00/NUL_Select/NUM_Plus_DecimalPoint");
            if (counterObject == null)
                return;

            counterObject.SetActive(true);

            GameObject zeroObject = Find(
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
            }
        }

        private GameObject Find(string path)
        {
            Transform child = base.transform.Find(path);
            return child == null ? null : child.gameObject;
        }

        private GameObject FindPreview(
            string path,
            string previewTypeName)
        {
            GameObject byPath = Find(
                "NUL_SWH_Option_00/ANM_Option_Preview/NUL_Option_Preview/" +
                path);
            if (byPath != null)
                return byPath;
            try
            {
                PropertyInfo property = GetType().GetProperty(
                    "AnmOptionPreview",
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic);

                object preview = property == null
                    ? null
                    : property.GetValue(this, null);
                if (preview == null)
                    return null;

                Type enumType = preview.GetType().Assembly.GetType(
                    "MU3.ANM_Option_Preview+OptionPreviewType");
                if (enumType == null)
                    return null;

                object enumValue = Enum.Parse(enumType, previewTypeName);
                MethodInfo method = preview.GetType().GetMethod(
                    "FindGameObject",
                    new Type[] { enumType });
                if (method == null)
                    return null;

                return method.Invoke(
                    preview,
                    new object[] { enumValue }) as GameObject;
            }
            catch
            {
                return null;
            }
        }
    }
}
