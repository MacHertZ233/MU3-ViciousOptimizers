# INI Config Specification

[English](IniSpecification.md) \\
[简体中文](IniSpecification_zh-hans.md)

Some mods allow you to modify their settings by modifying the key values in `mu3-VO.ini`.

Each setting can be set to **0 (disabled)** or **1 (enabled)**. If the corresponding key value cannot be read properly, the default value of 0 will be used.

Theoretically, the key values in `mu3-VO.ini` can be modified before each battle session.

Currently, the following adjustable options are included in `mu3-VO.ini`:
| Section | Key | Description |
| ------------- | ------------- | ------------- |
| CleanBattlefield  | ShowPlayerInfo    | Whether to display the player panel on the left (including scores, HP, judgement counts, Damage count, Bell count, etc.) |
| CleanBattlefield  | ShowEnemyInfo     | Whether to display the enemy panel on the right (including enemy type, HP, accumulated rewards, etc.) |
| CleanBattlefield  | ShowRetireInfo    | Whether to display Battle Retirement score info |
| CleanBattlefield  | ShowCardInfo      | Whether to display skills info at the bottom |
| CleanBattlefield  | ShowComboEffects  | Whether to display hundred Combo effects at the top |
| CleanBattlefield  | ShowDamageNumbers | Whether to display damage numbers near enemies |
| CleanBattlefield  | ShowZakoEffects   | Whether to display Zako defeat effects |
| CleanBattlefield  | ShowBossEffects   | Whether to display Boss entry and defeat effects |
| ShutTheFxxkUp     | PlaySoundEffects  | Whether to play in-battle sound effects |