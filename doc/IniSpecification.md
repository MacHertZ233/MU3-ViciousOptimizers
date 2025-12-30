# INI Config Specification

[English](IniSpecification.md) \\
[简体中文](IniSpecification_zh-hans.md)

Some mods allow you to modify their settings by modifying the key values in `mu3-VO.ini`.

For each setting, please refer to the Acceptable Values for content input. If the corresponding key value cannot be read properly, the default value of "0" will be used.

Theoretically, the key values in `mu3-VO.ini` can be modified before each battle session.

Currently, the following adjustable options are included in `mu3-VO.ini`:
| Section | Key | Description | Acceptable Values |
| ------------- | ------------- | ------------- | ---------- |
| CleanBattlefield  | ShowPlayerInfo    | Whether to display the player panel on the left (including scores, HP, judgement counts, Damage count, Bell count, etc.) | 0 (disable) or 1 (enable) |
| CleanBattlefield  | ShowEnemyInfo     | Whether to display the enemy panel on the right (including enemy type, HP, accumulated rewards, etc.) | 0 or 1 |
| CleanBattlefield  | ShowRetireInfo    | Whether to display Battle Retirement score info | 0 or 1 |
| CleanBattlefield  | ShowCardInfo      | Whether to display skills info at the bottom | 0 or 1 |
| CleanBattlefield  | ShowComboEffects  | Whether to display hundred Combo effects at the top | 0 or 1 |
| CleanBattlefield  | ShowDamageNumbers | Whether to display damage numbers near enemies | 0 or 1 |
| CleanBattlefield  | ShowZakoEffects   | Whether to display Zako defeat effects | 0 or 1 |
| CleanBattlefield  | ShowBossEffects   | Whether to display Boss entry and defeat effects | 0 or 1 |
| ShutTheFxxkUp     | PlaySoundEffects  | Whether to play in-battle sound effects | 0 or 1 |
| GuideSEOffset     | OffsetCount       | Offset of the countdown ticks before the music starts, measured in frames | Any decimal (recommended not to exceed ±10.0) |
| GuideSEOffset     | OffsetAnswer      | Offset of the correct hint SEs, measured in frames | Any decimal (recommended not to exceed ±10.0) |