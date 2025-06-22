# Mod Guide

They really fix a lot of values...\
And they really misspell things...

Hopefully I'm not missing anything.

## MU3.OptionSelecterController
Increase the element count of the offset settings, and change the counter formula.
```csharp
private void setupCpFuncArray()
{
    // ...
    this.cpFuncArray[2].func = new OptionSelecterController.chengeParamFunc(this.changeParamJudgeTiming);
    this.cpFuncArray[2].max = 200; // originally 40
    this.cpFuncArray[3].func = new OptionSelecterController.chengeParamFunc(this.changeParamJudgeAdjustment);
    this.cpFuncArray[3].max = 200; // originally 40
    // ...
}
```

```csharp
private void changeFieldJudge(int currentParam, string abortObjPath)
        {
            // ...
            if (currentParam == 100) // originally 20
            {
                gameObject2.SetActive(true);
            }
            else
            {
                gameObject2.SetActive(false);
            }
            MU3UICounter component = gameObject3.GetComponent<MU3UICounter>();
            component.Counter = -10.0 + 0.1 * (double)currentParam; // originally -2.0 + ...
            // ...
        }

```

## MU3.OptionMiniSummaryController
Change the counter formula.
```csharp
private void setParam(UserOption.OptionName id, int value)
{
    if (id != UserOption.OptionName.Speed)
    {
        // ...
        else
        {
            if (id == UserOption.OptionName.Timing || id == UserOption.OptionName.JudgeAdjustment)
            {
                this.arrayIcon[(int)id].transform.Find("NUL_Option_mini_Icon/NUM_DecimalPoint_1keta").GetComponent<MU3UICounter>().Counter = -10.0 + 0.1 * (double)value; // originally -2.0 + ...
                return;
            }
            // ...
        }
    }
    // ...
}
```

## MU3.Notes.GameOption
Replace the values of a read-only array. They are the actual values of the offset setting.\
Actually I don't know how to patch a static class, so currently the method I use in code is to create another extended `timingTbl` in `MU3.Notes.GameOption` instead.
```csharp
using System.Linq; // required to use Enumerable.Range()

public static class Const
{
    static Const()
    {
        // ...

        public static ReadOnlyCollection<float> timingTbl = Array.AsReadOnly<float>((from i in Enumerable.Range(0, 201) select -10f + (float)i * 0.1f).ToArray<float>());
        // originally from -2.0 to 2.0 with step 0.1

        // ...
    }
}
```

## MU3.User.UserOption
Replace an enum, and change min & max elements in some class members in DataSet.\
The game will get indexes of the read-only array from the enum. It's really freaking weird and I don't know why.
```csharp
// originally includes n20 to p20, with MAX = 40 and Default = 20
public enum eTiming
{
    n100, n99, n98, n97, n96, n95, n94, n93, n92, n91,
    // ...
    n10, n9, n8, n7, n6, n5, n4, n3, n2, n1,
    p0,
    p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,
    // ...
    p91, p92, p93, p94, p95, p96, p97, p98, p99, p100,
    MAX = 200, Default = 100
}
```

```csharp
public class DataSet
{
    public UserOption.eTiming Timing
	{
		get
		{
			return this.timing;
		}
		set
		{
			if (UserOption.eTiming.p100 < value) // originally p20, MAX is also ok
			{
				this.timing = UserOption.eTiming.p100;
				return;
			}
			if (value < UserOption.eTiming.n100) // originally n20
			{
				this.timing = UserOption.eTiming.n100;
				return;
			}
			this.timing = value;
		}
	}

    public bool isMax(UserOption.OptionName id)
    {
        bool result = false;
        switch (id)
        {
            // ...
            case UserOption.OptionName.Timing:
                if (this.timing == UserOption.eTiming.p100) // originally p20, MAX is also ok
                {
                    result = true;
                }
                break;
            case UserOption.OptionName.JudgeAdjustment:
                if (this.judgeAdjustment == UserOption.eTiming.p100)  // originally p20, MAX is also ok
                {
                    result = true;
                }
                break;
            // ...
        }
        return result;
    }
}
```