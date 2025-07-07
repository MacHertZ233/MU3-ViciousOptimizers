# MU3ExtendOffset
[English](README.md)\
[简体中文](README_zh-hans.md)

A mod for the game codename MU3 which extends the adjustment range of offset A & B to ±10.0.

It contains an optional mod named *ShiftSECount* which makes the cue ticks before the music more aligned with the music rhythm (if the dev team doesn't screw up the audio alignment).

## Usage

1. Install [BepInEx](https://github.com/BepInEx/BepInEx) 5 and [BepInEx.MonoMod.Loader](https://github.com/BepInEx/BepInEx.MonoMod.Loader) (optional) properly. It is recommended to put the `BepInEx` folder next to `mu3.exe` for easier configuration.

2. Download the latest release [here](https://github.com/MacHertZ233/MU3ExtendOffset/releases/latest).

3. There are 3 ways to install *ExtendOffset*:
    + **Plugin Method**: Put `ExtendOffset_plugin.dll` into `BepInEx\plugins` folder.
    + **MonoMod Method**: Put `Assembly-CSharp.ExtendOffset.mm.dll` into `BepInEx\monomod` folder. BepInEx.MonoMod.Loader is needed for this method.
    + **Assembly Replacement Method**: Replace `package\mu3_Data\Managed\Assembly-CSharp.dll` with `Assembly-CSharp-offset10.dll`. BEWARE: Use this method if and only if neither of the above methods is feasible.

4. If you need a potentially better rhythm gaming experience, there are 2 ways to install *ShiftSECount*:
    + **Plugin Method**: Put `ShiftSECount_plugin.dll` into `BepInEx\plugins` folder.
    + **MonoMod Method**: Put `Assembly-CSharp.ShiftSECount.mm.dll` into `BepInEx\monomod` folder. BepInEx.MonoMod.Loader is needed for this method.

## Supported Game Version

+ 1.50

## Known Issues

+ The MonoMod form of *ExtendOffset* is currently incompatible with *MoreProfileOptions*.
    + The Plugin form released lately is expected to address this issue, but still lacks more testing.
    + A modded `Assembly-CSharp-offset10.dll` will be provided as a final workaround. 

+ When offset A is adjusted to a larger value, the cue ticks before the music starts will noticeably deviate from the music.
    + Using *ShiftSECount* will mitigate the situation in most cases. But we don't know how well the devs are doing with the audio alignment.

## Build from Source

1. Put the following files into `.\lib` folder. They can be retrieved from `<MU3_game_path>\package\mu3_Data\Managed` and `BepInEx\core`. 
```
AMDaemon.NET.dll
Assembly-CSharp.dll
Assembly-CSharp-firstpass.dll
UnityEngine.dll
UnityEngine.UI.dll

BepInEx.dll
0Harmony.dll
```

2. Open the solution with Visual Studio (preferably VS2022) and build all projects.