using ExtendOffset_monomod;

namespace MU3.User
{
    public class patch_UserOption : UserOption
    {
        public class patch_DataSet : DataSet
        {
            private static string FieldName(string optionName)
            {
                return optionName == "Timing"
                    ? "timing"
                    : "judgeAdjustment";
            }

            public extern int orig_getData(OptionName id);

            public new int getData(OptionName id)
            {
                string name = id.ToString();
                if (name == "Timing" || name == "JudgeAdjustment")
                {
                    return OffsetRuntime.CodeToUi(
                        OffsetRuntime.GetCode(
                            this,
                            FieldName(name)));
                }

                return orig_getData(id);
            }

            public extern void orig_setData(OptionName id, int num);

            public new void setData(OptionName id, int num)
            {
                string name = id.ToString();
                if (name == "Timing" || name == "JudgeAdjustment")
                {
                    OffsetRuntime.SetCode(
                        this,
                        FieldName(name),
                        OffsetRuntime.UiToCode(num));
                    return;
                }

                orig_setData(id, num);
            }

            public extern bool orig_isMax(OptionName id);

            public new bool isMax(OptionName id)
            {
                string name = id.ToString();
                if (name == "Timing" || name == "JudgeAdjustment")
                {
                    return OffsetRuntime.GetCode(
                        this,
                        FieldName(name)) ==
                        OffsetRuntime.MaxPositiveCode;
                }

                return orig_isMax(id);
            }
        }
    }
}
