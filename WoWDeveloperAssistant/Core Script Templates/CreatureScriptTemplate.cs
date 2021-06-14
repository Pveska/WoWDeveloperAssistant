using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;

namespace WoWDeveloperAssistant.Core_Script_Templates
{
    public static class CreatureScriptTemplate
    {
        public static Dictionary<string, string> hooksDictionary = new Dictionary<string, string>
        {
            { "IsSummonedBy",      "void IsSummonedBy(Unit* p_Summoner) override"                                                            },
            { "QuestAccept",       "void sQuestAccept(Player* p_Player, Quest const* p_Quest) override"                                      },
            { "QuestReward",       "void sQuestReward(Player* p_Player, Quest const* p_Quest, uint32 /*p_Option*/) override"                 },
            { "GossipSelect",      "void sGossipSelect(Player* p_Player, uint32 /*p_MenuId*/, uint32 p_GossipListId) override"               },
            { "GossipHello",       "void sGossipHello(Player* p_Player) override"                                                            },
            { "MoveInLineOfSight", "void MoveInLineOfSight(Unit* p_Who) override"                                                            },
            { "DoAction",          "void DoAction(int32 const p_Action) override"                                                            },
            { "SetData",           "void SetData(uint64 /*p_Type*/, uint32 p_Value) override"                                                },
            { "OnSpellClick",      "void OnSpellClick(Unit* p_Clicker) override"                                                             },
            { "SpellHit",          "void SpellHit(Unit* p_Caster, SpellInfo const* p_Spell) override"                                        },
            { "OnSpellCasted",     "void OnSpellCasted(SpellInfo const* p_SpellInfo) override"                                               },
            { "PassengerBoarded",  "void PassengerBoarded(Unit* p_Passenger, int8 /*p_SeatID*/, bool p_Apply) override"                      },
            { "MovementInform",    "void MovementInform(uint32 /*p_Type*/, uint64 p_PointId) override"                                       },
            { "Reset",             "void Reset() override"                                                                                   },
            { "EnterCombat",       "void EnterCombat(Unit* /*p_Victim*/) override"                                                           },
            { "DamageTaken",       "void DamageTaken(Unit* /*p_Attacker*/, uint32& /*p_Damage*/, SpellInfo const* /*p_SpellInfo*/) override" },
            { "JustDied",          "void JustDied(Unit* /*p_Killer*/) override"                                                              },
            { "UpdateAI",          "void UpdateAI(uint32 const p_Diff) override"                                                             }
        };

