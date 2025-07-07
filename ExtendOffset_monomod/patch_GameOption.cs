using System;
using System.Collections.ObjectModel;
using System.Linq;
using MU3.User;

namespace MU3.Notes
{
    public class patch_GameOption : GameOption
    {
        public static ReadOnlyCollection<float> timingTbl = Array.AsReadOnly<float>((from i in Enumerable.Range(0, 201)
                                                                                     select -10f + (float)i * 0.1f).ToArray<float>());
        private static float calcTiming(UserOption.eTiming eTiming)
        {
            return timingTbl[(int)eTiming];
        }

        // Damn, I don't know how to patch the constructor in a static class... 
    }
}
