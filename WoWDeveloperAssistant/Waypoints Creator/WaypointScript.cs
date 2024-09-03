using System;
using System.Collections.Generic;
using WoWDeveloperAssistant.Misc;
using static WoWDeveloperAssistant.Misc.Packets;

namespace WoWDeveloperAssistant.Waypoints_Creator
{
    [Serializable]
    public class WaypointScript : ICloneable
    {
        public enum ScriptType : byte
        {
            Talk           = 0,
            Emote          = 1,
            SetField       = 2,
            SetFlag        = 4,
            RemoveFlag     = 5,
            RemoveAura     = 14,
            CastSpell      = 15,
            SetOrientation = 30,
            SetAnimKit     = 100,
            Jump           = 101,
            Unknown        = 99
        };

        enum Emotes
        {
            EMOTE_ONESHOT_NONE = 0,
            EMOTE_ONESHOT_TALK = 1,
            EMOTE_ONESHOT_BOW = 2,
            EMOTE_ONESHOT_WAVE = 3,
            EMOTE_ONESHOT_CHEER = 4,
            EMOTE_ONESHOT_EXCLAMATION = 5,
            EMOTE_ONESHOT_QUESTION = 6,
            EMOTE_ONESHOT_EAT = 7,
            EMOTE_STATE_DANCE = 10,
            EMOTE_ONESHOT_LAUGH = 11,
            EMOTE_STATE_SLEEP = 12,
            EMOTE_STATE_SIT = 13,
            EMOTE_ONESHOT_RUDE = 14,
            EMOTE_ONESHOT_ROAR = 15,
            EMOTE_ONESHOT_KNEEL = 16,
            EMOTE_ONESHOT_KISS = 17,
            EMOTE_ONESHOT_CRY = 18,
            EMOTE_ONESHOT_CHICKEN = 19,
            EMOTE_ONESHOT_BEG = 20,
            EMOTE_ONESHOT_APPLAUD = 21,
            EMOTE_ONESHOT_SHOUT = 22,
            EMOTE_ONESHOT_FLEX = 23,
            EMOTE_ONESHOT_SHY = 24,
            EMOTE_ONESHOT_POINT = 25,
            EMOTE_STATE_STAND = 26,
            EMOTE_STATE_READYUNARMED = 27,
            EMOTE_STATE_WORK_SHEATHED = 28,
            EMOTE_STATE_POINT = 29,
            EMOTE_STATE_NONE = 30,
            EMOTE_ONESHOT_WOUND = 33,
            EMOTE_ONESHOT_WOUND_CRITICAL = 34,
            EMOTE_ONESHOT_ATTACK_UNARMED = 35,
            EMOTE_ONESHOT_ATTACK1H = 36,
            EMOTE_ONESHOT_ATTACK2HTIGHT = 37,
            EMOTE_ONESHOT_ATTACK2HLOOSE = 38,
            EMOTE_ONESHOT_PARRYUNARMED = 39,
            EMOTE_ONESHOT_PARRY_SHIELD = 43,
            EMOTE_ONESHOT_READYUNARMED = 44,
            EMOTE_ONESHOT_READY1H = 45,
            EMOTE_ONESHOT_READYBOW = 48,
            EMOTE_ONESHOT_SPELLPRECAST = 50,
            EMOTE_ONESHOT_SPELL_CAST = 51,
            EMOTE_ONESHOT_BATTLEROAR = 53,
            EMOTE_ONESHOT_SPECIALATTACK1H = 54,
            EMOTE_ONESHOT_KICK = 60,
            EMOTE_ONESHOT_ATTACKTHROWN = 61,
            EMOTE_STATE_STUN = 64,
            EMOTE_STATE_DEAD = 65,
            EMOTE_ONESHOT_SALUTE = 66,
            EMOTE_STATE_KNEEL = 68,
            EMOTE_STATE_USE_STANDING = 69,
            EMOTE_ONESHOT_WAVE_NOSHEATHE = 70,
            EMOTE_ONESHOT_CHEER_NOSHEATHE = 71,
            EMOTE_ONESHOT_EAT_NOSHEATHE = 92,
            EMOTE_STATE_STUN_NOSHEATHE = 93,
            EMOTE_ONESHOT_DANCE = 94,
            EMOTE_ONESHOT_SALUTE_NOSHEATH = 113,
            EMOTE_STATE_USE_STANDING_NO_SHEATHE = 133,
            EMOTE_ONESHOT_LAUGH_NOSHEATHE = 153,
            EMOTE_STATE_WORK = 173,
            EMOTE_STATE_SPELLPRECAST = 193,
            EMOTE_ONESHOT_READYRIFLE = 213,
            EMOTE_STATE_READYRIFLE = 214,
            EMOTE_STATE_WORK_MINING = 233,
            EMOTE_STATE_WORK_CHOPWOOD = 234,
            EMOTE_STATE_APPLAUD = 253,
            EMOTE_ONESHOT_LIFTOFF = 254,
            EMOTE_ONESHOT_YES = 273,
            EMOTE_ONESHOT_NO = 274,
            EMOTE_ONESHOT_TRAIN = 275,
            EMOTE_ONESHOT_LAND = 293,
            EMOTE_STATE_AT_EASE = 313,
            EMOTE_STATE_READY1H = 333,
            EMOTE_STATE_SPELLKNEELSTART = 353,
            EMOTE_STAND_STATE_SUBMERGED = 373,
            EMOTE_ONESHOT_SUBMERGE = 374,
            EMOTE_STATE_READY2H = 375,
            EMOTE_STATE_READYBOW = 376,
            EMOTE_ONESHOT_MOUNTSPECIAL = 377,
            EMOTE_STATE_TALK = 378,
            EMOTE_STATE_FISHING = 379,
            EMOTE_ONESHOT_FISHING = 380,
            EMOTE_ONESHOT_LOOT = 381,
            EMOTE_STATE_WHIRLWIND = 382,
            EMOTE_STATE_DROWNED = 383,
            EMOTE_STATE_HOLD_BOW = 384,
            EMOTE_STATE_HOLD_RIFLE = 385,
            EMOTE_STATE_HOLD_THROWN = 386,
            EMOTE_ONESHOT_DROWN = 387,
            EMOTE_ONESHOT_STOMP = 388,
            EMOTE_ONESHOT_ATTACKOFF = 389,
            EMOTE_ONESHOT_ATTACKOFFPIERCE = 390,
            EMOTE_STATE_ROAR = 391,
            EMOTE_STATE_LAUGH = 392,
            EMOTE_ONESHOT_CREATURE_SPECIAL = 393,
            EMOTE_ONESHOT_JUMPLANDRUN = 394,
            EMOTE_ONESHOT_JUMPEND = 395,
            EMOTE_ONESHOT_TALK_NO_SHEATHE = 396,
            EMOTE_ONESHOT_POINT_NO_SHEATHE = 397,
            EMOTE_STATE_CANNIBALIZE = 398,
            EMOTE_ONESHOT_JUMPSTART = 399,
            EMOTE_STATE_DANCESPECIAL = 400,
            EMOTE_ONESHOT_DANCESPECIAL = 401,
            EMOTE_ONESHOT_CUSTOM_SPELL_01 = 402,
            EMOTE_ONESHOT_CUSTOM_SPELL_02 = 403,
            EMOTE_ONESHOT_CUSTOM_SPELL_03 = 404,
            EMOTE_ONESHOT_CUSTOM_SPELL_04 = 405,
            EMOTE_ONESHOT_CUSTOM_SPELL_05 = 406,
            EMOTE_ONESHOT_CUSTOM_SPELL_06 = 407,
            EMOTE_ONESHOT_CUSTOM_SPELL_07 = 408,
            EMOTE_ONESHOT_CUSTOM_SPELL_08 = 409,
            EMOTE_ONESHOT_CUSTOM_SPELL_09 = 410,
            EMOTE_ONESHOT_CUSTOM_SPELL_10 = 411,
            EMOTE_STATE_EXCLAIM = 412,
            EMOTE_STATE_DANCE_CUSTOM = 413,
            EMOTE_STATE_SIT_CHAIR_MED = 415,
            EMOTE_STATE_CUSTOM_SPELL_01 = 416,
            EMOTE_STATE_CUSTOM_SPELL_02 = 417,
            EMOTE_STATE_EAT = 418,
            EMOTE_STATE_CUSTOM_SPELL_04 = 419,
            EMOTE_STATE_CUSTOM_SPELL_03 = 420,
            EMOTE_STATE_CUSTOM_SPELL_05 = 421,
            EMOTE_STATE_SPELLEFFECT_HOLD = 422,
            EMOTE_STATE_EAT_NO_SHEATHE = 423,
            EMOTE_STATE_MOUNT = 424,
            EMOTE_STATE_READY2HL = 425,
            EMOTE_STATE_SIT_CHAIR_HIGH = 426,
            EMOTE_STATE_FALL = 427,
            EMOTE_STATE_LOOT = 428,
            EMOTE_STATE_SUBMERGED = 429,
            EMOTE_ONESHOT_COWER = 430,
            EMOTE_STATE_COWER = 431,
            EMOTE_ONESHOT_USESTANDING = 432,
            EMOTE_STATE_STEALTH_STAND = 433,
            EMOTE_ONESHOT_OMNICAST_GHOUL = 434,
            EMOTE_ONESHOT_ATTACKBOW = 435,
            EMOTE_ONESHOT_ATTACKRIFLE = 436,
            EMOTE_STATE_SWIM_IDLE = 437,
            EMOTE_STATE_ATTACK_UNARMED = 438,
            EMOTE_ONESHOT_SPELLCAST_NEW = 439,
            EMOTE_ONESHOT_DODGE = 440,
            EMOTE_ONESHOT_PARRY1H = 441,
            EMOTE_ONESHOT_PARRY2H = 442,
            EMOTE_ONESHOT_PARRY2HL = 443,
            EMOTE_STATE_FLYFALL = 444,
            EMOTE_ONESHOT_FLYDEATH = 445,
            EMOTE_STATE_FLY_FALL = 446,
            EMOTE_ONESHOT_FLY_SIT_GROUND_DOWN = 447,
            EMOTE_ONESHOT_FLY_SIT_GROUND_UP = 448,
            EMOTE_ONESHOT_EMERGE = 449,
            EMOTE_ONESHOT_DRAGONSPIT = 450,
            EMOTE_STATE_SPECIALUNARMED = 451,
            EMOTE_ONESHOT_FLYGRAB = 452,
            EMOTE_STATE_FLYGRABCLOSED = 453,
            EMOTE_ONESHOT_FLYGRABTHROWN = 454,
            EMOTE_STATE_FLY_SIT_GROUND = 455,
            EMOTE_STATE_WALKBACKWARDS = 456,
            EMOTE_ONESHOT_FLYTALK = 457,
            EMOTE_ONESHOT_FLYATTACK1H = 458,
            EMOTE_STATE_CUSTOMSPELL08 = 459,
            EMOTE_ONESHOT_FLY_DRAGONSPIT = 460,
            EMOTE_STATE_SIT_CHAIR_LOW = 461,
            EMOTE_ONE_SHOT_STUN = 462,
            EMOTE_ONESHOT_SPELL_CAST_OMNI = 463,
            EMOTE_STATE_READYTHROWN = 465,
            EMOTE_ONESHOT_WORK_CHOPWOOD = 466,
            EMOTE_ONESHOT_WORK_MINING = 467,
            EMOTE_STATE_SPELL_CHANNEL_OMNI = 468,
            EMOTE_STATE_SPELL_CHANNEL_DIRECTED = 469,
            EMOTE_STAND_STATE_NONE = 470,
            EMOTE_STATE_READYJOUST = 471,
            EMOTE_STATE_STRANGULATE = 472,
            EMOTE_STATE_STRANGULATE2 = 473,
            EMOTE_STATE_READYSPELLOMNI = 474,
            EMOTE_STATE_HOLD_JOUST = 475,
            EMOTE_ONESHOT_CRY_JAINA = 476,
            EMOTE_ONESHOT_SPECIALUNARMED = 477,
            EMOTE_STATE_DANCE_NOSHEATHE = 478,
            EMOTE_ONESHOT_SNIFF = 479,
            EMOTE_ONESHOT_DRAGONSTOMP = 480,
            EMOTE_ONESHOT_KNOCKDOWN = 482,
            EMOTE_STATE_READ = 483,
            EMOTE_ONESHOT_FLYEMOTETALK = 485,
            EMOTE_STATE_READ_ALLOWMOVEMENT = 492,
            EMOTE_STATE_CUSTOM_SPELL_06 = 498,
            EMOTE_STATE_CUSTOM_SPELL_07 = 499,
            EMOTE_STATE_CUSTOM_SPELL_08 = 500,
            EMOTE_STATE_CUSTOM_SPELL_09 = 501,
            EMOTE_STATE_CUSTOM_SPELL_10 = 502,
            EMOTE_STATE_READY1H_ALLOW_MOVEMENT = 505,
            EMOTE_STATE_READY2H_ALLOW_MOVEMENT = 506,
            EMOTE_ONESHOT_MONKOFFENSE_ATTACKUNARMED = 507,
            EMOTE_ONESHOT_MONKOFFENSE_SPECIALUNARMED = 508,
            EMOTE_ONESHOT_MONKOFFENSE_PARRYUNARMED = 509,
            EMOTE_STATE_MONKOFFENSE_READYUNARMED = 510,
            EMOTE_ONESHOT_PALMSTRIKE = 511,
            EMOTE_STATE_CRANE = 512,
            EMOTE_ONESHOT_OPEN = 517,
            EMOTE_STATE_READ_CHRISTMAS = 518,
            EMOTE_ONESHOT_FLYATTACK2HL = 526,
            EMOTE_ONESHOT_FLYATTACKTHROWN = 527,
            EMOTE_STATE_FLYREADYSPELLDIRECTED = 528,
            EMOTE_STATE_FLY_READY_1H = 531,
            EMOTE_STATE_MEDITATE = 533,
            EMOTE_STATE_FLY_READY_2HL = 534,
            EMOTE_ONESHOT_TOGROUND = 535,
            EMOTE_ONESHOT_TOFLY = 536,
            EMOTE_STATE_ATTACKTHROWN = 537,
            EMOTE_STATE_SPELL_CHANNEL_DIRECTED_NOSOUND = 538,
            EMOTE_ONESHOT_WORK = 539,
            EMOTE_STATE_READYUNARMED_NOSOUND = 540,
            EMOTE_ONESHOT_MONKOFFENSE_ATTACKUNARMEDOFF = 543,
            EMOTE_RECLINED_MOUNT_PASSENGER = 546,
            EMOTE_ONESHOT_QUESTION_NEW = 547,
            EMOTE_ONESHOT_SPELL_CHANNEL_DIRECTED_NOSOUND = 549,
            EMOTE_STATE_KNEEL_NEW = 550,
            EMOTE_ONESHOT_FLYATTACKUNARMED = 551,
            EMOTE_ONESHOT_FLYCOMBATWOUND = 552,
            EMOTE_ONESHOT_MOUNTSELFSPECIAL = 553,
            EMOTE_ONESHOT_ATTACKUNARMED_NOSOUND = 554,
            EMOTE_STATE_WOUNDCRITICAL_DOESNTWORK = 555,
            EMOTE_ONESHOT_ATTACK1H_NOSOUND_DOESNTWORK = 556,
            EMOTE_STATE_MOUNT_SELF_IDLE = 557,
            EMOTE_ONESHOT_WALK = 558,
            EMOTE_STATE_OPENED = 559,
            EMOTE_ONESHOT_YELL_USE_ONESHOT_SHOUT = 560,
            EMOTE_ONESHOT_BREATHOFFIRE = 565,
            EMOTE_STATE_ATTACK1H = 567,
            EMOTE_STATE_USESTANDING_LOOP_1 = 569,
            EMOTE_STATE_USESTANDING = 572,
            EMOTE_ONESHOT_LAUGH_NOSOUND = 574,
            EMOTE_RECLINED_MOUNT = 575,
            EMOTE_ONESHOT_ATTACK1H_NEW = 577,
            EMOTE_STATE_CRY_NOSOUND = 578,
            EMOTE_ONESHOT_CRY_NOSOUND = 579,
            EMOTE_ONESHOT_COMBATCRITICAL = 584,
            EMOTE_STATE_TRAIN = 585,
            EMOTE_STATE_WORK_CHOPWOOD_NEW = 586,
            EMOTE_ONESHOT_SPECIALATTACK2H = 587,
            EMOTE_STATE_READ_AND_TALK = 588,
            EMOTE_ONESHOT_STAND_VAR1 = 589,
            EMOTE_REXXAR_STRANGLES_GOBLIN = 590,
            EMOTE_ONESHOT_STAND_VAR2 = 591,
            EMOTE_ONESHOT_DEATH = 592,
            EMOTE_STATE_TALKONCE = 595,
            EMOTE_STATE_ATTACK2H = 596,
            EMOTE_STATE_SIT_GROUND = 598,
            EMOTE_STATE_WORK_CHOPWOOD_GARR = 599,
            EMOTE_STATE_CUSTOMSPELL01 = 601,
            EMOTE_ONESHOT_COMBATWOUND = 602,
            EMOTE_ONESHOT_TALK_EXCLAMATION = 603,
            EMOTE_ONESHOT_QUESTION_SWIM_ALLOW = 604,
            EMOTE_STATE_CRY = 605,
            EMOTE_STATE_USESTANDING_LOOP = 606,
            EMOTE_STATE_WORK_SMITH = 613,
            EMOTE_STATE_WORK_CHOPWOOD_GARR_FLESH = 614,
            EMOTE_STATE_CUSTOMSPELL02 = 615,
            EMOTE_STATE_READ_AND_SIT = 616,
            EMOTE_STATE_READYSPELLDIRECTED = 617,
            EMOTE_STATE_PARRY_UNARMED = 619,
            EMOTE_STATE_STATE_BLOCK_SHIELD = 620,
            EMOTE_STATE_STATE_SIT_GROUND = 621,
            EMOTE_STATE_ONESHOT_MOUNT_SPECIAL = 628,
            EMOTE_STATE_DRAGONS_PITHOVER = 629,
            EMOTE_STATE_EMOTE_SETTLE = 635,
            EMOTE_STATE_ONESHOT_SETTLE = 636,
            EMOTE_STATE_STATE_ATTACK_UNARMED_STILL = 638,
            EMOTE_STATE_STATE_READ_BOOK_AND_TALK = 641,
            EMOTE_STATE_ONESHOT_SLAM = 642,
            EMOTE_STATE_ONESHOT_GRABTHROWN = 643,
            EMOTE_STATE_ONESHOT_READYSPELLDIRECTED_NOSOUND = 644,
            EMOTE_STATE_STATE_READYSPELLOMNI_WITH_SOUND = 645,
            EMOTE_STATE_ONESHOT_TALK_BARSERVER = 646,
            EMOTE_STATE_ONESHOT_WAVE_BARSERVER = 647,
            EMOTE_STATE_STATE_WORK_MINING = 648,
            EMOTE_STATE_STATE_READY2HL_ALLOW_MOVEMENT = 654,
            EMOTE_STATE_STATE_USESTANDING_NOSHEATHE = 655,
            EMOTE_STATE_ONESHOT_WORK = 657,
            EMOTE_STATE_STATE_HOLD_THROWN = 658,
            EMOTE_STATE_ONESHOT_CANNIBALIZE = 659,
            EMOTE_STATE_ONESHOT_NO = 661,
            EMOTE_STATE_ONESHOT_ATTACKUNARMED_NOFLAGS = 662,
            EMOTE_STATE_STATE_READYGLV = 663,
            EMOTE_STATE_ONESHOT_COMBATABILITYGLV01 = 664,
            EMOTE_STATE_ONESHOT_COMBATABILITYGLVOFF01 = 665,
            EMOTE_STATE_ONESHOT_COMBATABILITYGLVBIG02 = 666,
            EMOTE_STATE_ONESHOT_PARRYGLV = 667,
            EMOTE_STATE_STATE_WORK_MININGNOMOVEMENT = 668,
            EMOTE_STATE_ONESHOT_TALK_NOSHEATHE = 669,
            EMOTE_STATE_ONESHOT_STAND_VAR3 = 671,
            EMOTE_STATE_STATE_KNEEL = 672,
            EMOTE_STATE_ONESHOT_CUSTOM0 = 673,
            EMOTE_STATE_ONESHOT_CUSTOM1 = 674,
            EMOTE_STATE_ONESHOT_CUSTOM2 = 675,
            EMOTE_STATE_ONESHOT_CUSTOM3 = 676,
            EMOTE_STATE_STATE_FLY_READY_UNARMED = 677,
            EMOTE_STATE_ONESHOT_CHEER_FORTHEALLIANCE = 679,
            EMOTE_STATE_ONESHOT_CHEER_FORTHEHORDE = 680,
            EMOTE_STATE_UNKLEGION = 689, ///< No name in DB2
            EMOTE_STATE_ONESHOT_STAND_VAR4 = 690,
            EMOTE_STATE_ONESHOT_FLYEMOTEEXCLAMATION = 691,
            EMOTE_STATE_UNKLEGION_1 = 696, ///< No name in DB2
            EMOTE_STATE_UNKLEGION_2 = 699, ///< No name in DB2
            EMOTE_STATE_EMOTE_EAT = 700,
            EMOTE_STATE_UNKLEGION_3 = 701, ///< No name in DB2
            EMOTE_STATE_MONKHEAL_CHANNELOMNI = 705,
            EMOTE_STATE_MONKDEFENSE_READYUNARMED = 706,
            EMOTE_ONESHOT_STAND = 707,
            EMOTE_STATE_UNKLEGION_4 = 708, ///< No name in DB2
            EMOTE_STATE_MONK2HLIDLE = 712,
            EMOTE_STATE_WAPERCH = 713,
            EMOTE_STATE_WAGUARDSTAND01 = 714,
            EMOTE_STATE_READ_AND_SIT_CHAIR_MED = 715,
            EMOTE_STATE_WAGUARDSTAND02 = 716,
            EMOTE_STATE_WAGUARDSTAND03 = 717,
            EMOTE_STATE_WAGUARDSTAND04 = 718,
            EMOTE_STATE_WACHANT02 = 719,
            EMOTE_STATE_WALEAN01 = 720,
            EMOTE_STATE_DRUNKWALK = 721,
            EMOTE_STATE_WASCRUBBING = 722,
            EMOTE_STATE_WACHANT01 = 723,
            EMOTE_STATE_WACHANT03 = 724,
            EMOTE_STATE_WASUMMON01 = 725,
            EMOTE_STATE_WATRANCE01 = 726,
            EMOTE_STATE_CUSTOMSPELL05 = 727,
            EMOTE_STATE_WAHAMMERLOOP = 728,
            EMOTE_STATE_WABOUND01 = 729,
            EMOTE_STATE_WABOUND02 = 730,
            EMOTE_STATE_WASACKHOLD = 731,
            EMOTE_STATE_WASIT01 = 732,
            EMOTE_STATE_WAROWINGSTANDLEFT = 733,
            EMOTE_STATE_WAROWINGSTANDRIGHT = 734,
            EMOTE_STATE_LOOT_BITE_SOUND = 735,
            EMOTE_ONESHOT_WASUMMON01 = 736,
            EMOTE_ONESHOT_STAND_VAR2_ULTRA = 737,
            EMOTE_ONESHOT_FALCONEER_START = 738,
            EMOTE_STATE_FALCONEER_LOOP = 739,
            EMOTE_ONESHOT_FALCONEER_END = 740,
            EMOTE_STATE_WAPERCH_NOINTERACT = 741,
            EMOTE_ONESHOT_WASTANDDRINK = 742,
            EMOTE_STATE_WALEAN02 = 743,
            EMOTE_ONESHOT_READ_END = 744,
            EMOTE_STATE_WAGUARDSTAND04_ALLOW_MOVEMENT = 745,
            EMOTE_STATE_READYCROSSBOW = 747,
            EMOTE_ONESHOT_WASTANDDRINK_NOSHEAT = 748,
            EMOTE_STATE_WAHANG01 = 749,
            EMOTE_STATE_WABEGGARSTAND = 750,
            EMOTE_STATE_WADRUNKSTAND = 751,
            EMOTE_ONESHOT_WACRIERTALK = 753,
            EMOTE_STATE_HOLD_CROSSBOW = 754,
            EMOTE_STATE_WASIT02 = 757,
            EMOTE_STATE_WACRANKSTAND = 761,
            EMOTE_ONESHOT_READ_START = 762,
            EMOTE_ONESHOT_READ_LOOP = 763,
            EMOTE_ONESHOT_WADRUNKDRINK = 765,
            EMOTE_STATE_SIT_CHAIR_MED_EAT = 766,
            EMOTE_STATE_KNEEL_COPY = 767,
            EMOTE_STATE_WORK_CHOPMEAT_NOSHEATHE_CLEAVER = 868,
            EMOTE_UNK_BFA_1 = 869,
            EMOTE_ONESHOT_BARPATRON_POINT = 870,
            EMOTE_STATE_STAND_NOSOUND = 871,
            EMOTE_STATE_MOUNT_FLIGHT_IDLE_NOSOUND = 872,
            EMOTE_STATE_USESTANDING_LOOP_SKINNING = 873,
            EMOTE_ONESHOT_VEHICLEGRAB = 874,
            EMOTE_STATE_USESTANDING_LOOP_JEWELCRAFTING = 875,
            EMOTE_STATE_BARPATRON_STAND = 876,
            EMOTE_ONESHOT_WABEGGARPOINT = 877,
            EMOTE_STATE_WACRIERSTAND01 = 878,
            EMOTE_ONESHOT_WABEGGARBEG = 879,
            EMOTE_STATE_WABOATWHEELSTAND = 880,
            EMOTE_STATE_WASIT03 = 882,
            EMOTE_STATE_BARSWEEP_STAND = 883,
            EMOTE_STATE_WAGUARDSTAND03_NO_INTERUPT = 884,
            EMOTE_STATE_WAGUARDSTAND02_NO_INTERUPT = 885,
            EMOTE_STATE_BARTENDSTAND = 886,
            EMOTE_STATE_WAHAMMERLOOP_KULTIRAN = 887,
            EMOTE_ONESHOT_CLOSE = 898,
            EMOTE_ONESHOT_ATTACK2H = 906,
            EMOTE_STATE_WA_BARREL_HOLD = 908,
            EMOTE_STATE_WA_BARREL_WALK = 909,
            EMOTE_STATE_CUSTOMSPELL04 = 910,
            EMOTE_STATE_FLYWAPERCH01 = 912,
            EMOTE_ONESHOT_PALSPELLCAST1HUP = 916,
            EMOTE_ONESHOT_READYSPELLOMNI = 917,
            EMOTE_ONESHOT_SPELLCAST_DIRECTED = 961,
            EMOTE_STATE_FLYCUSTOMSPELL07 = 977,
            EMOTE_STATE_FLYCHANNELCASTOMNI = 978,
            EMOTE_STATE_CLOSED = 979,
            EMOTE_STATE_CUSTOMSPELL10 = 980,
            EMOTE_STATE_WAWHEELBARROWSTAND = 981,
            EMOTE_STATE_CUSTOMSPELL06 = 982,
            EMOTE_STATE_CUSTOM1 = 983,
            EMOTE_STATE_WASIT04 = 986,
            EMOTE_ONESHOT_BARSWEEP_STAND = 987,
            EMOTE_TORGHAST_TALKING_HEAD_MAW_CAST_SOUND = 989,
            EMOTE_TORGHAST_TALKING_HEAD_MAW_CAST_SOUND2 = 990,
            EMOTE_ONESHOT_STAND_VAR0 = 991,
            EMOTE_ONESHOT_FLYCUSTOMSPELL01 = 992,
            EMOTE_ONESHOT_SPELEFFECT_DECAY = 993,
            EMOTE_STATE_CREATURE_SPECIAL = 994,
            EMOTE_ONESHOT_WAREACT01 = 1001,
            EMOTE_ONESHOT_FLYCUSTOMSPELL04 = 1004,
            EMOTE_ONESHOT_TALK_SUBDUED = 1005,
            EMOTE_STATE_EMOTETALK = 1006,
            EMOTE_STATE_WAINTERACTION = 1007,
            EMOTE_ONESHOT_TAKE_OFF_START = 1009,
            MAX_EMOTE
        };

