using System;
using System.Collections.Generic;

namespace WoWDeveloperAssistant.Conditions_Creator
{
    public static class Conditions
    {
        public enum ConditionSourceTypes : uint
        {
            SOURCE_CREATURE_LOOT_TEMPLATE      = 1,
            SOURCE_DISENCHANT_LOOT_TEMPLATE    = 2,
            SOURCE_FISHING_LOOT_TEMPLATE       = 3,
            SOURCE_GAMEOBJECT_LOOT_TEMPLATE    = 4,
            SOURCE_ITEM_LOOT_TEMPLATE          = 5,
            SOURCE_MAIL_LOOT_TEMPLATE          = 6,
            SOURCE_MILLING_LOOT_TEMPLATE       = 7,
            SOURCE_PICKPOCKETING_LOOT_TEMPLATE = 8,
            SOURCE_PROSPECTING_LOOT_TEMPLATE   = 9,
            SOURCE_REFERENCE_LOOT_TEMPLATE     = 10,
            SOURCE_SKINNING_LOOT_TEMPLATE      = 11,
            SOURCE_SPELL_LOOT_TEMPLATE         = 12,
            SOURCE_SPELL_IMPLICIT_TARGET       = 13,
            SOURCE_GOSSIP_MENU                 = 14,
            SOURCE_GOSSIP_MENU_OPTION          = 15,
            SOURCE_CREATURE_TEMPLATE_VEHICLE   = 16,
            SOURCE_SPELL                       = 17,
            SOURCE_SPELL_CLICK_EVENT           = 18,
            SOURCE_QUEST_ACCEPT                = 19,
            SOURCE_VEHICLE_SPELL               = 21,
            SOURCE_SMART_EVENT                 = 22,
            SOURCE_NPC_VENDOR                  = 23,
            SOURCE_SPELL_PROC                  = 24,
            SOURCE_TERRAIN_SWAP                = 25,
            SOURCE_PHASE                       = 26,
            SOURCE_QUEST_SHOW_MARK             = 1000,
            SOURCE_AREATRIGGER_ACTION          = 1002,
            SOURCE_LOOT_ITEM                   = 1003,
            SOURCE_PLAYERCHOICE_RESPONSE       = 1004,
            SOURCE_GAMEOBJECT_INTERACT         = 1005,
            SOURCE_GARRISON_MISSION            = 1006,
            SOURCE_NPC_TRADESKILL              = 1007
        };

        public enum ConditionTypes : uint
        {
            CONDITION_AURA                     = 1,
            CONDITION_ITEM                     = 2,
            CONDITION_ITEM_EQUIPPED            = 3,
            CONDITION_ZONEID                   = 4,
            CONDITION_REPUTATION_RANK          = 5,
            CONDITION_TEAM                     = 6,
            CONDITION_SKILL                    = 7,
            CONDITION_QUESTREWARDED            = 8,
            CONDITION_QUESTTAKEN               = 9,
            CONDITION_INEBRIATIONSTATE         = 10,
            CONDITION_WORLD_STATE              = 11,
            CONDITION_ACTIVE_EVENT             = 12,
            CONDITION_INSTANCE_DATA            = 13,
            CONDITION_QUEST_NONE               = 14,
            CONDITION_CLASS                    = 15,
            CONDITION_RACE                     = 16,
            CONDITION_ACHIEVEMENT              = 17,
            CONDITION_TITLE                    = 18,
            CONDITION_SPAWNMASK                = 19,
            CONDITION_GENDER                   = 20,
            CONDITION_UNUSED_21                = 21,
            CONDITION_MAPID                    = 22,
            CONDITION_AREAID                   = 23,
            CONDITION_UNUSED_24                = 24,
            CONDITION_SPELL                    = 25,
            CONDITION_PHASEMASK                = 26,
            CONDITION_LEVEL                    = 27,
            CONDITION_QUEST_COMPLETE           = 28,
            CONDITION_NEAR_CREATURE            = 29,
            CONDITION_NEAR_GAMEOBJECT          = 30,
            CONDITION_OBJECT_ENTRY             = 31,
            CONDITION_TYPE_MASK                = 32,
            CONDITION_RELATION_TO              = 33,
            CONDITION_REACTION_TO              = 34,
            CONDITION_DISTANCE_TO              = 35,
            CONDITION_ALIVE                    = 36,
            CONDITION_HP_VAL                   = 37,
            CONDITION_HP_PCT                   = 38,
            CONDITION_HAS_BUILDING_TYPE        = 39,
            CONDITION_HAS_GARRISON_LEVEL       = 40,
            CONDITION_IN_WATER                 = 41,
            CONDITION_HAS_KNOWLEDGE_LEVEL      = 42,
            CONDITION_QUESTSTATE               = 47,
            CONDITION_QUEST_OBJECTIVE_COMPLETE = 48,
            CONDITION_CONTRIBUTION             = 49,
            CONDITION_HAS_ORDER_HALL_TALENT    = 100,
            CONDITION_FACTION                  = 101,
            CONDITION_IN_COMBAT                = 102,
            CONDITION_LIQUID_TYPE              = 103,
            CONDITION_WORLD_QUEST_ACTIVE       = 104,
            CONDITION_INACCESSIBLE_LIST        = 105,
            CONDITION_ON_VEHICLE               = 106,
            CONDITION_CRITERIA_VALUE           = 107,
            CONDITION_ESSENCE                  = 108
        };

