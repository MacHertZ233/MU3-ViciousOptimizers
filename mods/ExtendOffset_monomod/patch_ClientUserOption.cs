using ExtendOffset_monomod;

namespace MU3.Client
{
    public class patch_UserOption : UserOption
    {
        public extern void orig_copyTo(MU3.User.UserOption option);

        public new void copyTo(MU3.User.UserOption option)
        {
            orig_copyTo(option);
            OffsetRuntime.CopyRawClientValues(this, option);
        }
    }
}
