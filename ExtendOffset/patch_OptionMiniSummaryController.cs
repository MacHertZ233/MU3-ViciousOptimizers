using MU3.CustomUI;
using MU3.User;
using UnityEngine;

namespace MU3
{
    public class patch_OptionMiniSummaryController : OptionMiniSummaryController
    {
        private GameObject[] arrayIcon;// = new GameObject[35];
        private extern void orig_setParam(UserOption.OptionName id, int value);

        private void setParam(UserOption.OptionName id, int value)
        {
            if (id == UserOption.OptionName.Timing || id == UserOption.OptionName.JudgeAdjustment)
            {
                this.arrayIcon[(int)id].transform.Find("NUL_Option_mini_Icon/NUM_DecimalPoint_1keta").GetComponent<MU3UICounter>().Counter = -10.0 + 0.1 * (double)value; // originally -2.0 + ...
                return;
            }
            else
            {
                orig_setParam(id, value);
            }
        }
    }
}
