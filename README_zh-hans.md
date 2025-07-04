# MU3ExtendOffset
[English](README.md)\
[简体中文](README_zh-hans.md)

一个代号MU3游戏的mod。可以把A/B延迟的可调整范围扩大为±10.0。

同时包含一个可选的mod——ShiftSECount，用来消除音乐播放前的提示音与音乐的节奏之间的时间差距（如果开发团队没把对音搞砸的话）。

## 用法

1. 为MU3游戏正确安装 [BepInEx](https://github.com/BepInEx/BepInEx) 5 和 [BepInEx.MonoMod.Loader](https://github.com/BepInEx/BepInEx.MonoMod.Loader)。为方便配置，建议将名为`BepInEx`的文件夹与`mu3.exe`放在同一路径下。

2. 从[这里](https://github.com/MacHertZ233/MU3ExtendOffset/releases/latest)下载最新版本的mod。

3. 把`Assembly-CSharp.ExtendOffset.mm.dll`放入`BepInEx\monomod`文件夹中\
\
或者\
\
***（仅作为替代方案！）***\
把`package\mu3_Data\Managed\Assembly-CSharp.dll`用`Assembly-CSharp-offset10.dll`覆盖掉。

4. （可选）把`Assembly-CSharp.ShiftSECount.mm.dll`放入`BepInEx\monomod`文件夹中。

## 支持的游戏版本

+ 1.50

## 已知问题

+ ExtendOffset目前与MoreProfileOptions不兼容。作为替代方案，将在发布版本中提供一个修改过后的 `Assembly-CSharp-offset10.dll` 。

+ 【已解决】 ~~当A延迟调整到较大值时，音乐开始前的提示音会明显偏离音乐节奏。~~

## 从源代码构建

1. 把以下的文件放入`.\lib`文件夹中。它们都可以从`<MU3游戏路径>\package\mu3_Data\Managed`中获取。
```
AMDaemon.NET.dll
Assembly-CSharp.dll
Assembly-CSharp-firstpass.dll
UnityEngine.dll
UnityEngine.UI.dll
```

2. 用Visual Studio（最好是VS2022）打开解决方案并构建所有项目。