namespace AzerothReborn.RealmServer.Reference;

public static class Constants
{
    public const ulong GUID_PLAYER = default;
    public const int RevisionDbCharactersVersion = 1;

    public const int RevisionDbMangosVersion = 21;
    public const int RevisionDbMangosStructure = 7;
    public const int RevisionDbMangosContent = 77;

    public const int RevisionDbRealmVersion = 21;
    public const int RevisionDbRealmStructure = 2;
    public const int RevisionDbRealmContent = 1;

    public const int GROUP_SUBGROUPSIZE = 5;  // (MAX_RAID_SIZE / MAX_GROUP_SIZE)
    public const int GROUP_SIZE = 5;          // Normal Group Size/More then 5, it's a raid group
    public const int GROUP_RAIDSIZE = 40;     // Max Raid Size
    public const ulong GUID_ITEM = 0x4000000000000000UL;
    public const ulong GUID_CONTAINER = 0x4000000000000000UL;
    public const ulong GUID_GAMEOBJECT = 0xF110000000000000UL;
    public const ulong GUID_TRANSPORT = 0xF120000000000000UL;
    public const ulong GUID_UNIT = 0xF130000000000000UL;
    public const ulong GUID_PET = 0xF140000000000000UL;
    public const ulong GUID_DYNAMICOBJECT = 0xF100000000000000UL;
    public const ulong GUID_CORPSE = 0xF101000000000000UL;
    public const ulong GUID_MO_TRANSPORT = 0x1FC0000000000000UL;
    public const uint GUID_MASK_LOW = 0xFFFFFFFFU;
    public const ulong GUID_MASK_HIGH = 0xFFFFFFFF00000000UL;
    public const float DEFAULT_DISTANCE_VISIBLE = 155.8f;
    public const float DEFAULT_DISTANCE_DETECTION = 7f;

    // TODO: Is this correct? The amount of time since last pvp action until you go out of combat again
    public const int DEFAULT_PVP_COMBAT_TIME = 6000; // 6 seconds

