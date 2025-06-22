using MU3.CustomUI;
using UnityEngine;

namespace MU3
{
    public class patch_OptionSelecterController : OptionSelecterController
    {
        private OptionSelecterController.chengeParamFuncArray[] cpFuncArray; // = new chengeParamFuncArray[35];
        private extern void orig_setupCpFuncArray();

        private void setupCpFuncArray()
        {
            orig_setupCpFuncArray();
            cpFuncArray[2].max = 200;
            cpFuncArray[3].max = 200;
        }

        //private extern void orig_changeFieldJudge(int currentParam, string abortObjPath);   
        private new void changeFieldJudge(int currentParam, string abortObjPath)
        {
            string name = "NUL_SWH_Option_00/NUL_Select/NUM_Plus_DecimalPoint";
            GameObject gameObject = base.transform.Find(name).gameObject;
            gameObject.SetActive(true);
            string name2 = "NUL_SWH_Option_00/NUL_Select/NUM_Plus_DecimalPoint/Default_01";
            GameObject gameObject2 = base.transform.Find(name2).gameObject;
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
            MU3UIImageChanger component2 = base.transform.Find(abortObjPath).gameObject.GetComponent<MU3UIImageChanger>();
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
        }
    }
}