        public static Dictionary<string, Dictionary<string, string>> hookBodiesDictionary = new Dictionary<string, Dictionary<string, string>>
        {
            { "IsSummonedBy",
                new Dictionary<string, string>
                {
                    { "PlayerCheck", "Player* l_Player = p_Summoner->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" }
                }
            },

            { "QuestAccept",
                new Dictionary<string, string>
                {
                    { "QuestIdSwitch", "switch (p_Quest->GetQuestId())" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "case eQuests::QuestName:" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(12) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "default:" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(8) + "}" }
                }
            },

            { "QuestReward",
                new Dictionary<string, string>
                {
                    { "QuestIdSwitch", "switch (p_Quest->GetQuestId())" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "case eQuests::QuestName:" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(12) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "default:" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(8) + "}" }
                }
            },

            { "GossipSelect",
                new Dictionary<string, string>
                {
                    { "GloseGossipWindow",    "p_Player->PlayerTalkClass->SendCloseGossip();" },
                    { "GossipOptionIdSwitch", "switch (p_GossipListId)" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "case 0:" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(12) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "default:" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(8) + "}" }
                }
            },

            { "MoveInLineOfSight",
                new Dictionary<string, string>
                {
                    { "PlayerCheck",   "Player* l_Player = p_Who->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
                    { "DistanceCheck", "if (me->GetExactDist2d(p_Who) <= 10.0f)" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "\r\n" + Utils.AddSpacesCount(8) + "}" }
                }
            },

            { "DoAction",
                new Dictionary<string, string>
                {
                    { "DoActionIdSwitch", "switch (p_Action)" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "case eActions::ActionName:" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(12) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "default:" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(8) + "}" }
                }
            },

            { "SetData",
                new Dictionary<string, string>
                {
                    { "SetDataIdSwitch", "switch (p_Value)" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "case eDatas::DataName:" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(12) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "default:" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(8) + "}" }
                }
            },

            { "OnSpellClick",
                new Dictionary<string, string>
                {
                    { "PlayerCheck", "Player* l_Player = p_Clicker->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" }
                }
            },

            { "SpellHit",
                new Dictionary<string, string>
                {
                    { "PlayerCheck",   "Player* l_Player = p_Caster->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
                    { "SpellIdSwitch", "switch (p_Spell->Id)" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "case eSpells::SpellName:" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(12) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "default:" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(8) + "}" }
                }
            },

            { "OnSpellCasted",
                new Dictionary<string, string>
                {
                    { "SpellIdSwitch", "switch (p_SpellInfo->Id)" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "case eSpells::SpellName:" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(12) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "default:" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(8) + "}" }
                }
            },

            { "PassengerBoarded",
                new Dictionary<string, string>
                {
                    { "PlayerCheck", "Player* l_Player = p_Passenger->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" }
                }
            },

            { "MovementInform",
                new Dictionary<string, string>
                {
                    { "PointIdSwitch", "switch (p_PointId)" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "case ePoints::PointName:" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(12) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "default:" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(8) + "}" }
                }
            },

            { "Reset",
                new Dictionary<string, string>
                {
                    { "EventReset", "events.Reset();" }
                }
            },

            { "EnterCombat",
                new Dictionary<string, string>
                {
                    { "ScheduleEvent", "events.ScheduleEvent(eEvents::EventName, 10000);" }
                }
            },

            { "UpdateAI",
                new Dictionary<string, string>
                {
                    { "UpdateOperations",     "UpdateOperations(p_Diff);" },
                    { "CheckPlayerOrDespawn", "CheckPlayerOrDespawn(p_Diff, eQuests::QuestId);" },
                    { "DefaultPlayerCheck",   "if (me->isSummon() && UpdateCheckTimer(p_Diff))" + "\r\n" +  Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "Player* l_Player = me->GetAnyPlayerOwner();" + "\r\n" + Utils.AddSpacesCount(16) + "if (!l_Player || !l_Player->IsInWorld() || !l_Player->HasQuest(eQuests::QuestId))" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "me->DespawnOrUnsummon();" + "\r\n" + Utils.AddSpacesCount(16) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "}" },
                    { "CombatChecks",         "if (!UpdateVictim())" + "\r\n" + Utils.AddSpacesCount(12) + "return;" + "\r\n\r\n" + Utils.AddSpacesCount(8) + "events.Update(p_Diff);" + "\r\n\r\n" + Utils.AddSpacesCount(8) + "if (me->HasUnitState(UNIT_STATE_CASTING))" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
                    { "EventsSwitch",         "switch (events.ExecuteEvent())" + "\r\n" + Utils.AddSpacesCount(8) + "{" + "\r\n" + Utils.AddSpacesCount(12) + "case eEvents::EventName:" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "events.ScheduleEvent(eEvents::Eventname, 10000);" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(12) + "}" + "\r\n" + Utils.AddSpacesCount(12) + "default:" + "\r\n" + Utils.AddSpacesCount(16) + "break;" + "\r\n" + Utils.AddSpacesCount(8) + "}" },
                    { "DoMeleeAttack",        "DoMeleeAttackIfReady();" }
                }
            },
        };

        public static void CreateTemplate(uint objectEntry, ListBox hooksListBox, TreeView hookBodiesTreeView)
        {
            string scriptBody = "";
            string defaultName = "";
            string scriptName = "";

            string creatureNameQuery = "SELECT `Name1` FROM `creature_template_wdb` WHERE `entry` = " + objectEntry + ";";
            var creatureNameDs = Properties.Settings.Default.UsingDB ? SQLModule.DatabaseSelectQuery(creatureNameQuery) : null;

            if (creatureNameDs != null)
            {
                foreach (DataRow row in creatureNameDs.Tables["table"].Rows)
                {
                    defaultName = row[0].ToString();
                }
            }

            if (defaultName == "")
                return;

            scriptName = "npc_" + NormilizeScriptName(defaultName) + "_" + objectEntry;
            scriptBody = "/// " + defaultName + " - " + objectEntry + "\r\n";
            scriptBody += "struct " + scriptName + " : public " + (IsVehicleScript(hooksListBox) ? "VehicleAI" : "ScriptedAI") + "\r\n";
            scriptBody += "{" + "\r\n";
            scriptBody += Utils.AddSpacesCount(4) + "explicit " + scriptName + "(Creature* p_Creature) : " + (IsVehicleScript(hooksListBox) ? "VehicleAI" : "ScriptedAI") + "(p_Creature) { }";
            scriptBody += GetEnumsBody(hookBodiesTreeView);
            scriptBody += GetHooksBody(hooksListBox, hookBodiesTreeView);
            scriptBody += "\r\n" + "};" + "\r\n";

            Clipboard.SetText(scriptBody);
            MessageBox.Show("Template has been successfully builded and copied on your clipboard!");
        }

        public static string NormilizeScriptName(string line)
        {
            Regex nonWordRegex = new Regex(@"\W+");
            string normilizedString = line;

            normilizedString = normilizedString.Replace(" ", "_");

            foreach (char character in normilizedString)
            {
                if (character == '_')
                    continue;

                if (nonWordRegex.IsMatch(character.ToString()))
                {
                    normilizedString = normilizedString.Replace(nonWordRegex.Match(character.ToString()).ToString(), "");
                }
            }

            normilizedString = normilizedString.ToLower();

            return normilizedString;
        }

        private static bool IsVehicleScript(ListBox listBox)
        {
            return listBox.SelectedItems.Cast<object>().Any(item => item.ToString() == "PassengerBoarded");
        }

        private static string GetEnumsBody(TreeView hookBodiesTreeView)
        {
            string body = "";

            if (IsHookBodiesContainItem("SpellIdSwitch", hookBodiesTreeView) ||
                IsHookBodiesContainItem("EventsSwitch", hookBodiesTreeView))
            {
                body += "\r\n\r\n" + Utils.AddSpacesCount(4) + "enum eSpells" + "\r\n" + Utils.AddSpacesCount(4) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "\r\n" + Utils.AddSpacesCount(4) + "};";
            }

            if (IsHookBodiesContainItem("PointIdSwitch", hookBodiesTreeView))
            {
                body += "\r\n\r\n" + Utils.AddSpacesCount(4) + "enum ePoints" + "\r\n" + Utils.AddSpacesCount(4) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "\r\n" + Utils.AddSpacesCount(4) + "};";
            }

            if (IsHookBodiesContainItem("EventsSwitch", hookBodiesTreeView))
            {
                body += "\r\n\r\n" + Utils.AddSpacesCount(4) + "enum eEvents" + "\r\n" + Utils.AddSpacesCount(4) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "\r\n" + Utils.AddSpacesCount(4) + "};";
            }

            return body;
        }

        private static bool IsHookBodiesContainItem(string itemName, TreeView hookBodiesTreeView)
        {
            return hookBodiesTreeView.Nodes.Cast<TreeNode>().Any(parentNode => parentNode.Nodes.Cast<TreeNode>().Any(childNode => childNode.Checked && childNode.Text == itemName));
        }

        private static string GetHooksBody(ListBox hooksListBox, TreeView hookBodiesTreeView)
        {
            string body = "";

            foreach (var hook in hooksListBox.SelectedItems)
            {
                body += "\r\n\r\n" + Utils.AddSpacesCount(4) + hooksDictionary[hook.ToString()];
                body += "\r\n" + Utils.AddSpacesCount(4) + "{" + "\r\n" + Utils.AddSpacesCount(8);

                foreach (TreeNode parentNode in hookBodiesTreeView.Nodes)
                {
                    if (parentNode.Text != hook.ToString())
                        continue;

                    bool moreThanOne = false;

                    foreach (TreeNode childNode in parentNode.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            if (moreThanOne)
                            {
                                body += "\r\n\r\n" + Utils.AddSpacesCount(8);
                            }

                            body += hookBodiesDictionary[hook.ToString()][childNode.Text];

                            if (!moreThanOne)
                            {
                                moreThanOne = true;
                            }
                        }
                    }
                }

                body += "\r\n" + Utils.AddSpacesCount(4) + "}";
            }

            return body;
        }
    }
}
