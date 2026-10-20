using System.Reflection;
using ExtendOffset_monomod;
using MU3;
using MU3.User;
using UnityEngine;

namespace MU3
{
    public class patch_OptionMiniSummaryController
        : OptionMiniSummaryController
    {
        private GameObject[] arrayIcon;

        private extern void orig_setParam(
            UserOption.OptionName id,
            int value);

        private void setParam(UserOption.OptionName id, int value)
        {
            string name = id.ToString();
            if (name != "Timing" && name != "JudgeAdjustment")
            {
                orig_setParam(id, value);
                return;
            }

            int index = (int)id;
            if (arrayIcon == null ||
                index < 0 ||
                index >= arrayIcon.Length)
                return;
            if (arrayIcon[index] == null)
                return;

            Transform counterTransform = arrayIcon[index].transform.Find(
                "NUL_Option_mini_Icon/NUM_DecimalPoint_1keta");
            if (counterTransform == null)
                return;

            Component counter = FindComponent(
                counterTransform.gameObject,
                "MU3.CustomUI.MU3UICounter");
            if (counter == null)
                return;

            PropertyInfo property = counter.GetType().GetProperty(
                "Counter",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);

            if (property != null)
            {
                property.SetValue(
                    counter,
                    (double)OffsetRuntime.TimingForCode(value),
                    null);
            }
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
