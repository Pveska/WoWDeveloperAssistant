using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class CreatureFlagsAdvisor
    {
        enum NpcFlags : long
        {
            UNIT_NPC_FLAG_NONE                  = 0x00000000,
            UNIT_NPC_FLAG_GOSSIP                = 0x00000001,
            UNIT_NPC_FLAG_QUESTGIVER            = 0x00000002,
            UNIT_NPC_FLAG_UNK1                  = 0x00000004,
            UNIT_NPC_FLAG_UNK2                  = 0x00000008,
            UNIT_NPC_FLAG_TRAINER               = 0x00000010,
            UNIT_NPC_FLAG_TRAINER_CLASS         = 0x00000020,
            UNIT_NPC_FLAG_TRAINER_PROFESSION    = 0x00000040,
            UNIT_NPC_FLAG_VENDOR                = 0x00000080,
            UNIT_NPC_FLAG_VENDOR_AMMO           = 0x00000100,
            UNIT_NPC_FLAG_VENDOR_FOOD           = 0x00000200,
            UNIT_NPC_FLAG_VENDOR_POISON         = 0x00000400,
            UNIT_NPC_FLAG_VENDOR_REAGENT        = 0x00000800,
            UNIT_NPC_FLAG_REPAIR                = 0x00001000,
            UNIT_NPC_FLAG_FLIGHTMASTER          = 0x00002000,
            UNIT_NPC_FLAG_SPIRITHEALER          = 0x00004000,
            UNIT_NPC_FLAG_SPIRITGUIDE           = 0x00008000,
            UNIT_NPC_FLAG_INNKEEPER             = 0x00010000,
            UNIT_NPC_FLAG_BANKER                = 0x00020000,
            UNIT_NPC_FLAG_PETITIONER            = 0x00040000,
            UNIT_NPC_FLAG_TABARDDESIGNER        = 0x00080000,
            UNIT_NPC_FLAG_BATTLEMASTER          = 0x00100000,
            UNIT_NPC_FLAG_AUCTIONEER            = 0x00200000,
            UNIT_NPC_FLAG_STABLEMASTER          = 0x00400000,
            UNIT_NPC_FLAG_GUILD_BANKER          = 0x00800000,
            UNIT_NPC_FLAG_SPELLCLICK            = 0x01000000,
            UNIT_NPC_FLAG_PLAYER_VEHICLE        = 0x02000000,
            UNIT_NPC_FLAG_MAILBOX               = 0x04000000,
            UNIT_NPC_FLAG_ARTIFACT_POWER_RESPEC = 0x08000000,
            UNIT_NPC_FLAG_TRANSMOGRIFIER        = 0x10000000,
            UNIT_NPC_FLAG_VAULTKEEPER           = 0x20000000,
            UNIT_NPC_FLAG_PETBATTLE             = 0x40000000,
            UNIT_NPC_FLAG_BLACK_MARKET          = 0x80000000
        };

        enum NpcFlags2 : long
        {
            UNIT_NPC_FLAG2_NONE                         = 0x00000000,
            UNIT_NPC_FLAG2_ITEM_UPGRADE                 = 0x00000001,
            UNIT_NPC_FLAG2_GARRISON_ARCHITECT           = 0x00000002,
            UNIT_NPC_FLAG2_AI_OBSTACLE                  = 0x00000004,
            UNIT_NPC_FLAG2_STEERING                     = 0x00000008,
            UNIT_NPC_FLAG2_GARRISON_SHIPMENT_CRAFTER    = 0x00000010,
            UNIT_NPC_FLAG2_GARRISON_MISSION_NPC         = 0x00000020,
            UNIT_NPC_FLAG2_TRADESKILL_NPC               = 0x00000040,
            UNIT_NPC_FLAG2_BLACK_MARKET_VIEW            = 0x00000080,
            UNIT_NPC_FLAG2_CONTRIBUTION_COLLECTOR       = 0x00000100
        };

        public enum UnitFlags : long
        {
            UNIT_FLAG_SERVER_CONTROLLED      = 0x00000001,
            UNIT_FLAG_NON_ATTACKABLE         = 0x00000002,
            UNIT_FLAG_REMOVE_CLIENT_CONTROL  = 0x00000004,
            UNIT_FLAG_PVP_ATTACKABLE         = 0x00000008,
            UNIT_FLAG_RENAME                 = 0x00000010,
            UNIT_FLAG_PREPARATION            = 0x00000020,
            UNIT_FLAG_UNK_6                  = 0x00000040,
            UNIT_FLAG_NOT_ATTACKABLE_1       = 0x00000080,
            UNIT_FLAG_IMMUNE_TO_PC           = 0x00000100,
            UNIT_FLAG_IMMUNE_TO_NPC          = 0x00000200,
            UNIT_FLAG_LOOTING                = 0x00000400,
            UNIT_FLAG_PET_IN_COMBAT          = 0x00000800,
            UNIT_FLAG_PVP                    = 0x00001000,
            UNIT_FLAG_SILENCED               = 0x00002000,
            UNIT_FLAG_CANNOT_SWIM            = 0x00004000,
            UNIT_FLAG_CAN_USE_SWIM_ANIMATION = 0x00008000,
            UNIT_FLAG_UNK_16                 = 0x00010000,
            UNIT_FLAG_PACIFIED               = 0x00020000,
            UNIT_FLAG_STUNNED                = 0x00040000,
            UNIT_FLAG_IN_COMBAT              = 0x00080000,
            UNIT_FLAG_TAXI_FLIGHT            = 0x00100000,
            UNIT_FLAG_DISARMED               = 0x00200000,
            UNIT_FLAG_CONFUSED               = 0x00400000,
            UNIT_FLAG_FLEEING                = 0x00800000,
            UNIT_FLAG_PLAYER_CONTROLLED      = 0x01000000,
            UNIT_FLAG_NOT_SELECTABLE         = 0x02000000,
            UNIT_FLAG_SKINNABLE              = 0x04000000,
            UNIT_FLAG_MOUNT                  = 0x08000000,
            UNIT_FLAG_UNK_28                 = 0x10000000,
            UNIT_FLAG_UNK_29                 = 0x20000000,
            UNIT_FLAG_SHEATHE                = 0x40000000,
            UNIT_FLAG_IMMUNE                 = 0x80000000
        };

        enum UnitFlags2 : long
        {
            UNIT_FLAG2_FEIGN_DEATH                  = 0x00000001,
            UNIT_FLAG2_UNK1                         = 0x00000002,
            UNIT_FLAG2_IGNORE_REPUTATION            = 0x00000004,
            UNIT_FLAG2_COMPREHEND_LANG              = 0x00000008,
            UNIT_FLAG2_MIRROR_IMAGE                 = 0x00000010,
            UNIT_FLAG2_INSTANTLY_APPEAR_MODEL       = 0x00000020,
            UNIT_FLAG2_FORCE_MOVEMENT               = 0x00000040,
            UNIT_FLAG2_DISARM_OFFHAND               = 0x00000080,
            UNIT_FLAG2_DISABLE_PRED_STATS           = 0x00000100,
            UNIT_FLAG2_ALLOW_CHANGING_TALENTS       = 0x00000200,
            UNIT_FLAG2_DISARM_RANGED                = 0x00000400,
            UNIT_FLAG2_REGENERATE_POWER             = 0x00000800,
            UNIT_FLAG2_RESTRICT_PARTY_INTERACTION   = 0x00001000,
            UNIT_FLAG2_PREVENT_SPELL_CLICK          = 0x00002000,
            UNIT_FLAG2_ALLOW_ENEMY_INTERACT         = 0x00004000,
            UNIT_FLAG2_DISABLE_TURN                 = 0x00008000,
            UNIT_FLAG2_UNK2                         = 0x00010000,
            UNIT_FLAG2_PLAY_DEATH_ANIM              = 0x00020000,
            UNIT_FLAG2_ALLOW_CHEAT_SPELLS           = 0x00040000,
            UNIT_FLAG2_NO_ACTIONS                   = 0x00080000,
            UNIT_FLAG2_UNK4                         = 0x00100000,
            UNIT_FLAG2_UNK5                         = 0x00200000,
            UNIT_FLAG2_UNK6                         = 0x00400000,
            UNIT_FLAG2_UNK7                         = 0x00800000,
            UNIT_FLAG2_UNK8                         = 0x01000000,
            UNIT_FLAG2_UPDATE_REACTION              = 0x02000000,
            UNIT_FLAG2_SELECTION_DISABLED           = 0x04000000,
            UNIT_FLAG2_UNK11                        = 0x08000000,
            UNIT_FLAG2_UNK12                        = 0x10000000,
            UNIT_FLAG2_UNK13                        = 0x20000000,
            UNIT_FLAG2_UNK14                        = 0x40000000,
            UNIT_FLAG2_UNK15                        = 0x80000000
        };

        enum UnitFlags3 : long
        {
            UNIT_FLAG3_UNK1                         = 0x00000001,
            UNIT_FLAG3_UNK2                         = 0x00000002,
            UNIT_FLAG3_CAN_FIGHT_WITHOUT_DISMOUNT   = 0x00000004,
            UNIT_FLAG3_UNK4                         = 0x00000008,
            UNIT_FLAG3_UNK5                         = 0x00000010,
            UNIT_FLAG3_UNK6                         = 0x00000020,
            UNIT_FLAG3_UNK7                         = 0x00000040,
            UNIT_FLAG3_UNK8                         = 0x00000080,
            UNIT_FLAG3_UNK9                         = 0x00000100,
            UNIT_FLAG3_UNK10                        = 0x00000200,
            UNIT_FLAG3_UNK11                        = 0x00000400,
            UNIT_FLAG3_UNK12                        = 0x00000800,
            UNIT_FLAG3_UNK13                        = 0x00001000,
            UNIT_FLAG3_UNK14                        = 0x00002000,
            UNIT_FLAG3_UNK15                        = 0x00004000,
            UNIT_FLAG3_UNK16                        = 0x00008000,
            UNIT_FLAG3_UNK17                        = 0x00010000,
            UNIT_FLAG3_UNK18                        = 0x00020000,
            UNIT_FLAG3_UNK19                        = 0x00040000,
            UNIT_FLAG3_UNK20                        = 0x00080000,
            UNIT_FLAG3_UNK21                        = 0x00100000,
            UNIT_FLAG3_UNK22                        = 0x00200000,
            UNIT_FLAG3_UNK23                        = 0x00400000,
            UNIT_FLAG3_UNK24                        = 0x00800000,
            UNIT_FLAG3_UNK25                        = 0x01000000,
            UNIT_FLAG3_UNK26                        = 0x02000000,
            UNIT_FLAG3_UNK27                        = 0x04000000,
            UNIT_FLAG3_UNK28                        = 0x08000000,
            UNIT_FLAG3_UNK29                        = 0x10000000,
            UNIT_FLAG3_UNK30                        = 0x20000000,
            UNIT_FLAG3_UNK31                        = 0x40000000,
            UNIT_FLAG3_UNK32                        = 0x80000000
        };

        enum DynamicFlags : long
        {
            UNIT_DYNFLAG_NONE                       = 0x0000,
            UNIT_DYNFLAG_UNK_1                      = 0x0001,
            UNIT_DYNFLAG_HIDE_MODEL                 = 0x0002,
            UNIT_DYNFLAG_LOOTABLE                   = 0x0004,
            UNIT_DYNFLAG_TRACK_UNIT                 = 0x0008,
            UNIT_DYNFLAG_TAPPED                     = 0x0010,
            UNIT_DYNFLAG_SPECIALINFO                = 0x0020,
            UNIT_DYNFLAG_DEAD                       = 0x0040,
            UNIT_DYNFLAG_REFER_A_FRIEND             = 0x0080
        };

        enum FlagsExtra : long
        {
            CREATURE_FLAG_EXTRA_INSTANCE_BIND         = 0x00000001,
            CREATURE_FLAG_EXTRA_CIVILIAN              = 0x00000002,
            CREATURE_FLAG_EXTRA_NO_PARRY              = 0x00000004,
            CREATURE_FLAG_EXTRA_NO_PARRY_HASTEN       = 0x00000008,
            CREATURE_FLAG_EXTRA_NO_BLOCK              = 0x00000010,
            CREATURE_FLAG_EXTRA_NO_CRUSH              = 0x00000020,
            CREATURE_FLAG_EXTRA_NO_XP_AT_KILL         = 0x00000040,
            CREATURE_FLAG_EXTRA_TRIGGER               = 0x00000080,
            CREATURE_FLAG_EXTRA_NO_TAUNT              = 0x00000100,
            CREATURE_FLAG_EXTRA_PERSONAL_LOOT         = 0x00000200,
            CREATURE_FLAG_EXTRA_STAY_AT_KNOCKBACK_POS = 0x00000400,
            CREATURE_FLAG_EXTRA_WORLDEVENT            = 0x00004000,
            CREATURE_FLAG_EXTRA_GUARD                 = 0x00008000,
            CREATURE_FLAG_EXTRA_NO_CRIT               = 0x00020000,
            CREATURE_FLAG_EXTRA_NO_SKILLGAIN          = 0x00040000,
            CREATURE_FLAG_EXTRA_TAUNT_DIMINISH        = 0x00080000,
            CREATURE_FLAG_EXTRA_ALL_DIMINISH          = 0x00100000,
            CREATURE_FLAG_EXTRA_LOG_GROUP_DMG         = 0x00200000,
            CREATURE_FLAG_EXTRA_IMMUNITY_KNOCKBACK    = 0x00400000,
            CREATURE_FLAG_EXTRA_DUNGEON_BOSS          = 0x10000000,
            CREATURE_FLAG_EXTRA_IGNORE_PATHFINDING    = 0x20000000,
            CREATURE_FLAG_EXTRA_DUNGEON_END_BOSS      = 0x40000000,
            CREATURE_FLAG_EXTRA_NO_MOVE_FLAGS_UPDATE  = 0x80000000
        };

        enum TypeFlags : long
        {
            CREATURE_TYPE_FLAG_TAMEABLE_PET                         = 0x00000001,
            CREATURE_TYPE_FLAG_GHOST_VISIBLE                        = 0x00000002,
            CREATURE_TYPE_FLAG_BOSS_MOB                             = 0x00000004,
            CREATURE_TYPE_FLAG_DO_NOT_PLAY_WOUND_PARRY_ANIMATION    = 0x00000008,
            CREATURE_TYPE_FLAG_HIDE_FACTION_TOOLTIP                 = 0x00000010,
            CREATURE_TYPE_FLAG_UNK5                                 = 0x00000020,
            CREATURE_TYPE_FLAG_SPELL_ATTACKABLE                     = 0x00000040,
            CREATURE_TYPE_FLAG_CAN_INTERACT_WHILE_DEAD              = 0x00000080,
            CREATURE_TYPE_FLAG_HERB_SKINNING_SKILL                  = 0x00000100,
            CREATURE_TYPE_FLAG_MINING_SKINNING_SKILL                = 0x00000200,
            CREATURE_TYPE_FLAG_DO_NOT_LOG_DEATH                     = 0x00000400,
            CREATURE_TYPE_FLAG_MOUNTED_COMBAT_ALLOWED               = 0x00000800,
            CREATURE_TYPE_FLAG_CAN_ASSIST                           = 0x00001000,
            CREATURE_TYPE_FLAG_IS_PET_BAR_USED                      = 0x00002000,
            CREATURE_TYPE_FLAG_MASK_UID                             = 0x00004000,
            CREATURE_TYPE_FLAG_ENGINEERING_SKINNING_SKILL           = 0x00008000,
            CREATURE_TYPE_FLAG_EXOTIC_PET                           = 0x00010000,
            CREATURE_TYPE_FLAG_USE_DEFAULT_COLLISION_BOX            = 0x00020000,
            CREATURE_TYPE_FLAG_IS_SIEGE_WEAPON                      = 0x00040000,
            CREATURE_TYPE_FLAG_CAN_COLLIDE_WITH_MISSILES            = 0x00080000,
            CREATURE_TYPE_FLAG_HIDE_NAME_PLATE                      = 0x00100000,
            CREATURE_TYPE_FLAG_DO_NOT_PLAY_MOUNTED_ANIMATIONS       = 0x00200000,
            CREATURE_TYPE_FLAG_IS_LINK_ALL                          = 0x00400000,
            CREATURE_TYPE_FLAG_INTERACT_ONLY_WITH_CREATOR           = 0x00800000,
            CREATURE_TYPE_FLAG_DO_NOT_PLAY_UNIT_EVENT_SOUNDS        = 0x01000000,
            CREATURE_TYPE_FLAG_HAS_NO_SHADOW_BLOB                   = 0x02000000,
            CREATURE_TYPE_FLAG_TREAT_AS_RAID_UNIT                   = 0x04000000,
            CREATURE_TYPE_FLAG_FORCE_GOSSIP                         = 0x08000000,
            CREATURE_TYPE_FLAG_DO_NOT_SHEATHE                       = 0x10000000,
            CREATURE_TYPE_FLAG_DO_NOT_TARGET_ON_INTERACTION         = 0x20000000,
            CREATURE_TYPE_FLAG_DO_NOT_RENDER_OBJECT_NAME            = 0x40000000,
            CREATURE_TYPE_FLAG_UNIT_IS_QUEST_BOSS                   = 0x80000000
        };
        enum TypeFlags2 : long
        {
            CREATURE_TYPEFLAGS_2_UNK1           = 0x00000001,
            CREATURE_TYPEFLAGS_2_UNK2           = 0x00000002,
            CREATURE_TYPEFLAGS_2_UNK3           = 0x00000004,
            CREATURE_TYPEFLAGS_2_UNK4           = 0x00000008,
            CREATURE_TYPEFLAGS_2_UNK5           = 0x00000010,
            CREATURE_TYPEFLAGS_2_UNK6           = 0x00000020,
            CREATURE_TYPEFLAGS_2_UNK7           = 0x00000040,
            CREATURE_TYPEFLAGS_2_UNK8           = 0x00000080,
            CREATURE_TYPEFLAGS_2_UNK9           = 0x00000100,
            CREATURE_TYPEFLAGS_2_UNK10          = 0x00000200,
            CREATURE_TYPEFLAGS_2_UNK11          = 0x00000400,
            CREATURE_TYPEFLAGS_2_UNK12          = 0x00000800,
            CREATURE_TYPEFLAGS_2_UNK13          = 0x00001000
        };

        public static void GetCreatureFlags(string creatureEntry)
        {
            DataSet unitFlagsDs = new DataSet();
            DataSet typeFlagsDs = new DataSet();
            string unitFlagsSqlQuery = "SELECT `npcflag`, `npcflag2`, `unit_flags`, `unit_flags2`, `unit_flags3`, `dynamicflags`, `flags_extra` FROM `creature_template` WHERE `entry` = " + creatureEntry + ";";
            string typeFlagsSqlQuery = "SELECT `TypeFlags`, `TypeFlags2` FROM `creature_template_wdb` WHERE `entry` = " + creatureEntry + ";";
            unitFlagsDs = SQLModule.DatabaseSelectQuery(unitFlagsSqlQuery);
            typeFlagsDs = SQLModule.DatabaseSelectQuery(typeFlagsSqlQuery);
            if (unitFlagsDs == null || typeFlagsDs == null)
                return;

            if (unitFlagsDs.Tables["table"].Rows.Count == 0 || typeFlagsDs.Tables["table"].Rows.Count == 0)
            {
                MessageBox.Show("Creature doesn't exists in your database!");
                return;
            }

            long npcFlags = Convert.ToInt64(unitFlagsDs.Tables["table"].Rows[0][0].ToString());
            long npcFlags2 = Convert.ToInt64(unitFlagsDs.Tables["table"].Rows[0][1].ToString());
            long unitFlags = Convert.ToInt64(unitFlagsDs.Tables["table"].Rows[0][2].ToString());
            long unitFlags2 = Convert.ToInt64(unitFlagsDs.Tables["table"].Rows[0][3].ToString());
            long unitFlags3 = Convert.ToInt64(unitFlagsDs.Tables["table"].Rows[0][4].ToString());
            long dynamicFlags = Convert.ToInt64(unitFlagsDs.Tables["table"].Rows[0][5].ToString());
            long extraFlags = Convert.ToInt64(unitFlagsDs.Tables["table"].Rows[0][6].ToString());
            long typeFlags = Convert.ToInt64(typeFlagsDs.Tables["table"].Rows[0][0].ToString());
            long typeFlags2 = Convert.ToInt64(typeFlagsDs.Tables["table"].Rows[0][1].ToString());

            List<long> npcFlagsList = new List<long>();
            List<long> npcFlags2List = new List<long>();
            List<long> unitFlagsList = new List<long>();
            List<long> unitFlags2List = new List<long>();
            List<long> unitFlags3List = new List<long>();
            List<long> dynamicFlagsList = new List<long>();
            List<long> extraFlagsList = new List<long>();
            List<long> typeFlagsList = new List<long>();
            List<long> typeFlags2List = new List<long>();

            if (npcFlags != 0)
            {
                var flagsArray = Enum.GetValues(typeof(NpcFlags));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (npcFlags - flag >= 0)
                    {
                        npcFlagsList.Add(flag);
                        npcFlags -= flag;
                    }
                }
            }

            if (npcFlags2 != 0)
            {
                var flagsArray = Enum.GetValues(typeof(NpcFlags2));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (npcFlags2 - flag >= 0)
                    {
                        npcFlags2List.Add(flag);
                        npcFlags2 -= flag;
                    }
                }
            }

            if (unitFlags != 0)
            {
                var flagsArray = Enum.GetValues(typeof(UnitFlags));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (unitFlags - flag >= 0)
                    {
                        unitFlagsList.Add(flag);
                        unitFlags -= flag;
                    }
                }
            }

            if (unitFlags2 != 0)
            {
                var flagsArray = Enum.GetValues(typeof(UnitFlags2));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (unitFlags2 - flag >= 0)
                    {
                        unitFlags2List.Add(flag);
                        unitFlags2 -= flag;
                    }
                }
            }

            if (unitFlags3 != 0)
            {
                var flagsArray = Enum.GetValues(typeof(UnitFlags3));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (unitFlags3 - flag >= 0)
                    {
                        unitFlags3List.Add(flag);
                        unitFlags3 -= flag;
                    }
                }
            }

            if (dynamicFlags != 0)
            {
                var flagsArray = Enum.GetValues(typeof(DynamicFlags));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (dynamicFlags - flag >= 0)
                    {
                        dynamicFlagsList.Add(flag);
                        dynamicFlags -= flag;
                    }
                }
            }

            if (extraFlags != 0)
            {
                var flagsArray = Enum.GetValues(typeof(FlagsExtra));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (extraFlags - flag >= 0)
                    {
                        extraFlagsList.Add(flag);
                        extraFlags -= flag;
                    }
                }
            }

            if (typeFlags != 0)
            {
                var flagsArray = Enum.GetValues(typeof(TypeFlags));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (typeFlags - flag >= 0)
                    {
                        typeFlagsList.Add(flag);
                        typeFlags -= flag;
                    }
                }
            }

            if (typeFlags2 != 0)
            {
                var flagsArray = Enum.GetValues(typeof(TypeFlags2));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (typeFlags2 - flag >= 0)
                    {
                        typeFlags2List.Add(flag);
                        typeFlags2 -= flag;
                    }
                }
            }

            npcFlagsList.Sort();
            npcFlags2List.Sort();
            unitFlagsList.Sort();
            unitFlags2List.Sort();
            unitFlags3List.Sort();
            dynamicFlagsList.Sort();
            extraFlagsList.Sort();
            typeFlagsList.Sort();
            typeFlags2List.Sort();

            string outputText = "";

            if (npcFlagsList.Count > 0)
            {
                outputText += "Creature has the following NpcFlags: \r\n";

                outputText = npcFlagsList.Aggregate(outputText, (current, itr) => current + ((NpcFlags) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Creature doesn't have any NpcFlags!\r\n";

            if (npcFlags2List.Count > 0)
            {
                outputText += "Creature has the following NpcFlags2: \r\n";

                outputText = npcFlags2List.Aggregate(outputText, (current, itr) => current + ((NpcFlags2) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Creature doesn't have any NpcFlags2!\r\n";

            if (unitFlagsList.Count > 0)
            {
                outputText += "Creature has the following UnitFlags: \r\n";

                outputText = unitFlagsList.Aggregate(outputText, (current, itr) => current + ((UnitFlags) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Creature doesn't have any UnitFlags!\r\n";

            if (unitFlags2List.Count > 0)
            {
                outputText += "Creature has the following UnitFlags2: \r\n";

                outputText = unitFlags2List.Aggregate(outputText, (current, itr) => current + ((UnitFlags2) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Creature doesn't have any UnitFlags2!\r\n";

            if (unitFlags3List.Count > 0)
            {
                outputText += "Creature has the following UnitFlags3: \r\n";

                outputText = unitFlags3List.Aggregate(outputText, (current, itr) => current + ((UnitFlags3) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Creature doesn't have any UnitFlags3!\r\n";

            if (dynamicFlagsList.Count > 0)
            {
                outputText += "Creature has the following DynamicFlags: \r\n";

                outputText = dynamicFlagsList.Aggregate(outputText, (current, itr) => current + ((DynamicFlags) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Creature doesn't have any DynamicFlags!\r\n";

            if (extraFlagsList.Count > 0)
            {
                outputText += "Creature has the following ExtraFlags: \r\n";

                outputText = extraFlagsList.Aggregate(outputText, (current, itr) => current + ((FlagsExtra) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Creature doesn't have any ExtraFlags!\r\n";

            if (typeFlagsList.Count > 0)
            {
                outputText += "Creature has the following TypeFlags: \r\n";

                outputText = typeFlagsList.Aggregate(outputText, (current, itr) => current + ((TypeFlags) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Creature doesn't have any TypeFlags!\r\n";

            if (typeFlags2List.Count > 0)
            {
                outputText += "Creature has the following TypeFlags2: \r\n";

                outputText = typeFlags2List.Aggregate(outputText, (current, itr) => current + ((TypeFlags2) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Creature doesn't have any TypeFlags2!\r\n";

            MessageBox.Show(outputText);
        }
    }
}
