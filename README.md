# MU3ExtendOffset
A mod for the game codename MU3 which extends the adjustment range of offset A & B to ±10.0.

## Usage

1. Install [BepInEx](https://github.com/BepInEx/BepInEx) 5 and [BepInEx.MonoMod.Loader](https://github.com/BepInEx/BepInEx.MonoMod.Loader) properly. The `BepInEx` folder should be next to `mu3.exe`.

2. Download the latest release [here](https://github.com/MacHertZ233/MU3ExtendOffset/releases/latest).

3. Put `Assembly-CSharp.ExtendOffset.mm.dll` into `BepInEx\monomod\`.\
\
or\
\
***(only as a workaround!)***\
Replace `package\mu3_Data\Managed\Assembly-CSharp.dll` with `Assembly-CSharp-offset10.dll`.

## Known Issues

+ When offset A is adjusted to a larger value, the cue tick before the music starts will noticeably deviate from the music.
+ The mod is currently incompatible with MoreProfileOptions. A modded `Assembly-CSharp-offset10.dll` will be provided in release as a workaround.

## Build from Source

1. Put the following files into `ExtendOffset\lib\`. They can be retrieved from `package\mu3_Data\Managed\`
```
AMDaemon.NET.dll
Assembly-CSharp.dll
Assembly-CSharp-firstpass.dll
UnityEngine.dll
UnityEngine.UI.dll
```

2. Open the solution with MSVS.

## [How I Modded the Assembly](docs/Mod%20Guide.md)