        enum UnitStandStateTypes
        {
            UNIT_STAND_STATE_STAND = 0,
            UNIT_STAND_STATE_SIT = 1,
            UNIT_STAND_STATE_SIT_CHAIR = 2,
            UNIT_STAND_STATE_SLEEP = 3,
            UNIT_STAND_STATE_SIT_LOW_CHAIR = 4,
            UNIT_STAND_STATE_SIT_MEDIUM_CHAIR = 5,
            UNIT_STAND_STATE_SIT_HIGH_CHAIR = 6,
            UNIT_STAND_STATE_DEAD = 7,
            UNIT_STAND_STATE_KNEEL = 8,
            UNIT_STAND_STATE_SUBMERGED = 9,
            UNIT_STAND_STATE_BARBER_SHOP = 11,
            UNIT_STAND_STATE_HOVER = 33554432,
            UNIT_STAND_STATE_HOVER2 = 50331648,
        };

        enum SheathStates
        {
            SHEATH_STATE_UNARMED = 0,
            SHEATH_STATE_MELEE = 1,
            SHEATH_STATE_RANGED = 2,
            MAX_SHEATH_STATE
        };

        public uint id;
        public uint delay;
        public ScriptType type;
        public uint dataLong;
        public uint dataLongSecond;
        public uint dataInt;
        public float x;
        public float y;
        public float z;
        public float o;
        public uint guid;
        public TimeSpan scriptTime;

