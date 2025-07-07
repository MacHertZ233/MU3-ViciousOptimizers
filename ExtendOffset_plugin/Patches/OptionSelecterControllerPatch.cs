using HarmonyLib;
using MU3;
using MU3.CustomUI;
using UnityEngine;

namespace ExtendOffset_plugin.Patches
{
    [HarmonyPatch(typeof(OptionSelecterController))]
    internal class OptionSelecterControllerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("setupCpFuncArray")]
        static void SetupCpFuncArrayPostfix(OptionSelecterController __instance)
        {
            OptionSelecterController.chengeParamFuncArray[] cpFuncArray = Traverse.Create(__instance).Field("cpFuncArray").GetValue<OptionSelecterController.chengeParamFuncArray[]>();
            cpFuncArray[2].max = 200;
            cpFuncArray[3].max = 200;
            //__instance.cpFuncArray = cpFuncArray;
        }

        [HarmonyPrefix]
        [HarmonyPatch("changeFieldJudge")]
        static bool ChangeFieldJudgePrefix(OptionSelecterController __instance, int currentParam, string abortObjPath)
        {
            string name = "NUL_SWH_Option_00/NUL_Select/NUM_Plus_DecimalPoint";
            GameObject gameObject = __instance.transform.Find(name).gameObject;
            gameObject.SetActive(true);
            string name2 = "NUL_SWH_Option_00/NUL_Select/NUM_Plus_DecimalPoint/Default_01";
            GameObject gameObject2 = __instance.transform.Find(name2).gameObject;
            if (currentParam == 100)
            {
                gameObject2.SetActive(true);
            }
            else
            {
                gameObject2.SetActive(false);
            }
            MU3UICounter component = gameObject.GetComponent<MU3UICounter>();
            component.Counter = -10.0 + 0.1 * (double)currentParam;
            MU3UIImageChanger component2 = __instance.transform.Find(abortObjPath).gameObject.GetComponent<MU3UIImageChanger>();
            int num;
            if (component.Counter < 0.0)
            {
                num = 0;
            }
            else if (component.Counter > 0.0)
            {
                num = 2;
            }
            else
            {
                num = 1;
            }
            component2.patternNumber = (float)num;

            return false;
        }
    }
}