    public const int DEFAULT_LOCK_TIMEOUT = 2000;
    public const int DEFAULT_INSTANCE_EXPIRE_TIME = 3600;              // 1 hour
    public const int DEFAULT_BATTLEFIELD_EXPIRE_TIME = 3600 * 24;      // 24 hours
    public static readonly bool[] SERVER_CONFIG_DISABLED_CLASSES = { false, false, false, false, false, false, false, false, false, true, false };
    public static readonly bool[] SERVER_CONFIG_DISABLED_RACES = { false, false, false, false, false, false, false, false, true, false, false };
    public const float UNIT_NORMAL_WALK_SPEED = 2.5f;
    public const float UNIT_NORMAL_RUN_SPEED = 7.0f;
    public const float UNIT_NORMAL_SWIM_SPEED = 4.722222f;
    public const float UNIT_NORMAL_SWIM_BACK_SPEED = 2.5f;
    public const float UNIT_NORMAL_WALK_BACK_SPEED = 4.5f;
    public const float UNIT_NORMAL_TURN_RATE = (float)Math.PI;
    public const float UNIT_NORMAL_TAXI_SPEED = 32.0f;
    public const int PLAYER_VISIBLE_ITEM_SIZE = 12;
    public const int PLAYER_SKILL_INFO_SIZE = 384 - 1;
    public const int PLAYER_EXPLORED_ZONES_SIZE = 64 - 1;
    public const int FIELD_MASK_SIZE_PLAYER = ((int)EPlayerFields.PLAYER_END + 32) / 32 * 32;
    public const int FIELD_MASK_SIZE_UNIT = ((int)EUnitFields.UNIT_END + 32) / 32 * 32;
    public const int FIELD_MASK_SIZE_GAMEOBJECT = ((int)EGameObjectFields.GAMEOBJECT_END + 32) / 32 * 32;
    public const int FIELD_MASK_SIZE_DYNAMICOBJECT = ((int)EDynamicObjectFields.DYNAMICOBJECT_END + 32) / 32 * 32;
    public const int FIELD_MASK_SIZE_ITEM = ((int)EContainerFields.CONTAINER_END + 32) / 32 * 32;
    public const int FIELD_MASK_SIZE_CORPSE = ((int)ECorpseFields.CORPSE_END + 32) / 32 * 32;
    public static readonly string[] WorldServerStatus = { "ONLINE/G", "ONLINE/R", "OFFLINE " };
    public const int Required_Build_1_12_1 = 5875;
    public const int Required_Build_1_12_2 = 6005;
    public const int Required_Build_1_12_3 = 6141;
    public const int ConnectionSleepTime = 100;
    public const int GUILD_RANK_MAX = 9; // Max Ranks Per Guild
    public const int GUILD_RANK_MIN = 5; // Min Ranks Per Guild
    public const string GOSSIP_TEXT_BANK = "The Bank";
    public const string GOSSIP_TEXT_WINDRIDER = "Wind rider master";
    public const string GOSSIP_TEXT_GRYPHON = "Gryphon Master";
    public const string GOSSIP_TEXT_BATHANDLER = "Bat Handler";
    public const string GOSSIP_TEXT_HIPPOGRYPH = "Hippogryph Master";
    public const string GOSSIP_TEXT_FLIGHTMASTER = "Flight Master";
    public const string GOSSIP_TEXT_AUCTIONHOUSE = "Auction House";
    public const string GOSSIP_TEXT_GUILDMASTER = "Guild Master";
    public const string GOSSIP_TEXT_INN = "The Inn";
    public const string GOSSIP_TEXT_MAILBOX = "Mailbox";
    public const string GOSSIP_TEXT_STABLEMASTER = "Stable Master";
    public const string GOSSIP_TEXT_WEAPONMASTER = "Weapons Trainer";
    public const string GOSSIP_TEXT_BATTLEMASTER = "Battlemaster";
    public const string GOSSIP_TEXT_CLASSTRAINER = "Class Trainer";
    public const string GOSSIP_TEXT_PROFTRAINER = "Profession Trainer";
    public const string GOSSIP_TEXT_OFFICERS = "The officers` lounge";
    public const string GOSSIP_TEXT_ALTERACVALLEY = "Alterac Valley";
    public const string GOSSIP_TEXT_ARATHIBASIN = "Arathi Basin";
    public const string GOSSIP_TEXT_WARSONGULCH = "Warsong Gulch";
    public const string GOSSIP_TEXT_IRONFORGE_BANK = "Bank of Ironforge";
    public const string GOSSIP_TEXT_STORMWIND_BANK = "Bank of Stormwind";
    public const string GOSSIP_TEXT_DEEPRUNTRAM = "Deeprun Tram";
    public const string GOSSIP_TEXT_ZEPPLINMASTER = "Zeppelin master";
    public const string GOSSIP_TEXT_FERRY = "Rut'theran Ferry";
    public const string GOSSIP_TEXT_DRUID = "Druid";
    public const string GOSSIP_TEXT_HUNTER = "Hunter";
    public const string GOSSIP_TEXT_PRIEST = "Priest";
    public const string GOSSIP_TEXT_ROGUE = "Rogue";
    public const string GOSSIP_TEXT_WARRIOR = "Warrior";
    public const string GOSSIP_TEXT_PALADIN = "Paladin";
    public const string GOSSIP_TEXT_SHAMAN = "Shaman";
    public const string GOSSIP_TEXT_MAGE = "Mage";
    public const string GOSSIP_TEXT_WARLOCK = "Warlock";
    public const string GOSSIP_TEXT_ALCHEMY = "Alchemy";
    public const string GOSSIP_TEXT_BLACKSMITHING = "Blacksmithing";
    public const string GOSSIP_TEXT_COOKING = "Cooking";
    public const string GOSSIP_TEXT_ENCHANTING = "Enchanting";
    public const string GOSSIP_TEXT_ENGINEERING = "Engineering";
    public const string GOSSIP_TEXT_FIRSTAID = "First Aid";
    public const string GOSSIP_TEXT_HERBALISM = "Herbalism";
    public const string GOSSIP_TEXT_LEATHERWORKING = "Leatherworking";
    public const string GOSSIP_TEXT_POISONS = "Poisons";
    public const string GOSSIP_TEXT_TAILORING = "Tailoring";
    public const string GOSSIP_TEXT_MINING = "Mining";
    public const string GOSSIP_TEXT_FISHING = "Fishing";
    public const string GOSSIP_TEXT_SKINNING = "Skinning";

