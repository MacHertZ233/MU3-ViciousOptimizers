# MU3ExtendOffset
[English](README.md)\
[简体中文](README_zh-hans.md)

A mod for the game codename MU3 which extends the adjustment range of offset A & B to ±10.0.

It contains an optional mod named ShiftSECount which makes the cue ticks before the music more aligned with the music rhythm (if the dev team doesn't screw up the audio alignment).

## Usage

1. Install [BepInEx](https://github.com/BepInEx/BepInEx) 5 and [BepInEx.MonoMod.Loader](https://github.com/BepInEx/BepInEx.MonoMod.Loader) properly. It is recommended to put the `BepInEx` folder next to `mu3.exe` for easier configuration.

2. Download the latest release [here](https://github.com/MacHertZ233/MU3ExtendOffset/releases/latest).

3. Put `Assembly-CSharp.ExtendOffset.mm.dll` into `BepInEx\monomod` folder.\
\
or\
\
***(only as a workaround!)***\
Replace `package\mu3_Data\Managed\Assembly-CSharp.dll` with `Assembly-CSharp-offset10.dll`.

4. (optional) Put `Assembly-CSharp.ShiftSECount.mm.dll` into `BepInEx\monomod` folder.

## Supported Game Version

+ 1.50

## Known Issues

+ The mod (ExtendOffset) is currently incompatible with MoreProfileOptions. A modded `Assembly-CSharp-offset10.dll` will be provided in release as a workaround.

+ [Solved] ~~When offset A is adjusted to a larger value, the cue ticks before the music starts will noticeably deviate from the music.~~

## Build from Source

1. Put the following files into `.\lib` folder. They can be retrieved from `<MU3_game_path>\package\mu3_Data\Managed`.
```
AMDaemon.NET.dll
Assembly-CSharp.dll
Assembly-CSharp-firstpass.dll
UnityEngine.dll
UnityEngine.UI.dll
```

2. Open the solution with Visual Studio (preferably VS2022) and build all projects.