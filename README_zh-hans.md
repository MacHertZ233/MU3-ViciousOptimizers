# MU3-ViciousOptimizers
[English](README.md)\
[简体中文](README_zh-hans.md)

一系列代号MU3游戏的mod，专为最大化节奏游戏体验而制作。

够邪恶，也够有用。

目前包含了以下mod：
+ **ExtendOffset**：将A和B延迟的可调整范围从±2.0扩大至±10.0。
+ **ShiftSECount**：消除音乐播放前的提示音与音乐的节奏之间的时间差距（如果开发团队没把对音搞砸的话）。
+ **MuteEnemySound**：消除敌人的人声和提示击败敌人的音效，以使音乐再也不会被它们盖过。
    + **MuteEnemySoundLite**: 仅消除敌人的人声。此mod是为想通过音频了解战斗进展的人而制作。
+ **HideStats**：在战斗中，隐藏左侧的玩家面板和右侧的敌人面板。同时也会隐藏伤害数字。

## 依赖
+ [BepInEx](https://github.com/BepInEx/BepInEx) 5
+ [BepInEx.MonoMod.Loader](https://github.com/BepInEx/BepInEx.MonoMod.Loader)（可选，使用MonoMod方法时需安装）

## 安装方式
每个mod的安装均有两种方式，选择其一即可。
+ **插件方法**：把`<mod名称>_plugin.dll`放入`BepInEx\plugins`文件夹中。
+ **MonoMod方法**：把`Assembly-CSharp.<mod名称>.mm.dll`放入`BepInEx\monomod`文件夹中。

对于ExtendOffset，还有一种最终方案，即是把`package\mu3_Data\Managed\Assembly-CSharp.dll`用`Assembly-CSharp-offset10.dll`覆盖掉。注意注意！务必在当且仅当以上两种方法都不可行时再使用此方法。

## 支持的游戏版本

+ 1.50

## 已知问题

+ MonoMod版本的ExtendOffset目前与[MoreProfileOptions](https://www.rainycolor.org/package/7EVENDAYSHOLIDAYS/MoreProfileOptions/)不兼容。
    + 最新发布的插件版本旨在解决这一问题，但仍欠缺更多测试。
    + 作为最终替代方案，将会提供一个修改过后的`Assembly-CSharp-offset10.dll` 。

+ 当使用ExtendOffset，并将A延迟调整到较大的数值时（无论正负），音乐开始前的提示音会明显偏离音乐节奏。
    + 使用ShiftSECount可以在大部分情况下缓解该情况。但天知道开发组的对音工作行不行。

+ HideStats不会隐藏战斗中止（BATTLE RETIRE）信息。

## 从源代码构建

1. 把以下的文件放入`.\lib`文件夹中。它们都可以从`<MU3游戏路径>\package\mu3_Data\Managed`和`BepInEx\core`中获取。
```
AMDaemon.NET.dll
Assembly-CSharp.dll
Assembly-CSharp-firstpass.dll
UnityEngine.dll
UnityEngine.UI.dll

BepInEx.dll
0Harmony.dll
```

2. 用Visual Studio（最好是VS2022）打开解决方案并构建所有项目。

## 致谢

+ [mu3-mods](https://gitea.tendokyu.moe/akanyan/mu3-mods)——为我制作mod提供了灵感和参考