        public WaypointScript() {}

        public WaypointScript(uint id, uint delay, ScriptType type, uint dataLong, uint dataLongSecond, uint dataInt, float x, float y, float z, float o, uint guid, TimeSpan time)
        { this.id = id; this.delay = delay; this.type = type; this.dataLong = dataLong; this.dataLongSecond = dataLongSecond; this.dataInt = dataInt; this.x = x; this.y = y; this.z = z; this.o = o; this.guid = guid; this.scriptTime = time; }

        public static List<WaypointScript> GetScriptsFromUpdatePacket(UpdateObjectPacket updatePacket)
        {
            List<WaypointScript> waypointScripts = new List<WaypointScript>();

            if (updatePacket.emoteStateId != null)
                waypointScripts.Add(new WaypointScript(0, 0, ScriptType.Emote, (uint)updatePacket.emoteStateId, 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, updatePacket.packetSendTime));

            if (updatePacket.sheatheState != null)
                waypointScripts.Add(new WaypointScript(0, 0, ScriptType.SetField, 160, (uint)updatePacket.sheatheState, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, updatePacket.packetSendTime));

            if (updatePacket.standState != null)
                waypointScripts.Add(new WaypointScript(0, 0, ScriptType.SetField, 117, (uint)updatePacket.standState, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, updatePacket.packetSendTime));

            if (updatePacket.jumpInfo.IsValid())
                waypointScripts.Add(new WaypointScript(0, 0, ScriptType.Jump, updatePacket.jumpInfo.moveTime, 0, 0, updatePacket.jumpInfo.jumpPos.x, updatePacket.jumpInfo.jumpPos.y, updatePacket.jumpInfo.jumpPos.z, updatePacket.jumpInfo.jumpGravity, 0, updatePacket.packetSendTime));

            return waypointScripts;
        }

