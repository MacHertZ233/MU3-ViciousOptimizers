# MU3-ViciousOptimizers

[English](README.md) \\
[简体中文](README_zh-hans.md)

A collection of mods for the game codename MU3, made purely to maximize the rhythm gaming experience.

Evil enough, and useful enough.

So far, the collection includes:

+ ***ExtendOffset***: Extends the adjustment range of offset A & B from ±2.0 to ±10.0.
+ ***ShiftSECount***: Makes the cue ticks before the music more aligned with the music rhythm (if the dev team doesn't screw up the audio alignment).
+ ***ShutTheFxxkUp***: Eliminates enemies' vocals as well as sound effects that prompt defeat of the enemies, so that the music is never obscured.
+ ***CleanBattlefield***: Optionally hides several elements of the battle interface. See [here](doc/IniSpecification.md) for details.

## Dependencies

+ [BepInEx](https://github.com/BepInEx/BepInEx) 5
+ [BepInEx.MonoMod.Loader](https://github.com/BepInEx/BepInEx.MonoMod.Loader) (optional, required for MonoMod method)

## Installation

There are 2 ways to install each mod. Just choose between one of them.

+ **Plugin Method**: Put `<ModName>_plugin.dll` into `BepInEx\plugins` folder.
+ **MonoMod Method**: Put `Assembly-CSharp.<ModName>.mm.dll` into `BepInEx\monomod` folder.

For *ExtendOffset*, there is also a final solution, which is to replace the game's `Assembly-CSharp.dll` with `Assembly-CSharp-offset10.dll`. BEWARE: Use this method if and only if neither of the above methods is feasible.

## Settings Modification

Put `mu3-VO.ini` into `<MU3_game_path>\package` folder, and modify the value (0 or 1) of each setting in the ini file as needed before launching the game.

Currently *CleanBattlefield* and *ShutTheFxxkUp* supports settings changes in this way. For detailed specifications, please refer [here](doc/IniSpecification.md).

## Supported Game Versions

+ 1.50

## Known Issues

+ The MonoMod version of *ExtendOffset* is currently incompatible with [*MoreProfileOptions*](https://www.rainycolor.org/package/7EVENDAYSHOLIDAYS/MoreProfileOptions/). Specifically, when using both of them at the same time, the adjustable range of offset A & B will possibly be locked between -10.0 and -6.0.
  + The Plugin version released lately is expected to address this issue, but still lacks more testing.
  + A modded `Assembly-CSharp-offset10.dll` will be provided as a final workaround.

+ When *ExtendOffset* is used, and offset A ia adjusted to a larger value (whether positive or negative), the cue ticks before the music starts will noticeably deviate from the music.
  + Using *ShiftSECount* will mitigate the situation in most cases. But we don't know how well the devs are doing with the audio alignment.

+ (Fixing & Testing) The MonoMod version of *CleanBattlefield* does not hide skills info in the final score page.

## Build from Source

1. Put `Assembly-CSharp.dll` into `.\lib` folder. It can be retrieved from `<MU3_game_path>\package\mu3_Data\Managed`.

2. Open the solution with Visual Studio (preferably VS2022) and build all projects.

## Credits

+ [mu3-mods](https://gitea.tendokyu.moe/akanyan/mu3-mods), for providing inspiration and reference for my MonoMods.
+ [mu3livetools](https://github.com/r-value/mu3livetools), for providing inspiration and reference for my BepInEx Plugins and some other useful things.
