# MU3ExtendOffset
[English](README.md)\
[简体中文](README_zh-hans.md)

一个代号MU3游戏的mod。可以把A/B延迟的可调整范围扩大为±10.0。

同时包含一个可选的mod——*ShiftSECount*，用来消除音乐播放前的提示音与音乐的节奏之间的时间差距（如果开发团队没把对音搞砸的话）。

## 用法

1. 为MU3游戏正确安装 [BepInEx](https://github.com/BepInEx/BepInEx) 5 和 [BepInEx.MonoMod.Loader](https://github.com/BepInEx/BepInEx.MonoMod.Loader)（可选）。为方便配置，建议将名为`BepInEx`的文件夹与`mu3.exe`放在同一路径下。

2. 从[这里](https://github.com/MacHertZ233/MU3ExtendOffset/releases/latest)下载最新版本的mod。

3. 有3种方法可以安装*ExtendOffset*：
    + **插件方法**：把`ExtendOffset_plugin.dll`放入`BepInEx\plugins`文件夹中
    + **MonoMod方法**：把`Assembly-CSharp.ExtendOffset.mm.dll`放入`BepInEx\monomod`文件夹中。这种方法需要您安装BepInEx.MonoMod.Loader。
    + **程序集（Assembly）替换方法**：把`package\mu3_Data\Managed\Assembly-CSharp.dll`用`Assembly-CSharp-offset10.dll`覆盖掉。注意注意！务必在当且仅当以上两种方法都不可行时再使用此方法。

4. 如果您需要潜在的更好的节奏游戏体验，那么有2种方法可以安装 *ShiftSECount*：
    + **插件方法**：把`ShiftSECount_plugin.dll`放入`BepInEx\plugins`文件夹中。
    + **MonoMod方法**：把`Assembly-CSharp.ShiftSECount.mm.dll`放入`BepInEx\monomod`文件夹中。这种方法需要您安装BepInEx.MonoMod.Loader。

## 支持的游戏版本

+ 1.50

## 已知问题

+ MonoMod版本的ExtendOffset目前与MoreProfileOptions不兼容。
    + 最新发布的插件版本旨在解决这一问题，但仍欠缺更多测试。
    + 作为最终替代方案，将会提供一个修改过后的`Assembly-CSharp-offset10.dll` 。

+ 当A延迟调整到较大值时，音乐开始前的提示音会明显偏离音乐节奏。
    + 使用*ShiftSECount*可以在大部分情况下缓解该情况。但天知道开发组的对音工作行不行。

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