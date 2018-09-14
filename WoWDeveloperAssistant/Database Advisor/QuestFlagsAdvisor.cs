using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace WoWDeveloperAssistant.Database_Advisor
{
    public static class QuestFlagsAdvisor
    {
        enum QuestFlags : long
        {
            QUEST_FLAGS_STAY_ALIVE = 0x00000001,
            QUEST_FLAGS_PARTY_ACCEPT = 0x00000002,
            QUEST_FLAGS_EXPLORATION = 0x00000004,
            QUEST_FLAGS_SHARABLE = 0x00000008,
            QUEST_FLAG_HAS_CONDITION = 0x00000010,
            QUEST_FLAGS_HIDE_REWARD_POI = 0x00000020,
            QUEST_FLAGS_RAID = 0x00000040,
            QUEST_FLAGS_EXPANSION_ONLY = 0x00000080,
            QUEST_FLAGS_NO_MONEY_FROM_EXPERIENCE = 0x00000100,
            QUEST_FLAGS_HIDDEN_REWARDS = 0x00000200,
            QUEST_FLAGS_AUTO_REWARDED = 0x00000400,
            QUEST_FLAGS_DEPRICATE_REPUTATION = 0x00000800,
            QUEST_FLAGS_DAILY = 0x00001000,
            QUEST_FLAGS_FLAG_FOR_PVP = 0x00002000,
            QUEST_FLAGS_UNAVAILABLE = 0x00004000,
            QUEST_FLAGS_WEEKLY = 0x00008000,
            QUEST_FLAGS_AUTO_SUBMIT = 0x00010000,
            QUEST_FLAGS_DISPLAY_ITEM_IN_TRACKER = 0x00020000,
            QUEST_FLAGS_DISABLE_COMPLETION_TEXT = 0x00040000,
            QUEST_FLAGS_AUTO_ACCEPT = 0x00080000,
            QUEST_FLAGS_AUTOCOMPLETE = 0x00100000,
            QUEST_FLAGS_AUTO_TAKE = 0x00200000,
            QUEST_FLAGS_UPDATE_PHASE_SHIFT = 0x00400000,
            QUEST_FLAGS_SOR_WHITE_LIST = 0x00800000,
            QUEST_FLAGS_LAUNCH_GOSSIP_COMPLETE = 0x01000000,
            QUEST_FLAGS_REMOVE_EXTRA_GET_ITEMS = 0x02000000,
            QUEST_FLAGS_HIDE_UNTIL_DISCOVERED = 0x04000000,
            QUEST_FLAGS_PORTRAIT_IN_QUEST_LOG = 0x08000000,
            QUEST_FLAGS_SHOW_ITEM_WHEN_COMPLETED = 0x10000000,
            QUEST_FLAGS_LAUNCH_GOSSIP_ACCEPT = 0x20000000,
            QUEST_FLAGS_ITEMS_GLOW_WHEN_DONE = 0x40000000,
            QUEST_FLAGS_FAIL_ON_LOGOUT = 0x80000000
        };

        enum QuestFlagsEx : long
        {
            QUEST_FLAGS_EX_KEEP_ADDITIONAL_ITEMS = 0x0000001,
            QUEST_FLAGS_EX_SUPPRESS_GOSSIP_COMPLETE = 0x0000002,
            QUEST_FLAGS_EX_SUPPRESS_GOSSIP_ACCEPT = 0x0000004,
            QUEST_FLAGS_EX_DISALLOW_PLAYER_AS_QUESTGIVER = 0x0000008,
            QUEST_FLAGS_EX_DISPLAY_CLASS_CHOICE_REWARDS = 0x0000010,
            QUEST_FLAGS_EX_DISPLAY_SPEC_CHOICE_REWARDS = 0x0000020,
            QUEST_FLAGS_EX_REMOVE_FROM_LOG_ON_PERIDOIC_RESET = 0x0000040,
            QUEST_FLAGS_EX_ACCOUNT_LEVEL_QUEST = 0x0000080,
            QUEST_FLAGS_EX_LEGENDARY_QUEST = 0x0000100,
            QUEST_FLAGS_EX_NO_GUILD_XP = 0x0000200,
            QUEST_FLAGS_EX_RESET_CACHE_ON_ACCEPT = 0x0000400,
            QUEST_FLAGS_EX_NO_ABANDON_ONCE_ANY_OBJECTIVE_COMPLETE = 0x0000800,
            QUEST_FLAGS_EX_RECAST_ACCEPT_SPELL_ON_LOGIN = 0x0001000,
            QUEST_FLAGS_EX_UPDATE_ZONE_AURAS = 0x0002000,
            QUEST_FLAGS_EX_NO_CREDIT_FOR_PROXY = 0x0004000,
            QUEST_FLAGS_EX_DISPLAY_AS_DAILY_QUEST = 0x0008000,
            QUEST_FLAGS_EX_PART_OF_QUEST_LINE = 0x0010000,
            QUEST_FLAGS_EX_QUEST_FOR_INTERNAL_BUILDS_ONLY = 0x0020000,
            QUEST_FLAGS_EX_SUPPRESS_SPELL_LEARN_TEXT_LINE = 0x0040000,
            QUEST_FLAGS_EX_DISPLAY_HEADER_AS_OBJECTIVE_FOR_TASKS = 0x0080000,
            QUEST_FLAGS_EX_GARRISON_NON_OWNERS_ALLOWED = 0x0100000,
            QUEST_FLAGS_EX_REMOVE_QUEST_ON_WEEKLY_RESET = 0x0200000,
            QUEST_FLAGS_EX_SUPPRESS_FAREWELL_AUDIO_AFTER_QUEST_ACCEPT = 0x0400000,
            QUEST_FLAGS_EX_REWARDS_BYPASS_WEEKLY_CAPS_AND_SEASON_TOTAL = 0x0800000,
            QUEST_FLAGS_EX_CLEAR_PROGRESS_OF_CRITERIA_TREE_OBJECTIVES_ON_ACCEPT = 0x1000000
        };

        enum QuestSpecialFlags
        {
            QUEST_SPECIAL_FLAGS_REPEATABLE = 0x001,
            QUEST_SPECIAL_FLAGS_EXPLORATION_OR_EVENT = 0x002,
            QUEST_SPECIAL_FLAGS_AUTO_ACCEPT = 0x004,
            QUEST_SPECIAL_FLAGS_DF_QUEST = 0x008,
            QUEST_SPECIAL_FLAGS_MONTHLY = 0x010,
            QUEST_SPECIAL_FLAGS_CAST = 0x020,
            QUEST_SPECIAL_FLAGS_DYNAMIC_ITEM_REWARD = 0x040,
            QUEST_SPECIAL_FLAGS_DB_ALLOWED = QUEST_SPECIAL_FLAGS_REPEATABLE | QUEST_SPECIAL_FLAGS_EXPLORATION_OR_EVENT | QUEST_SPECIAL_FLAGS_AUTO_ACCEPT | QUEST_SPECIAL_FLAGS_DF_QUEST | QUEST_SPECIAL_FLAGS_MONTHLY | QUEST_SPECIAL_FLAGS_DYNAMIC_ITEM_REWARD,
            QUEST_SPECIAL_FLAGS_KILL = 0x0200,
            QUEST_SPECIAL_FLAGS_TIMED = 0x0400,
            QUEST_SPECIAL_FLAGS_PLAYER_KILL = 0x0800
        };

        public static void GetQuestFlags(string questEntry)
        {
            DataSet questFlagsDs = new DataSet();
            string questFlagsSqlQuery = "SELECT `Flags`, `Flags2`, `SpecialFlags` FROM `quest_template` WHERE `Id` = " + questEntry + ";";
            questFlagsDs = (DataSet)SQLModule.DatabaseSelectQuery(questFlagsSqlQuery);
            if (questFlagsDs == null)
                return;

            if (questFlagsDs.Tables["table"].Rows.Count == 0)
            {
                MessageBox.Show("Quest doesn't exists in your database!");
                return;
            }

            long questFlags = Convert.ToInt64(questFlagsDs.Tables["table"].Rows[0][0].ToString());
            long questFlags2 = Convert.ToInt64(questFlagsDs.Tables["table"].Rows[0][1].ToString());
            long specialFlags = Convert.ToInt64(questFlagsDs.Tables["table"].Rows[0][2].ToString());

            List<long> questFlagsList = new List<long>();
            List<long> questFlags2List = new List<long>();
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

            if (questFlags2 != 0)
            {
                var flagsArray = Enum.GetValues(typeof(QuestFlagsEx));
                Array.Reverse(flagsArray);

                foreach (long flag in flagsArray)
                {
                    if (questFlags2 - flag >= 0)
                    {
                        questFlags2List.Add(flag);
                        questFlags2 -= flag;
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
            questFlags2List.Sort();
            specialFlagsList.Sort();

            string outputText = "";

            if (questFlagsList.Count > 0)
            {
                outputText += "Quest has the following QuestFlags: \r\n";

                foreach (long itr in questFlagsList)
                {
                    outputText += (QuestFlags)itr + ": " + itr + "\r\n";
                }
            }
            else
                outputText += "Quest doesn't have any QuestFlags!\r\n";

            if (questFlags2List.Count > 0)
            {
                outputText += "Quest has the following QuestFlags2: \r\n";

                foreach (long itr in questFlags2List)
                {
                    outputText += (QuestFlagsEx)itr + ": " + itr + "\r\n";
                }
            }
            else
                outputText += "Quest doesn't have any QuestFlags2!\r\n";

            if (specialFlagsList.Count > 0)
            {
                outputText += "Quest has the following SpecialFlags: \r\n";

                foreach (long itr in specialFlagsList)
                {
                    outputText += (QuestSpecialFlags)itr + ": " + itr + "\r\n";
                }
            }
            else
                outputText += "Quest doesn't have any SpecialFlags!\r\n";

            MessageBox.Show(outputText);
        }
    }
}
