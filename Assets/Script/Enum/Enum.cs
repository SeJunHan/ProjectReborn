public enum Direction { Left = 1, Right = -1 }

public enum SocialClass { Helot, Commons, SemiNoble, Noble, King }
public enum Job
{
    Slayer,
    Smith, Adventurer,
    Knight, Alchemist,
    LowNobility, MiddleNobility, HighNobility,
    King
}
public enum CharacterStatType
{
    MySocialClass = 1, MyJob, /*MyAge,*/ Reputation,
    MyPositon, ActivePoint, Proficiency, MyItem,
    Smith, Bania,
    Knight, Alchemist,
    Nobility,
    King,
    Commons, People, Noble
}
public enum ItemType { Plus = 4, Minus }
public enum StartButtonOrder { New, Load, Option, Exit }
public enum UIPopUpOrder { MainUI, InvenPanel, SettingPanel, QuestPanel, ConversationPanel }
public enum UIInventoryOrder { Name, MySocialClass, MyJob, /*MyAge,*/ ActivePoint, Reputation, Proficiency, MyItem = 8 }
public enum UIMainButtonOrder { Setting, SkipDay }
public enum UIMainImageOrder { Time, ActivePoint, /*Qurter,*/ Job, Proficiency }
public enum UIMainTextOrder { Days }
//public enum UISettingButtonOrder { SettingClose, Pause, Save, Load, Sound, Resume, LoadClose, SoundClose, Back, BackOk, BackCancel }
public enum UISettingButtonOrder { SettingClose, Pause, Save, Load, Sound, Back, Resume, LoadClose, SoundClose, BackOk, BackCancel }
public enum UISettingPanelOrder { Pause, Save, Load, Sound, Back }
public enum UISoundOrder { Background, Effect }
public enum KeyDirection { Up, Down, Left, Right }
public enum QuestState { None, SelectResidence, Chat, QuestStand, QuestStart, QuestProgress, QuestEnd, Help, Exchange }
public enum QuestData { QuestNumber, QuestObjectNumber, ClearCount, QuestContent }
public enum PoolData { Skeleton, Boomerang, EnemyAttack, PlayerProjectile, CrowMonster, ScarecrowProjectile }