        public static WaypointScript GetScriptsFromSpellPacket(SpellStartPacket spellPacket)
        {
            return new WaypointScript(0, 0, ScriptType.CastSpell, spellPacket.spellId, 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, spellPacket.spellCastStartTime);
        }

        public static WaypointScript GetScriptsFromMovementPacket(MonsterMovePacket movePacket)
        {
            if (movePacket.HasJump())
            {
                return new WaypointScript(0, 0, ScriptType.Jump, movePacket.jumpInfo.moveTime, 0, 0, movePacket.jumpInfo.jumpPos.x, movePacket.jumpInfo.jumpPos.y, movePacket.jumpInfo.jumpPos.z, movePacket.jumpInfo.jumpGravity, 0, movePacket.packetSendTime);
            }

            return new WaypointScript(0, 0, ScriptType.SetOrientation, 0, 0, 0, 0.0f, 0.0f, 0.0f, movePacket.creatureOrientation, 0, movePacket.packetSendTime);
        }

        public static WaypointScript GetScriptsFromAuraUpdatePacket(AuraUpdatePacket auraPacket, Creature creature)
        {
            return new WaypointScript(0, 0, ScriptType.RemoveAura, creature.GetSpellIdForAuraSlot(auraPacket), 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, auraPacket.packetSendTime);
        }