    // VMAPS
    public const string VMAP_MAGIC = "VMAP_2.0";

    public const float VMAP_MAX_CAN_FALL_DISTANCE = 10.0f;
    public const float VMAP_INVALID_HEIGHT = -100000.0f; // for check
    public const float VMAP_INVALID_HEIGHT_VALUE = -200000.0f; // real assigned value in unknown height case

    // MAPS
    public const float SIZE = 533.3333f;

    public const int RESOLUTION_WATER = 128 - 1;
    public const int RESOLUTION_FLAGS = 16 - 1;
    public const int RESOLUTION_TERRAIN = 16 - 1;
    public static readonly int groundFlagsMask = unchecked((int)(0xFFFFFFFF & (int)~(MovementFlags.MOVEMENTFLAG_LEFT | MovementFlags.MOVEMENTFLAG_RIGHT | MovementFlags.MOVEMENTFLAG_BACKWARD | MovementFlags.MOVEMENTFLAG_FORWARD | MovementFlags.MOVEMENTFLAG_WALK)));
    public static readonly int movementFlagsMask = (int)(MovementFlags.MOVEMENTFLAG_FORWARD | MovementFlags.MOVEMENTFLAG_BACKWARD | MovementFlags.MOVEMENTFLAG_STRAFE_LEFT | MovementFlags.MOVEMENTFLAG_STRAFE_RIGHT | MovementFlags.MOVEMENTFLAG_PITCH_UP | MovementFlags.MOVEMENTFLAG_PITCH_DOWN | MovementFlags.MOVEMENTFLAG_JUMPING | MovementFlags.MOVEMENTFLAG_FALLING | MovementFlags.MOVEMENTFLAG_SWIMMING | MovementFlags.MOVEMENTFLAG_SPLINE);
    public static readonly int TurningFlagsMask = (int)(MovementFlags.MOVEMENTFLAG_LEFT | MovementFlags.MOVEMENTFLAG_RIGHT);
    public static readonly int movementOrTurningFlagsMask = movementFlagsMask | TurningFlagsMask;
    public const byte ITEM_SLOT_NULL = 255;
    public const long ITEM_BAG_NULL = -1;
    public const int PETITION_GUILD_PRICE = 1000;
    public const int PETITION_GUILD = 5863;       // Guild Charter, ItemFlags = &H2000
    public const int GUILD_TABARD_ITEM = 5976;
    public const int CREATURE_MAX_SPELLS = 4;
    public const int MAX_OWNER_DIS = 100;
    public const int SPELL_DURATION_INFINITE = -1;
    public const int MAX_AURA_EFFECTs_VISIBLE = 48;                  // 48 AuraSlots (32 buff, 16 debuff)
    public const int MAX_AURA_EFFECTs_PASSIVE = 192;
    public static readonly int MAX_AURA_EFFECTs = MAX_AURA_EFFECTs_VISIBLE + MAX_AURA_EFFECTs_PASSIVE;
    public static readonly int MAX_AURA_EFFECT_FLAGs = MAX_AURA_EFFECTs_VISIBLE / 8;
    public static readonly int MAX_AURA_EFFECT_LEVELSs = MAX_AURA_EFFECTs_VISIBLE / 4;
    public const int MAX_POSITIVE_AURA_EFFECTs = 32;
    public static readonly int MAX_NEGATIVE_AURA_EFFECTs = MAX_AURA_EFFECTs_VISIBLE - MAX_POSITIVE_AURA_EFFECTs;
    public const uint UINT32_MAX = 0xFFFFFFFF;
    public const int UINT32_MIN = 0;
    public const long MpqId = 441536589L;
    public const long MpqHeaderSize = 32L;
}
