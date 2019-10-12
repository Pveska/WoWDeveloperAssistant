using System.Collections.Generic;
using System.Data;
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
            { "GossipSelect",      "void sGossipSelect(Player* p_Player, uint32 /*p_Sender*/, uint32 p_Action) override"                     },
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
                    { "PlayerCheck",     "Player* l_Player = p_Summoner->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(16) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(20) + "return;" },
                    { "SetSummonerGuid", "m_SummonerGuid = p_Summoner->GetObjectGuid();" }
                }
            },

            { "QuestAccept",
                new Dictionary<string, string>
                {
                    { "QuestIdSwitch", "switch (p_Quest->GetQuestId())" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "case eQuests::QuestName:" + "\r\n" + Utils.AddSpacesCount(20) + "{" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(20) + "}" + "\r\n" + Utils.AddSpacesCount(20) + "default:" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(16) + "}" }
                }
            },

            { "QuestReward",
                new Dictionary<string, string>
                {
                    { "QuestIdSwitch", "switch (p_Quest->GetQuestId())" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "case eQuests::QuestName:" + "\r\n" + Utils.AddSpacesCount(20) + "{" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(20) + "}" + "\r\n" + Utils.AddSpacesCount(20) + "default:" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(16) + "}" }
                }
            },

            { "GossipSelect",
                new Dictionary<string, string>
                {
                    { "GloseGossipWindow",    "p_Player->PlayerTalkClass->SendCloseGossip();" },
                    { "GossipOptionIdSwitch", "switch (p_Action)" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "case 0:" + "\r\n" + Utils.AddSpacesCount(20) + "{" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(20) + "}" + "\r\n" + Utils.AddSpacesCount(20) + "default:" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(16) + "}" }
                }
            },

            { "MoveInLineOfSight",
                new Dictionary<string, string>
                {
                    { "PlayerCheck",   "Player* l_Player = p_Who->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(16) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(20) + "return;" },
                    { "DistanceCheck", "if (me->GetExactDist2d(p_Who) <= 10.0f)" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "\r\n" + Utils.AddSpacesCount(16) + "}" }
                }
            },

            { "DoAction",
                new Dictionary<string, string>
                {
                    { "DoActionIdSwitch", "switch (p_Action)" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "case eActions::ActionName:" + "\r\n" + Utils.AddSpacesCount(20) + "{" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(20) + "}" + "\r\n" + Utils.AddSpacesCount(20) + "default:" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(16) + "}" }
                }
            },

            { "SetData",
                new Dictionary<string, string>
                {
                    { "SetDataIdSwitch", "switch (p_Value)" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "case eDatas::DataName:" + "\r\n" + Utils.AddSpacesCount(20) + "{" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(20) + "}" + "\r\n" + Utils.AddSpacesCount(20) + "default:" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(16) + "}" }
                }
            },

            { "OnSpellClick",
                new Dictionary<string, string>
                {
                    { "PlayerCheck",   "Player* l_Player = p_Clicker->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(16) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(20) + "return;" },
                }
            },

            { "SpellHit",
                new Dictionary<string, string>
                {
                    { "PlayerCheck",   "Player* l_Player = p_Caster->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(16) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(20) + "return;" },
                    { "SpellIdSwitch", "switch (p_Spell->Id)" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "case eSpells::SpellName:" + "\r\n" + Utils.AddSpacesCount(20) + "{" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(20) + "}" + "\r\n" + Utils.AddSpacesCount(20) + "default:" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(16) + "}" }
                }
            },

            { "OnSpellCasted",
                new Dictionary<string, string>
                {
                    { "SpellIdSwitch", "switch (p_SpellInfo->Id)" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "case eSpells::SpellName:" + "\r\n" + Utils.AddSpacesCount(20) + "{" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(20) + "}" + "\r\n" + Utils.AddSpacesCount(20) + "default:" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(16) + "}" }
                }
            },

            { "PassengerBoarded",
                new Dictionary<string, string>
                {
                    { "PlayerCheck",   "Player* l_Player = p_Passenger->ToPlayer();" + "\r\n" + Utils.AddSpacesCount(16) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(20) + "return;" },
                }
            },

            { "MovementInform",
                new Dictionary<string, string>
                {
                    { "PlayerCheck",   "Player* l_Player = ObjectAccessor::GetPlayer(*me, m_SummonerGuid);" + "\r\n" + Utils.AddSpacesCount(16) + "if (!l_Player)" + "\r\n" + Utils.AddSpacesCount(20) + "return;" },
                    { "PointIdSwitch", "switch (p_PointId)" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "case ePoints::PointName:" + "\r\n" + Utils.AddSpacesCount(20) + "{" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(20) + "}" + "\r\n" + Utils.AddSpacesCount(20) + "default:" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(16) + "}" }
                }
            },

            { "Reset",
                new Dictionary<string, string>
                {
                    { "EventReset",   "m_Events.Reset();" },
                }
            },

            { "EnterCombat",
                new Dictionary<string, string>
                {
                    { "ScheduleEvent", "m_Events.ScheduleEvent(eEvents::EventName, 10000);" },
                }
            },

            { "UpdateAI",
                new Dictionary<string, string>
                {
                    { "PlayerCheck",   "Player* l_Player = ObjectAccessor::GetPlayer(*me, m_SummonerGuid);" + "\r\n" + Utils.AddSpacesCount(16) + "if (!l_Player || !l_Player->IsInWorld() || !l_Player->HasQuest(eQuests::QuestName))" + "\r\n" + Utils.AddSpacesCount(16) + "{" +  "\r\n" + Utils.AddSpacesCount(20) + "me->DespawnOrUnsummon();" + "\r\n" + Utils.AddSpacesCount(20) + "return;" + "\r\n" + Utils.AddSpacesCount(16) + "}" },
                    { "CombatChecks",  "if (!UpdateVictim())" + "\r\n" + Utils.AddSpacesCount(20) + "return;" + "\r\n\r\n" + Utils.AddSpacesCount(16) + "m_Events.Update(p_Diff);" + "\r\n\r\n" + Utils.AddSpacesCount(16) + "if (me->HasUnitState(UNIT_STATE_CASTING))" + "\r\n" + Utils.AddSpacesCount(20) + "return;" },
                    { "EventsSwitch",  "switch (m_Events.ExecuteEvent())" + "\r\n" + Utils.AddSpacesCount(16) + "{" + "\r\n" + Utils.AddSpacesCount(20) + "case eEvents::EventName:" + "\r\n" + Utils.AddSpacesCount(20) + "{" + "\r\n" + Utils.AddSpacesCount(24) + "m_Events.ScheduleEvent(eEvents::Eventname, 10000);" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(20) + "}" + "\r\n" + Utils.AddSpacesCount(20) + "default:" + "\r\n" + Utils.AddSpacesCount(24) + "break;" + "\r\n" + Utils.AddSpacesCount(16) + "}" },
                    { "DoMeleeAttack", "DoMeleeAttackIfReady();" }
                }
            },
        };
        public static void CreateTemplate(uint objectEntry, ListBox hooksListBox, TreeView hookBodiesTreeView)
        {
            string scriptBody = "";
            string defaultName = "";
            string scriptName = "";

            DataSet creatureNameDs = new DataSet();
            string creatureNameQuery = "SELECT `Name1` FROM `creature_template_wdb` WHERE `entry` = " + objectEntry + ";";
            creatureNameDs = Properties.Settings.Default.UsingDB ? (DataSet)SQLModule.DatabaseSelectQuery(creatureNameQuery) : null;

            if (creatureNameDs != null)
            {
                foreach (DataRow row in creatureNameDs.Tables["table"].Rows)
                {
                    defaultName = row[0].ToString();
                }
            }

            if (defaultName == "")
                return;

            scriptName = "npc_" + defaultName.Replace(" ", "_").ToLower().Replace("'", "") + "_" + objectEntry;
            scriptBody = "/// " + defaultName + " - " + objectEntry + "\r\n";
            scriptBody += "class " + scriptName + " : public CreatureScript" + "\r\n";
            scriptBody += "{" + "\r\n";
            scriptBody += Utils.AddSpacesCount(4) + "public:" + "\r\n";
            scriptBody += Utils.AddSpacesCount(8) + scriptName + "()" + " : CreatureScript(\"" + scriptName + "\")" + " { }" + "\r\n\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "struct " + scriptName + "AI" + " : public " + (IsVehicleScript(hooksListBox) ? "VehicleAI" : "ScriptedAI") + "\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "{" + "\r\n";
            scriptBody += Utils.AddSpacesCount(12) + "explicit " + scriptName + "AI" + "(Creature* p_Creature) : " + (IsVehicleScript(hooksListBox) ? "VehicleAI" : "ScriptedAI") + "(p_Creature) { }";
            scriptBody += GetEnumsBody(hookBodiesTreeView);
            scriptBody += GetVariablesBody(hookBodiesTreeView);
            scriptBody += GetHooksBody(hooksListBox, hookBodiesTreeView);
            scriptBody += "\r\n" + Utils.AddSpacesCount(8) + "};" + "\r\n\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "CreatureAI* GetAI(Creature* p_Creature) const override" + "\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "{" + "\r\n";
            scriptBody += Utils.AddSpacesCount(12) + "return new " + scriptName + "AI(p_Creature);" + "\r\n";
            scriptBody += Utils.AddSpacesCount(8) + "}" + "\r\n";
            scriptBody += "};";

            Clipboard.SetText(scriptBody);
            MessageBox.Show("Template has been successfully builded and copied on your clipboard!");
        }

        private static bool IsVehicleScript(ListBox listBox)
        {
            foreach (var item in listBox.SelectedItems)
            {
                if (item.ToString() == "PassengerBoarded")
                    return true;
            }

            return false;
        }

        private static string GetEnumsBody(TreeView hookBodiesTreeView)
        {
            string body = "";

            if (IsHookBodiesContainItem("SpellIdSwitch", hookBodiesTreeView) ||
                IsHookBodiesContainItem("EventsSwitch", hookBodiesTreeView))
            {
                body += "\r\n\r\n" + Utils.AddSpacesCount(12) + "enum eSpells" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "\r\n" + Utils.AddSpacesCount(12) + "};";
            }

            if (IsHookBodiesContainItem("PointIdSwitch", hookBodiesTreeView))
            {
                body += "\r\n\r\n" + Utils.AddSpacesCount(12) + "enum ePoints" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "\r\n" + Utils.AddSpacesCount(12) + "};";
            }

            if (IsHookBodiesContainItem("EventsSwitch", hookBodiesTreeView))
            {
                body += "\r\n\r\n" + Utils.AddSpacesCount(12) + "enum eEvents" + "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16) + "\r\n" + Utils.AddSpacesCount(12) + "};";
            }

            return body;
        }

        private static string GetVariablesBody(TreeView hookBodiesTreeView)
        {
            string body = "";
            uint variablesCount = 0;

            if (IsHookBodiesContainItem("SetSummonerGuid", hookBodiesTreeView))
            {
                if (variablesCount == 0)
                {
                    body += "\r\n\r\n" + Utils.AddSpacesCount(12) + "ObjectGuid m_SummonerGuid;";
                }
                else
                {
                    body += "\r\n" + Utils.AddSpacesCount(12) + "ObjectGuid m_SummonerGuid;";
                }

                variablesCount++;
            }

            if (IsHookBodiesContainItem("EventsSwitch", hookBodiesTreeView))
            {
                if (variablesCount == 0)
                {
                    body += "\r\n\r\n" + Utils.AddSpacesCount(12) + "EventMap m_Events;";
                }
                else
                {
                    body += "\r\n" + Utils.AddSpacesCount(12) + "EventMap m_Events;";
                }

                variablesCount++;
            }

            return body;
        }

        private static bool IsHookBodiesContainItem(string itemName, TreeView hookBodiesTreeView)
        {
            foreach (TreeNode parentNode in hookBodiesTreeView.Nodes)
            {
                foreach (TreeNode childNode in parentNode.Nodes)
                {
                    if (childNode.Checked && childNode.Text == itemName)
                        return true;
                }
            }

            return false;
        }

        private static string GetHooksBody(ListBox hooksListBox, TreeView hookBodiesTreeView)
        {
            string body = "";

            foreach (var hook in hooksListBox.SelectedItems)
            {
                body += "\r\n\r\n" + Utils.AddSpacesCount(12) + hooksDictionary[hook.ToString()];
                body += "\r\n" + Utils.AddSpacesCount(12) + "{" + "\r\n" + Utils.AddSpacesCount(16);

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
                                body += "\r\n\r\n" + Utils.AddSpacesCount(16);
                            }

                            body += hookBodiesDictionary[hook.ToString()][childNode.Text];

                            if (!moreThanOne)
                            {
                                moreThanOne = true;
                            }
                        }
                    }
                }

                body += "\r\n" + Utils.AddSpacesCount(12) + "}";
            }

            return body;
        }
    }
}