        public static Dictionary<string, Dictionary<string, bool>> textBoxAccessibilityDictionary = new Dictionary<string, Dictionary<string, bool>>
        {
            { "SOURCE_CREATURE_LOOT_TEMPLATE"     , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_DISENCHANT_LOOT_TEMPLATE"   , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_FISHING_LOOT_TEMPLATE"      , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_GAMEOBJECT_LOOT_TEMPLATE"   , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_ITEM_LOOT_TEMPLATE"         , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_MAIL_LOOT_TEMPLATE"         , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_MILLING_LOOT_TEMPLATE"      , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_PICKPOCKETING_LOOT_TEMPLATE", CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_PROSPECTING_LOOT_TEMPLATE"  , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_REFERENCE_LOOT_TEMPLATE"    , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_SKINNING_LOOT_TEMPLATE"     , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_SPELL_LOOT_TEMPLATE"        , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_SPELL_IMPLICIT_TARGET"      , CreateSourceTextBoxDictionary(true, true, false, true)    },
            { "SOURCE_GOSSIP_MENU"                , CreateSourceTextBoxDictionary(true, true, false, true)    },
            { "SOURCE_GOSSIP_MENU_OPTION"         , CreateSourceTextBoxDictionary(true, true, false, true)    },
            { "SOURCE_CREATURE_TEMPLATE_VEHICLE"  , CreateSourceTextBoxDictionary(false, true, false, true)   },
            { "SOURCE_SPELL"                      , CreateSourceTextBoxDictionary(false, true, false, true)   },
            { "SOURCE_SPELL_CLICK_EVENT"          , CreateSourceTextBoxDictionary(true, true, false, true)    },
            { "SOURCE_QUEST_ACCEPT"               , CreateSourceTextBoxDictionary(false, true, false, false)  },
            { "SOURCE_VEHICLE_SPELL"              , CreateSourceTextBoxDictionary(true, true, false, true)    },
            { "SOURCE_SMART_EVENT"                , CreateSourceTextBoxDictionary(true, true, true, true)     },
            { "SOURCE_NPC_VENDOR"                 , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_SPELL_PROC"                 , CreateSourceTextBoxDictionary(false, true, false, true)   },
            { "SOURCE_TERRAIN_SWAP"               , CreateSourceTextBoxDictionary(false, true, false, false)  },
            { "SOURCE_PHASE"                      , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_QUEST_SHOW_MARK"            , CreateSourceTextBoxDictionary(false, true, false, false)  },
            { "SOURCE_AREATRIGGER_ACTION"         , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_LOOT_ITEM"                  , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "SOURCE_PLAYERCHOICE_RESPONSE"      , CreateSourceTextBoxDictionary(false, true, false, false)  },
            { "SOURCE_GAMEOBJECT_INTERACT"        , CreateSourceTextBoxDictionary(false, true, false, false)  },
            { "SOURCE_GARRISON_MISSION"           , CreateSourceTextBoxDictionary(false, true, false, false)  },
            { "SOURCE_NPC_TRADESKILL"             , CreateSourceTextBoxDictionary(true, true, false, false)   },
            { "CONDITION_AURA"                    , CreateConditionTextBoxDictionary(true, true, true)        },
            { "CONDITION_ITEM"                    , CreateConditionTextBoxDictionary(true, true, true)        },
            { "CONDITION_ITEM_EQUIPPED"           , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_ZONEID"                  , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_REPUTATION_RANK"         , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_TEAM"                    , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_SKILL"                   , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_QUESTREWARDED"           , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_QUESTTAKEN"              , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_INEBRIATIONSTATE"        , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_WORLD_STATE"             , CreateConditionTextBoxDictionary(true, true, true)        },
            { "CONDITION_ACTIVE_EVENT"            , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_INSTANCE_DATA"           , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_QUEST_NONE"              , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_CLASS"                   , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_RACE"                    , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_ACHIEVEMENT"             , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_TITLE"                   , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_SPAWNMASK"               , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_GENDER"                  , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_UNUSED_21"               , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_MAPID"                   , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_AREAID"                  , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_UNUSED_24"               , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_SPELL"                   , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_PHASEMASK"               , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_LEVEL"                   , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_QUEST_COMPLETE"          , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_NEAR_CREATURE"           , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_NEAR_GAMEOBJECT"         , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_OBJECT_ENTRY"            , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_TYPE_MASK"               , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_RELATION_TO"             , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_REACTION_TO"             , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_DISTANCE_TO"             , CreateConditionTextBoxDictionary(true, true, true)        },
            { "CONDITION_ALIVE"                   , CreateConditionTextBoxDictionary(false, false, false)     },
            { "CONDITION_HP_VAL"                  , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_HP_PCT"                  , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_HAS_BUILDING_TYPE"       , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_HAS_GARRISON_LEVEL"      , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_IN_WATER"                , CreateConditionTextBoxDictionary(false, false, false)     },
            { "CONDITION_HAS_KNOWLEDGE_LEVEL"     , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_QUESTSTATE"              , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_QUEST_OBJECTIVE_COMPLETE", CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_CONTRIBUTION"            , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_HAS_ORDER_HALL_TALENT"   , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_FACTION"                 , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_IN_COMBAT"               , CreateConditionTextBoxDictionary(false, false, false)     },
            { "CONDITION_LIQUID_TYPE"             , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_WORLD_QUEST_ACTIVE"      , CreateConditionTextBoxDictionary(true, false, false)      },
            { "CONDITION_INACCESSIBLE_LIST"       , CreateConditionTextBoxDictionary(false, false, false)     },
            { "CONDITION_ON_VEHICLE"              , CreateConditionTextBoxDictionary(true, true, false)       },
            { "CONDITION_CRITERIA_VALUE"          , CreateConditionTextBoxDictionary(true, true, true)        },
            { "CONDITION_ESSENCE"                 , CreateConditionTextBoxDictionary(true, true, false)       }
        };

        public static Dictionary<ConditionSourceTypes, string> sourceTypeCommentsDictionary = new Dictionary<ConditionSourceTypes, string>
        {
            { ConditionSourceTypes.SOURCE_CREATURE_LOOT_TEMPLATE     , "Creature @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_DISENCHANT_LOOT_TEMPLATE   , "Disenchant @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_FISHING_LOOT_TEMPLATE      , "Fishing @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_GAMEOBJECT_LOOT_TEMPLATE   , "Gameobject @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_ITEM_LOOT_TEMPLATE         , "Item @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_MAIL_LOOT_TEMPLATE         , "Mail @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_MILLING_LOOT_TEMPLATE      , "Milling @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_PICKPOCKETING_LOOT_TEMPLATE, "Pickpocketing @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_PROSPECTING_LOOT_TEMPLATE  , "Prospecting @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_REFERENCE_LOOT_TEMPLATE    , "Reference @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_SKINNING_LOOT_TEMPLATE     , "Skinning @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_SPELL_LOOT_TEMPLATE        , "Spell @sourceGroup have item @sourceEntry in loot if player" },
            { ConditionSourceTypes.SOURCE_SPELL_IMPLICIT_TARGET      , "Make target implicit for spell @sourceEntry if @target/@caster" },
            { ConditionSourceTypes.SOURCE_GOSSIP_MENU                , "Show gossip menu if @player/@object" },
            { ConditionSourceTypes.SOURCE_GOSSIP_MENU_OPTION         , "Show gossip menu option if @player/@object" },
            { ConditionSourceTypes.SOURCE_CREATURE_TEMPLATE_VEHICLE  , "Remove player from vehicle if @player/@vehicle" },
            { ConditionSourceTypes.SOURCE_SPELL                      , "Spell @sourceEntry can be casted if @caster/@target" },
            { ConditionSourceTypes.SOURCE_SPELL_CLICK_EVENT          , "Player can use spellclick if @he/@target" },
            { ConditionSourceTypes.SOURCE_QUEST_ACCEPT               , "Quest @sourceEntry available if player" },
            { ConditionSourceTypes.SOURCE_VEHICLE_SPELL              , "Show vehicle spell @sourceEntry if @player/@vehicle" },
            { ConditionSourceTypes.SOURCE_SMART_EVENT                , "Smart script occurs if @invoker/@object" },
            { ConditionSourceTypes.SOURCE_NPC_VENDOR                 , "Vendor @sourceGroup have item @sourceEntry in list if player" },
            { ConditionSourceTypes.SOURCE_SPELL_PROC                 , "Proc occurs if @actor/target" },
            { ConditionSourceTypes.SOURCE_TERRAIN_SWAP               , "Terrain swap applies if player" },
            { ConditionSourceTypes.SOURCE_PHASE                      , "Phase @sourceGroup in zone @sourceEntry only shows if player" },
            { ConditionSourceTypes.SOURCE_QUEST_SHOW_MARK            , "Leave comment for this condition here" },
            { ConditionSourceTypes.SOURCE_AREATRIGGER_ACTION         , "Leave comment for this condition here" },
            { ConditionSourceTypes.SOURCE_LOOT_ITEM                  , "Leave comment for this condition here" },
            { ConditionSourceTypes.SOURCE_PLAYERCHOICE_RESPONSE      , "Leave comment for this condition here" },
            { ConditionSourceTypes.SOURCE_GAMEOBJECT_INTERACT        , "Leave comment for this condition here" },
            { ConditionSourceTypes.SOURCE_GARRISON_MISSION           , "Leave comment for this condition here" },
            { ConditionSourceTypes.SOURCE_NPC_TRADESKILL             , "Leave comment for this condition here" },
        };

        public static Dictionary<ConditionTypes, string> conditionTypeCommentsDictionary = new Dictionary<ConditionTypes, string>
        {
            { ConditionTypes.CONDITION_AURA                    , "have aura @conditionValue1" },
            { ConditionTypes.CONDITION_ITEM                    , "have item @conditionValue1 in quantity @conditionValue2" },
            { ConditionTypes.CONDITION_ITEM_EQUIPPED           , "equipped item @conditionValue1" },
            { ConditionTypes.CONDITION_ZONEID                  , "in zone with id @conditionValue1" },
            { ConditionTypes.CONDITION_REPUTATION_RANK         , "have reputation rank @conditionValue2 with faction @conditionValue1" },
            { ConditionTypes.CONDITION_TEAM                    , "team id @conditionValue1" },
            { ConditionTypes.CONDITION_SKILL                   , "have rank @conditionValue2 for skill @conditionValue1" },
            { ConditionTypes.CONDITION_QUESTREWARDED           , "rewarded quest @conditionValue1" },
            { ConditionTypes.CONDITION_QUESTTAKEN              , "taken quest @conditionValue1" },
            { ConditionTypes.CONDITION_INEBRIATIONSTATE        , "have drunken state @conditionValue1" },
            { ConditionTypes.CONDITION_WORLD_STATE             , "located in world with world state @conditionValue1 with value @conditionValue2" },
            { ConditionTypes.CONDITION_ACTIVE_EVENT            , "located in world with active event @conditionValue1" },
            { ConditionTypes.CONDITION_INSTANCE_DATA           , "located in instance with entry @conditionValue1 and data @conditionValue2" },
            { ConditionTypes.CONDITION_QUEST_NONE              , "have not quest @conditionValue1" },
            { ConditionTypes.CONDITION_CLASS                   , "have class @conditionValue1" },
            { ConditionTypes.CONDITION_RACE                    , "have race @conditionValue1" },
            { ConditionTypes.CONDITION_ACHIEVEMENT             , "have achievement @conditionValue1" },
            { ConditionTypes.CONDITION_TITLE                   , "have title @conditionValue1" },
            { ConditionTypes.CONDITION_SPAWNMASK               , "have spawnmask @conditionValue1" },
            { ConditionTypes.CONDITION_GENDER                  , "have gender @conditionValue1" },
            { ConditionTypes.CONDITION_UNUSED_21               , "" },
            { ConditionTypes.CONDITION_MAPID                   , "in map with id @conditionValue1" },
            { ConditionTypes.CONDITION_AREAID                  , "in area with id @conditionValue1" },
            { ConditionTypes.CONDITION_UNUSED_24               , "" },
            { ConditionTypes.CONDITION_SPELL                   , "know spell @conditionValue1" },
            { ConditionTypes.CONDITION_PHASEMASK               , "have phasemask @conditionValue1" },
            { ConditionTypes.CONDITION_LEVEL                   , "level have state @conditionValue2 with value @conditionValue1" },
            { ConditionTypes.CONDITION_QUEST_COMPLETE          , "completed quest @conditionValue1" },
            { ConditionTypes.CONDITION_NEAR_CREATURE           , "have creature with entry @conditionValue1 within @conditionValue2 yards" },
            { ConditionTypes.CONDITION_NEAR_GAMEOBJECT         , "have gameobject with entry @conditionValue1 within @conditionValue2 yards" },
            { ConditionTypes.CONDITION_OBJECT_ENTRY            , "have entry or guid with value @conditionValue1" },
            { ConditionTypes.CONDITION_TYPE_MASK               , "have type mask @conditionValue1" },
            { ConditionTypes.CONDITION_RELATION_TO             , "have relation to target @conditionValue1 and type @conditionValue2" },
            { ConditionTypes.CONDITION_REACTION_TO             , "have reaction to target @conditionValue1 with rank @conditionValue2" },
            { ConditionTypes.CONDITION_DISTANCE_TO             , "have distance @conditionValue2 yards to target @conditionValue1 with comparision type @conditionValue3" },
            { ConditionTypes.CONDITION_ALIVE                   , "alive" },
            { ConditionTypes.CONDITION_HP_VAL                  , "have hp value @conditionValue1 with comparision type @conditionValue2" },
            { ConditionTypes.CONDITION_HP_PCT                  , "have hp pct @conditionValue1 with comparision type @conditionValue2" },
            { ConditionTypes.CONDITION_HAS_BUILDING_TYPE       , "have activeted building @conditionValue1 in garrison" },
            { ConditionTypes.CONDITION_HAS_GARRISON_LEVEL      , "have garrison level @conditionValue1" },
            { ConditionTypes.CONDITION_IN_WATER                , "in water" },
            { ConditionTypes.CONDITION_HAS_KNOWLEDGE_LEVEL     , "have artifact knowledge level @conditionValue1" },
            { ConditionTypes.CONDITION_QUESTSTATE              , "have quest @conditionValue1 on state @conditionValue2" },
            { ConditionTypes.CONDITION_QUEST_OBJECTIVE_COMPLETE, "complete objective @conditionValue1" },
            { ConditionTypes.CONDITION_CONTRIBUTION            , "have contribution @conditionValue1 in state @conditionValue2" },
            { ConditionTypes.CONDITION_HAS_ORDER_HALL_TALENT   , "have order hall talent @conditionValue1" },
            { ConditionTypes.CONDITION_FACTION                 , "have faction @conditionValue1" },
            { ConditionTypes.CONDITION_IN_COMBAT               , "in combat" },
            { ConditionTypes.CONDITION_LIQUID_TYPE             , "in liquid type @conditionValue1" },
            { ConditionTypes.CONDITION_WORLD_QUEST_ACTIVE      , "have world quest @conditionValue1 active" },
            { ConditionTypes.CONDITION_INACCESSIBLE_LIST       , "have creature target in inaccessible list" },
            { ConditionTypes.CONDITION_ON_VEHICLE              , "is on vehicle" },
            { ConditionTypes.CONDITION_CRITERIA_VALUE          , "have criteria @conditionValue1 value @conditionValue2 with comparision type @conditionValue3" },
            { ConditionTypes.CONDITION_ESSENCE                 , "have essence with id @conditionValue1 on rank @conditionValue2" }
        };

        public static Dictionary<string, bool> CreateSourceTextBoxDictionary(bool sourceGroup, bool sourceEntry, bool sourceId, bool conditionTarget)
        {
            Dictionary<string, bool> textBoxDictionary = new Dictionary<string, bool>();
            textBoxDictionary.Add("textBox_SourceGroup", sourceGroup);
            textBoxDictionary.Add("textBox_SourceEntry", sourceEntry);
            textBoxDictionary.Add("textBox_SourceId", sourceId);
            textBoxDictionary.Add("textBox_ConditionTarget", conditionTarget);
            return textBoxDictionary;
        }

        public static Dictionary<string, bool> CreateConditionTextBoxDictionary(bool conditionValue1, bool conditionValue2, bool conditionValue3)
        {
            Dictionary<string, bool> textBoxDictionary = new Dictionary<string, bool>();
            textBoxDictionary.Add("textBox_ConditionValue1", conditionValue1);
            textBoxDictionary.Add("textBox_ConditionValue2", conditionValue2);
            textBoxDictionary.Add("textBox_ConditionValue3", conditionValue3);
            return textBoxDictionary;
        }

        public struct Condition
        {
            public uint sourceType;
            public string sourceGroup;
            public string sourceEntry;
            public string sourceId;
            public string elseGroup;
            public uint conditionType;
            public string conditionTarget;
            public string conditionValue1;
            public string conditionValue2;
            public string conditionValue3;
            public string negativeCondition;
            public string scriptName;

            public Condition(MainForm mainForm)
            {
                sourceType = (uint)Enum.Parse(typeof(Conditions.ConditionSourceTypes), mainForm.comboBox_ConditionSourceType.SelectedItem.ToString());
                sourceGroup = mainForm.textBox_SourceGroup.Text != "" ? mainForm.textBox_SourceGroup.Text : "0";
                sourceEntry = mainForm.textBox_SourceEntry.Text != "" ? mainForm.textBox_SourceEntry.Text : "0";
                sourceId = mainForm.textBox_SourceId.Text != "" ? mainForm.textBox_SourceId.Text : "0";
                elseGroup = mainForm.textBox_ElseGroup.Text != "" ? mainForm.textBox_ElseGroup.Text : "0";
                conditionType = (uint)Enum.Parse(typeof(Conditions.ConditionTypes), mainForm.comboBox_ConditionType.SelectedItem.ToString());
                conditionTarget = mainForm.textBox_ConditionTarget.Text != "" ? mainForm.textBox_ConditionTarget.Text : "0";
                conditionValue1 = mainForm.textBox_ConditionValue1.Text != "" ? mainForm.textBox_ConditionValue1.Text : "0";
                conditionValue2 = mainForm.textBox_ConditionValue2.Text != "" ? mainForm.textBox_ConditionValue2.Text : "0";
                conditionValue3 = mainForm.textBox_ConditionValue3.Text != "" ? mainForm.textBox_ConditionValue3.Text : "0";
                negativeCondition = mainForm.textBox_NegativeCondition.Text != "" ? mainForm.textBox_NegativeCondition.Text : "0";
                scriptName = mainForm.textBox_ScriptName.Text;
            }
        }
    }
}