        public static WaypointScript GetScriptsFromEmotePacket(EmotePacket emotePacket)
        {
            return new WaypointScript(0, 0, ScriptType.Emote, emotePacket.emoteId, 0, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, emotePacket.packetSendTime);
        }

        public static WaypointScript GetScriptsFromSetAiAnimKitPacket(SetAiAnimKitPacket animKitPacket)
        {
            return new WaypointScript(0, 0, ScriptType.SetAnimKit, (uint)animKitPacket.aiAnimKitId, 0, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, animKitPacket.packetSendTime);
        }

        public static WaypointScript GetScriptsFromPlayOneShotAnimKitPacket(PlayOneShotAnimKitPacket playOneShotAnimKitPacket)
        {
            return new WaypointScript(0, 0, ScriptType.SetAnimKit, (uint)playOneShotAnimKitPacket.animKitId, 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, 0, playOneShotAnimKitPacket.packetSendTime);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void SetId(uint id)
        {
            this.id = id;
        }

        public void SetGuid(uint guid)
        {
            this.guid = guid;
        }

        public void SetDelay(uint delay)
        {
            this.delay = delay;
        }

        public string GetComment()
        {
            string output = "-- Script Type: ";

            switch (type)
            {
                case ScriptType.Emote:
                {
                    if (dataLongSecond == 1)
                    {
                        output += $"Set EmoteState, Emote: {(Emotes)dataLong}";
                    }
                    else
                    {
                        output += $"Play Emote, Emote: {(Emotes)dataLong}";
                    }

                    break;
                }
                case ScriptType.SetField:
                {
                    switch (dataLong)
                    {
                        case 117:
                        {
                            output += $"Set Stand State, State: {(UnitStandStateTypes)dataLongSecond}";
                            break;
                        }
                        case 160:
                        {
                            output += $"Set Sheath State, State: {(SheathStates)dataLongSecond}";
                            break;
                        }
                        default:
                            break;
                    }

                    break;
                }
                case ScriptType.CastSpell:
                {
                    output += DB2.Db2.SpellName.ContainsKey((int)dataLong) ? $"Cast Spell, Name: {DB2.Db2.SpellName[(int)dataLong].Name}" : "Cast Spell, Name: Unknown";
                    break;
                }
                case ScriptType.RemoveAura:
                {
                    output += DB2.Db2.SpellName.ContainsKey((int)dataLong) ? $"Remove Aura, Name: {DB2.Db2.SpellName[(int)dataLong].Name}" : "Remove Aura, Name: Unknown";
                    break;
                }
                case ScriptType.SetAnimKit:
                {
                    if (dataLongSecond == 0)
                    {
                        output += $"Set Ai Anim Kit";
                    }
                    else
                    {
                        output += $"Play One Shot Anim Kit";
                    }

                    break;
                }
                default:
                {
                    output += $"{type}";
                    break;
                }
            }

            return output;
        }
    }
}
