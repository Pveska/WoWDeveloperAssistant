using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class QuestFlagsAdvisor
    {
        enum QuestFlags : long
        {
            QUESTER_MUST_NOT_DIE                                    = 0x00000001,
            START_EVENT_MUST_COMPLETE                               = 0x00000002,
            QUEST_COMPLETION_AREATRIGGER                            = 0x00000004,
            PUSHABLE                                                = 0x00000008,
            QUES_HAS_CONDITION                                      = 0x00000010,
            HIDE_REWARD_POI                                         = 0x00000020,
            RAID_GROUP                                              = 0x00000040,
            WAR_MODE_REWARDS_OPT_IN                                 = 0x00000080,
            NO_MONEY_FOR_XP                                         = 0x00000100,
            HIDE_REWARD                                             = 0x00000200,
            TRACKING_EVENT_NOT_A_QUEST                              = 0x00000400,
            DEPRECATE_REPUTATION                                    = 0x00000800,
            DAILY_QUEST                                             = 0x00001000,
            QUEST_FLAGS_PVP                                         = 0x00002000,
            DEPRECATED                                              = 0x00004000,
            WEEKLY_QUEST                                            = 0x00008000,
            AUTO_COMPLETE                                           = 0x00010000,
            DISPLAY_ITEM_IN_TRACKER                                 = 0x00020000,
            DISABLE_COMPLETION_TEXT                                 = 0x00040000,
            AUTO_ACCEPT                                             = 0x00080000,
            ACCEPT_SPELL_PLAYER_CAST                                = 0x00100000,
            COMPLETE_SPELL_PLAYER_CAST                              = 0x00200000,
            UPDATE_PHASE_SHIFT                                      = 0x00400000,
            SCROLL_OF_RESURRECTIOB_WHITELIS                         = 0x00800000,
            GOSSIP_ON_QUEST_COMPLETION_FORCE_GOSSIP                 = 0x01000000,
            REMOVE_EXTRA_GET_ITEMS                                  = 0x02000000,
            WELL_KNOWN                                              = 0x04000000,
            PORTRAIT_FROM_LOG                                       = 0x08000000,
            SHOW_ITEM_WHEN_COMPLETED                                = 0x10000000,
            GOSSIP_ON_QUEST_ACCEPT_FORCE_GOSSIP                     = 0x20000000,
            ITEMS_GLOW_WHEN_DONE                                    = 0x40000000,
            FAIL_ON_LOGOUT                                          = 0x80000000
        };

        enum QuestFlagsEx : long
        {
            KEEP_ADDITIONAL_ITEMS                                   = 0x00000001,
            GOSSIP_ON_QUEST_COMPLETION_SUPPRESS_GOSSIP              = 0x00000002,
            GOSSIP_ON_QUEST_ACCEPT_SUPPRESS_GOSSIP                  = 0x00000004,
            DISALLOW_PLAYER_AS_QUESTGIVER                           = 0x00000008,
            CHOICE_REWARD_FILTER_MATCHES_CLASS                      = 0x00000010,
            CHOICE_REWARD_FILTER_MATCHES_SPEC                       = 0x00000020,
            REMOVE_FROM_LOG_ON_PERIDOIC_RESET                       = 0x00000040,
            ACCOUNT_LEVEL_QUEST                                     = 0x00000080,
            LEGENDARY_QUEST                                         = 0x00000100,
            NO_GUILD_XP                                             = 0x00000200,
            RESET_CACHE_ON_ACCEPT                                   = 0x00000400,
            NO_ABANDON_ONCE_ANY_OBJECTIVE_COMPLETE                  = 0x00000800,
            RECAST_ACCEPT_SPELL_ON_LOGIN                            = 0x00001000,
            UPDATE_ZONE_AURAS                                       = 0x00002000,
            NO_CREDIT_FOR_PROXY_CREATURES                           = 0x00004000,
            DISPLAY_AS_DAILY_QUEST                                  = 0x00008000,
            PART_OF_QUEST_LINE                                      = 0x00010000,
            QUEST_FOR_INTERNAL_BUILDS_ONLY                          = 0x00020000,
            SUPPRESS_SPELL_LEARN_TEXT_LINE_FOR_FOLLOWERS            = 0x00040000,
            DISPLAY_HEADER_AS_OBJECTIVE_FOR_TASKS                   = 0x00080000,
            GARRISON_NON_OWNERS_ALLOWED                             = 0x00100000,
            REMOVE_QUEST_ON_WEEKLY_RESET                            = 0x00200000,
            SUPPRESS_FAREWELL_AUDIO_AFTER_QUEST_ACCEPT              = 0x00400000,
            REWARDS_BYPASS_WEEKLY_CAPS_AND_SEASON_TOTAL             = 0x00800000,
            IS_A_WORLD_QUEST                                        = 0x01000000,
            NOT_IGNORABLE                                           = 0x02000000,
            AUTO_PUSH                                               = 0x04000000,
            NO_COMPLETE_QUEST_SPELL_EFFECTS                         = 0x08000000,
            DO_NOT_TOAST_HONOR_REWARD                               = 0x10000000,
            KEEP_REPEATABLE_QUEST_ON_FACTION_CHANGE                 = 0x20000000,
            KEEP_PROGRESS_ON_FACTION_CHANGE                         = 0x40000000,
            PUSH_TEAM_QUEST_USING_MAP_CONTROLLER                    = 0x80000000
        };

        enum QuestFlagsEx2 : long
        {
            RESET_ON_GAME_MILESTONE                                 = 0x00000001,
            NO_WAR_MODE_BONUS                                       = 0x00000002,
            AWARD_HIGHEST_PROFESSION                                = 0x00000004,
            NOT_REPLAYABLE                                          = 0x00000008,
            NO_REPLAY_REWARDS                                       = 0x00000010,
            DISABLE_WAYPOINT_PATHING                                = 0x00000020,
            RESET_ON_MYTHIC_PLUS_SEASON                             = 0x00000040,
            RESET_ON_PVP_SEASON                                     = 0x00000080,
            ENABLE_OVERRIDE_SORT_ORDER                              = 0x00000100,
            FORCE_STARTING_LOC_ON_ZONE_MAP                          = 0x00000200,
            BONUS_LOOT_NEVER                                        = 0x00000400,
            BONUS_LOOT_ALWAYS                                       = 0x00000800,
            HIDE_TASK_ON_MAIN_MAP                                   = 0x00001000,
            HIDE_TASK_IN_TRACKER                                    = 0x00002000,
            SKIP_DISABLED_CHECK                                     = 0x00004000,
            ENFORCE_MAXIMUM_QUEST_LEVEL                             = 0x00008000,
            HIDE_REQUIRED_ITEMS_ON_TURN_IN                          = 0x00010000,
        };

        enum QuestSpecialFlags : long
        {
            QUEST_SPECIAL_FLAGS_NONE                  = 0x000,
            QUEST_SPECIAL_FLAGS_REPEATABLE            = 0x001,
            QUEST_SPECIAL_FLAGS_EXPLORATION_OR_EVENT  = 0x002,
            QUEST_SPECIAL_FLAGS_AUTO_ACCEPT           = 0x004,
            QUEST_SPECIAL_FLAGS_DF_QUEST              = 0x008,
            QUEST_SPECIAL_FLAGS_MONTHLY               = 0x010,
            QUEST_SPECIAL_FLAGS_CAST                  = 0x020,
            QUEST_SPECIAL_FLAGS_DYNAMIC_ITEM_REWARD   = 0x040,
            QUEST_SPECIAL_FLAGS_AUTO_COMPLETE         = 0x080,
            QUEST_SPECIAL_FLAGS_SKIP_EMPTY_OBJECTIVES = 0x0100,
            QUEST_SPECIAL_FLAGS_DB_ALLOWED            = QUEST_SPECIAL_FLAGS_REPEATABLE | QUEST_SPECIAL_FLAGS_EXPLORATION_OR_EVENT | QUEST_SPECIAL_FLAGS_AUTO_ACCEPT | QUEST_SPECIAL_FLAGS_DF_QUEST | QUEST_SPECIAL_FLAGS_MONTHLY | QUEST_SPECIAL_FLAGS_DYNAMIC_ITEM_REWARD | QUEST_SPECIAL_FLAGS_AUTO_COMPLETE | QUEST_SPECIAL_FLAGS_SKIP_EMPTY_OBJECTIVES,
            QUEST_SPECIAL_FLAGS_DELIVER               = 0x0200,
            QUEST_SPECIAL_FLAGS_SPEAKTO               = 0x0400,
            QUEST_SPECIAL_FLAGS_KILL                  = 0x0800,
            QUEST_SPECIAL_FLAGS_TIMED                 = 0x1000,
            QUEST_SPECIAL_FLAGS_PLAYER_KILL           = 0x2000,
        };

        public static void GetQuestFlags(string questEntry)
        {
            string questFlagsSqlQuery = "SELECT `Flags`, `FlagsEx`, `FlagsEx2` FROM `quest_template` WHERE `Id` = " + questEntry + ";";
            var questFlagsDs = SQLModule.WorldSelectQuery(questFlagsSqlQuery);
            if (questFlagsDs == null)
                return;

            questFlagsSqlQuery = "SELECT `SpecialFlags` FROM `quest_template_addon` WHERE `Id` = " + questEntry + ";";
            DataSet AddonQuestFlagsDs = SQLModule.WorldSelectQuery(questFlagsSqlQuery);
            if (AddonQuestFlagsDs == null)
                return;

            if (questFlagsDs.Tables["table"].Rows.Count == 0)
            {
                MessageBox.Show("Quest doesn't exists in your database!");
                return;
            }

            long specialFlags = 0;
            if (questFlagsDs.Tables["table"].Rows.Count == 0)
                specialFlags = Convert.ToInt64(AddonQuestFlagsDs.Tables["table"].Rows[0][0].ToString());

            long questFlags = Convert.ToInt64(questFlagsDs.Tables["table"].Rows[0][0].ToString());
            long questFlagsEx = Convert.ToInt64(questFlagsDs.Tables["table"].Rows[0][1].ToString());
            long questFlagsEx2 = Convert.ToInt64(questFlagsDs.Tables["table"].Rows[0][2].ToString());

            List<long> questFlagsList = new List<long>();
            List<long> questFlagsExList = new List<long>();
            List<long> questFlagsEx2List = new List<long>();
            List<long> specialFlagsList = new List<long>();

            if (questFlags != 0)
            {
                var flagsArray = Enum.GetValues(typeof(QuestFlags));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (questFlags - flag >= 0)
                    {
                        questFlagsList.Add(flag);
                        questFlags -= flag;
                    }
                }
            }

            if (questFlagsEx != 0)
            {
                var flagsArray = Enum.GetValues(typeof(QuestFlagsEx));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (questFlagsEx - flag >= 0)
                    {
                        questFlagsExList.Add(flag);
                        questFlagsEx -= flag;
                    }
                }
            }

            if (questFlagsEx2 != 0)
            {
                var flagsArray = Enum.GetValues(typeof(QuestFlagsEx2));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (questFlagsEx2 - flag >= 0)
                    {
                        questFlagsEx2List.Add(flag);
                        questFlagsEx2 -= flag;
                    }
                }
            }

            if (specialFlags != 0)
            {
                var flagsArray = Enum.GetValues(typeof(QuestSpecialFlags));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (specialFlags - flag >= 0)
                    {
                        specialFlagsList.Add(flag);
                        specialFlags -= flag;
                    }
                }
            }

            questFlagsList.Sort();
            questFlagsExList.Sort();
            questFlagsEx2List.Sort();
            specialFlagsList.Sort();

            string outputText = "";

            if (questFlagsList.Count > 0)
            {
                outputText += "Quest has the following QuestFlags: \r\n";

                outputText = questFlagsList.Aggregate(outputText, (current, itr) => current + ((QuestFlags) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Quest doesn't have any QuestFlags!\r\n";

            if (questFlagsExList.Count > 0)
            {
                outputText += "Quest has the following QuestFlagsEx: \r\n";

                outputText = questFlagsExList.Aggregate(outputText, (current, itr) => current + ((QuestFlagsEx) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Quest doesn't have any QuestFlagsEx!\r\n";

            if (questFlagsEx2List.Count > 0)
            {
                outputText += "Quest has the following QuestFlagsEx2: \r\n";

                outputText = questFlagsEx2List.Aggregate(outputText, (current, itr) => current + ((QuestFlagsEx2)itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Quest doesn't have any QuestFlagsEx2!\r\n";

            if (specialFlagsList.Count > 0)
            {
                outputText += "Quest has the following SpecialFlags: \r\n";

                outputText = specialFlagsList.Aggregate(outputText, (current, itr) => current + ((QuestSpecialFlags) itr + ": " + itr + "\r\n"));
            }
            else
                outputText += "Quest doesn't have any SpecialFlags!\r\n";

            MessageBox.Show(outputText);
        }
    }
}
