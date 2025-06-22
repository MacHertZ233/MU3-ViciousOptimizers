using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MU3.Notes
{
    public class patch_GameOption : GameOption
    {
        public static new ReadOnlyCollection<float> timingTbl = Array.AsReadOnly<float>((from i in Enumerable.Range(0, 201) 
                                                                                         select -10f + (float)i * 0.1f).ToArray<float>());
    }
}
