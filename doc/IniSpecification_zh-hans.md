# INI配置说明

[English](IniSpecification.md) \\
[简体中文](IniSpecification_zh-hans.md)

部分mod支持通过修改`mu3-VO.ini`中的键值来修改mod的设置。

每项设置可填入的内容，请参考可接受值。若无法正常读取相应的键值，则采用“0”的默认值。

理论上，每次进入战斗前，都可以修改`mu3-VO.ini`中的键值。

目前，`mu3-VO.ini`中包含了以下可设置项：
| 段落 | 键 | 说明 | 可接受值 |
| ------------- | ------------- | ------------- | ---------- |
| CleanBattlefield  | ShowPlayerInfo    | 是否显示左侧玩家面板（包含分数、血量、判定数量、受伤数量、BELL数量等） | 0（禁用）或1（启用） |
| CleanBattlefield  | ShowEnemyInfo     | 是否显示右侧敌人面板（包含敌人类型、血量、积累的奖励等） | 0或1 |
| CleanBattlefield  | ShowRetireInfo    | 是否显示战斗中止分数信息 | 0或1 |
| CleanBattlefield  | ShowCardInfo      | 是否显示底部卡牌技能信息 | 0或1 |
| CleanBattlefield  | ShowComboEffects  | 是否显示顶部整百COMBO特效 | 0或1 |
| CleanBattlefield  | ShowDamageNumbers | 是否显示敌人附近的伤害数字 | 0或1 |
| CleanBattlefield  | ShowZakoEffects   | 是否显示小兵被击败特效 | 0或1 |
| CleanBattlefield  | ShowBossEffects   | 是否显示BOSS出场和被击败特效 | 0或1 |
| ShutTheFxxkUp     | PlaySoundEffects  | 是否播放战斗中音效 | 0或1 |
| GuideSEOffset     | OffsetCount       | 音乐开始前提示音的延迟，单位为帧 | 任意小数（建议不超过±10.0） |
| GuideSEOffset     | OffsetAnswer      | 谱面正解音的延迟，单位为帧 | 任意小数（建议不超过±10.0） |