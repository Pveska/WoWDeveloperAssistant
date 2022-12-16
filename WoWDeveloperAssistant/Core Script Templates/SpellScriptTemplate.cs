using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WoWDeveloperAssistant.Misc;
using WoWDeveloperAssistant.Creature_Scripts_Creator;

namespace WoWDeveloperAssistant.Core_Script_Templates
{
    public static class SpellScriptTemplate
    {
        public static Dictionary<string, string> hooksDictionary = new Dictionary<string, string>
        {
            { "BeforeCast",                 "void HandleBeforeCast ()"                                                                                 },
            { "OnCast",                     "void HandleOnCast ()"                                                                                     },
            { "AfterCast",                  "void HandleAfterCast ()"                                                                                  },
            { "OnCheckCast",                "SpellCastResult CheckCast ()"                                                                             },
            { "OnTakePowers",               "void HandleTakePowers (Powers p_PowerType, int32& p_PowerCost)"                                           },
            { "OnCalculateThreat",          "void HandleCalculateThreat (Unit* p_Target, float& p_Amount)"                                             },
            { "OnCalculateChannelDuration", "void HandleCalculateChannelDuration (Unit const* p_Caster, int32& p_ChannelDuration)"                     },
            { "OnInterrupt",                "void HandleInterrupt (uint32 p_Time)"                                                                     },
            { "OnCheckInterrupt",           "bool HandleCheckInterrupt ()"                                                                             },
            { "OnPrepare",                  "void HandleOnPrepare ()"                                                                                  },
            { "OnEffectLaunch",             "void HandleEffectLaunch (SpellEffIndex p_EffIndex)"                                                       },
            { "OnEffectLaunchTarget",       "void HandleEffectLaunchTarget (SpellEffIndex p_EffIndex)"                                                 },
            { "OnEffectHit",                "void HandleEffectHit (SpellEffIndex p_EffIndex)"                                                          },
            { "OnEffectHitTarget",          "void HandleEffectHitTarget (SpellEffIndex p_EffIndex)"                                                    },
            { "BeforeHit",                  "void HandleBeforeHit ()"                                                                                  },
            { "OnHit",                      "void HandleHit ()"                                                                                        },
            { "AfterHit",                   "void HandleAfterHit ()"                                                                                   },
            { "OnObjectAreaTargetSelect",   "void HandleTargetsSelect (std::list<WorldObject*>& p_Targets)"                                            },
            { "OnObjectTargetSelect",       "void HandleTargetSelect (WorldObject*& p_Target)"                                                         },
            { "OnDispelAura",               "void HandleDispelAura (uint32 p_AuraID)"                                                                  },
            { "OnDestinationTargetSelect",  "void HandleSelectDest (SpellDestination& p_Dest)"                                                         },

            { "DoCheckAreaTarget",          "bool HandleCheckAreaTarget (Unit* p_Target)"                                                              },
            { "OnDispel",                   "void HandleOnDispel (DispelInfo* p_DispelInfo)"                                                           },
            { "AfterDispel",                "void HandleAfterDispel (DispelInfo* p_DispelInfo)"                                                        },
            { "OnEffectApply",              "void HandleOnEffectApply (AuraEffect const* p_AurEff, AuraEffectHandleModes p_Mode)"                      },
            { "AfterEffectApply",           "void HandleAfterEffectApply (AuraEffect const* p_AurEff, AuraEffectHandleModes p_Mode)"                   },
            { "OnEffectRemove",             "void HandleOnEffectRemove (AuraEffect const* p_AurEff, AuraEffectHandleModes p_Mode)"                     },
            { "AfterEffectRemove",          "void HandleAfterEffectRemove (AuraEffect const* p_AurEff, AuraEffectHandleModes p_Mode)"                  },
            { "OnEffectPeriodic",           "void HandleTick (AuraEffect const* p_AurEff)"                                                             },
            { "AfterEffectPeriodic",        "void HandleAfterTick (AuraEffect const* p_AurEff)"                                                        },
            { "OnAuraUpdate",               "void HandleUpdate (const uint32 p_Diff)"                                                                  },
            { "OnEffectUpdate",             "void HandleEffectUpdate (uint32 p_Diff, AuraEffect* p_AurEff)"                                            },
            { "OnEffectUpdatePeriodic",     "void HandleEffectUpdateTick (AuraEffect* p_AurEff)"                                                       },
            { "DoEffectCalcAmount",         "void HandleDoCalcAmount (AuraEffect const* p_AurEff, int32& p_Amount, bool& p_CanBeRecalculated)"         },
            { "DoEffectCalcFloatAmount",    "void HandleDoCalcFloatAmount (AuraEffect const* p_AurEff, float& p_Amount, bool& p_CanBeRecalculated)"    },
            { "DoCalcMaxDuration",          "void HandleDoCalcMaxDuration (int32& p_MaxDuration)"                                                      },
            { "DoEffectCalcPeriodic",       "void HandleDoCalcPeriodic (AuraEffect const* p_AurEff, bool& p_IsPeriodic, int32& p_AuraPeriod)"          },
            { "DoEffectCalcSpellMod",       "void HandleDoCalcSpellMod (AuraEffect const* p_AurEff, SpellModifier*& p_SpellMod)"                       },
            { "OnEffectAbsorb",             "void HandleOnAbsorb (AuraEffect* p_AurEff, DamageInfo& p_DamageInfo, uint32& p_AbsorbAmount)"             },
            { "AfterEffectAbsorb",          "void HandleAfterAbsorb (AuraEffect* p_AurEff, DamageInfo& p_DamageInfo, uint32& p_AbsorbAmount)"          },
            { "OnEffectManaShield",         "void HandleOnManaShield (AuraEffect* p_AurEff, DamageInfo& p_DamageInfo, uint32& p_AbsorbAmount)"         },
            { "AfterEffectManaShield",      "void HandleAfterManaShield (AuraEffect* p_AurEff, DamageInfo& p_DamageInfo, uint32& p_AbsorbAmount)"      },
            { "OnEffectSplitDamage",        "void HandleSplitDamage (AuraEffect* p_AurEff, DamageInfo& p_DamageInfo, uint32& p_SplitAmount)"           },
            { "DoCheckProc",                "bool HandleCheckProc (ProcEventInfo& p_EventInfo)"                                                        },
            { "DoCheckEffectProc",          "void HandleCheckEffectProc (AuraEffect const* p_AurEff, ProcEventInfo& p_EventInfo, bool & p_IsTriggeredEffect)" },
            { "DoCalcPPM",                  "void HandleCalcPPM (float& p_PPM, ProcEventInfo& p_EventInfo)"                                            },
            { "DoCalcProcChance",           "void HandleCalcProcChance (float& p_Chance, ProcEventInfo& p_EventInfo)"                                  },
            { "DoPrepareProc",              "void HandlePrepareProc (ProcEventInfo& p_EventInfo)"                                                      },
            { "OnProc",                     "void HandleOnProc (ProcEventInfo& p_EventInfo)"                                                           },
            { "AfterProc",                  "void HandleAferProc (ProcEventInfo& p_EventInfo)"                                                         },
            { "OnEffectProc",               "void HandleEffectProc (AuraEffect const* p_AurEff, ProcEventInfo& p_EventInfo)"                           },
            { "AfterEffectProc",            "void HandleAfterEffectProc (AuraEffect const* p_AurEff, ProcEventInfo& p_EventInfo)"                      },
            { "CanRefreshProc",             "bool HandleCanRefreshProc ()"                                                                             },
            { "OnEffectKeyboundOverride",   "void HandleKeyboundOverride (AuraEffect* p_AurEff, DamageInfo& p_DamageInfo, uint32& p_SplitAmount)"      },
            { "DoCheckSpellProcEvent",      "bool HandleDoCheckSpellProcEvent (SpellInfo const* p_ProcSpell, uint32 p_ProcFlag1, uint32 p_ProcFlag2, uint32 p_ProcExtra)" },
            { "OnEffectExpireTick",         "void HandleExpireTick (AuraEffect* p_AurEff, float& p_Remaining)"                                         }
        };

        public static Dictionary<string, string> hooksSpellChecksDictionary = new Dictionary<string, string>()
        {
            { "CasterCheck", "Unit* l_Caster = GetCaster();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Caster)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "ExplTargetCheck", "Unit* l_Target = GetExplTargetUnit();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Target)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "ExplGameObjectCheck", "GameObject* l_GameObjectTarget = GetExplTargetGObj();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_GameObjectTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "ExplItemCheck", "Item* l_ItemTarget = GetExplTargetItem();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_ItemTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "ExplDestCheck", "WorldLocation const* l_DestTarget = GetExplTargetDest();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_DestTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "TargetCheck", "Unit* l_Target = GetHitUnit();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Target)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "CreatureCheck", "Unit* l_CreatureTarget = GetHitCreature();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_CreatureTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "PlayerCheckCheck", "Unit* l_PlayerTarget = GetHitPlayer();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_PlayerTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "ItemCheck", "Unit* l_ItemTarget = GetHitItem();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_ItemTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "GameObjectCheck", "Unit* l_GameObjectTarget = GetHitGObj();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_GameObjectTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "DestCheck", "WorldLocation const* l_DestTarget = GetHitDest();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_DestTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
        };

        public static Dictionary<string, string> hooksAuraChecksDictionary = new Dictionary<string, string>()
        {
            { "CasterCheck", "Unit* l_Caster = GetCaster();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Caster)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "TargetCheck", "Unit* l_Target = GetTarget();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Target)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "UnitOwnerCheck", "Unit* l_Owner = GetUnitOwner();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Target)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "DynObjectCheck", "DynamicObject* l_DynObject = GetDynobjOwner();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_DynObject)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "DamageInfoCheck", "DamageInfo* l_DamageInfo = p_EventInfo.GetDamageInfo();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_DamageInfo)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "SpellInfoCheck", "SpellInfo const* l_SpellInfo = p_EventInfo.GetSpellInfo();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_SpellInfo)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "DMGActorCheck", "Unit* l_Actor = l_DamageInfo->GetActor();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Actor)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "DMGTargetCheck", "Unit* l_ProcTarget = l_DamageInfo->GetTarget();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_ProcTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "ActorCheck", "Unit* l_Actor = p_EventInfo.GetActor();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_Actor)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "ActionTargetCheck", "Unit* l_ActionTarget = p_EventInfo.GetActionTarget();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_ActionTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "ProcTargetCheck", "Unit* l_ProcTarget = p_EventInfo.GetProcTarget();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_ProcTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
            { "NonActorTargetCheck", "Unit* l_NonActorTarget = p_EventInfo.GetNonActorTarget();" + "\r\n" + Utils.AddSpacesCount(8) + "if (!l_NonActorTarget)" + "\r\n" + Utils.AddSpacesCount(12) + "return;" },
        };

        public static Dictionary<string, List<string>> hookBodiesDictionary = new Dictionary<string, List<string>>
        {
            { "BeforeCast",
                new List<string> { "CasterCheck", "ExplTargetCheck", "ExplGameObjectCheck", "ExplItemCheck", "ExplDestCheck" }
            },

             { "OnCast",
                new List<string> { "CasterCheck", "ExplTargetCheck", "ExplGameObjectCheck", "ExplItemCheck", "ExplDestCheck" }
            },

            { "AfterCast",
                new List<string> { "CasterCheck", "ExplTargetCheck", "ExplGameObjectCheck", "ExplItemCheck", "ExplDestCheck" }
            },

            { "OnCheckCast",
                new List<string> { "CasterCheck", "ExplTargetCheck", "ExplGameObjectCheck", "ExplItemCheck", "ExplDestCheck" }
            },

            { "OnTakePowers",
                new List<string> { "CasterCheck", "ExplTargetCheck" }
            },

            { "OnCalculateThreat",
                new List<string> { "CasterCheck" }
            },

            { "OnCheckInterrupt",
                new List<string> { "CasterCheck", "ExplTargetCheck", "ExplGameObjectCheck", "ExplItemCheck", "ExplDestCheck" }
            },

            { "OnEffectLaunch",
                new List<string> { "CasterCheck", "ExplTargetCheck", "ExplGameObjectCheck", "ExplItemCheck", "ExplDestCheck" }
            },

            { "OnEffectLaunchTarget",
                new List<string> { "CasterCheck", "TargetCheck", "CreatureCheck", "PlayerCheckCheck", "ItemCheck", "GameObjectCheck", "DestCheck" }
            },

            { "OnEffectHit",
                new List<string> { "CasterCheck", "ExplTargetCheck", "ExplGameObjectCheck", "ExplItemCheck", "ExplDestCheck" }
            },

            { "OnEffectHitTarget",
                new List<string> { "CasterCheck", "TargetCheck", "CreatureCheck", "PlayerCheckCheck", "ItemCheck", "GameObjectCheck", "DestCheck" }
            },

            { "BeforeHit",
                new List<string> { "CasterCheck", "TargetCheck", "CreatureCheck", "PlayerCheckCheck", "ItemCheck", "GameObjectCheck", "DestCheck" }
            },

            { "OnHit",
                new List<string> { "CasterCheck", "TargetCheck", "CreatureCheck", "PlayerCheckCheck", "ItemCheck", "GameObjectCheck", "DestCheck" }
            },

             { "AfterHit",
                new List<string> { "CasterCheck", "TargetCheck", "CreatureCheck", "PlayerCheckCheck", "ItemCheck", "GameObjectCheck", "DestCheck" }
            },

            { "OnObjectAreaTargetSelect",
                new List<string> { "CasterCheck" }
            },

            { "OnObjectTargetSelect",
                new List<string> { "CasterCheck" }
            },

            { "OnDispelAura",
                new List<string> { "CasterCheck" }
            },

            { "OnDestinationTargetSelect",
                new List<string> { "CasterCheck" }
            },


            { "OnDispel",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "AfterDispel",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnEffectApply",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

             { "AfterEffectApply",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnEffectRemove",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "AfterEffectRemove",
               new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

             { "OnEffectPeriodic",
               new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "AfterEffectPeriodic",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnAuraUpdate",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnEffectUpdate",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnEffectUpdatePeriodic",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "DoEffectCalcAmount",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "DoEffectCalcFloatAmount",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "DoCalcMaxDuration",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "DoEffectCalcPeriodic",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "DoEffectCalcSpellMod",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnEffectAbsorb",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "AfterEffectAbsorb",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnEffectManaShield",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "AfterEffectManaShield",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnEffectSplitDamage",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "DoCheckProc",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck", "DamageInfoCheck", "SpellInfoCheck", "DMGActorCheck", "DMGTargetCheck", "ActorCheck", "ActionTargetCheck", "ProcTargetCheck", "NonActorTargetCheck" }
            },

            { "DoCheckEffectProc",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck", "DamageInfoCheck", "SpellInfoCheck", "DMGActorCheck", "DMGTargetCheck", "ActorCheck", "ActionTargetCheck", "ProcTargetCheck", "NonActorTargetCheck" }
            },

            { "DoCalcPPM",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck", "DamageInfoCheck", "SpellInfoCheck", "DMGActorCheck", "DMGTargetCheck", "ActorCheck", "ActionTargetCheck", "ProcTargetCheck", "NonActorTargetCheck" }
            },

            { "DoCalcProcChance",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck", "DamageInfoCheck", "SpellInfoCheck", "DMGActorCheck", "DMGTargetCheck", "ActorCheck", "ActionTargetCheck", "ProcTargetCheck", "NonActorTargetCheck" }
            },

            { "DoPrepareProc",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck", "DamageInfoCheck", "SpellInfoCheck", "DMGActorCheck", "DMGTargetCheck", "ActorCheck", "ActionTargetCheck", "ProcTargetCheck", "NonActorTargetCheck" }
            },

            { "OnProc",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck", "DamageInfoCheck", "SpellInfoCheck", "DMGActorCheck", "DMGTargetCheck", "ActorCheck", "ActionTargetCheck", "ProcTargetCheck", "NonActorTargetCheck" }
            },

            { "AfterProc",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck", "DamageInfoCheck", "SpellInfoCheck", "DMGActorCheck", "DMGTargetCheck", "ActorCheck", "ActionTargetCheck", "ProcTargetCheck", "NonActorTargetCheck" }
            },

            { "OnEffectProc",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck", "DamageInfoCheck", "SpellInfoCheck", "DMGActorCheck", "DMGTargetCheck", "ActorCheck", "ActionTargetCheck", "ProcTargetCheck", "NonActorTargetCheck" }
            },

            { "AfterEffectProc",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck", "DamageInfoCheck", "SpellInfoCheck", "DMGActorCheck", "DMGTargetCheck", "ActorCheck", "ActionTargetCheck", "ProcTargetCheck", "NonActorTargetCheck" }
            },

            { "CanRefreshProc",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnEffectKeyboundOverride",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "DoCheckSpellProcEvent",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            },

            { "OnEffectExpireTick",
                new List<string> { "CasterCheck", "TargetCheck", "UnitOwnerCheck", "DynObjectCheck" }
            }
        };

        public static Dictionary<string, string> hooksRegisterDictionary = new Dictionary<string, string>
        {
            { "BeforeCast",                 "HookName += SpellCastFn(class::function);"                                                                 },
            { "OnCast",                     "HookName += SpellCastFn(class::function);"                                                                 },
            { "AfterCast",                  "HookName += SpellCastFn(class::function);"                                                                 },
            { "OnCheckCast",                "HookName += SpellCheckCastFn(class::function);"                                                            },
            { "OnTakePowers",               "HookName += SpellTakePowersFn(class::function);"                                                           },
            { "OnCalculateThreat",          "HookName += SpellCalculateThreatFn(class::function);"                                                      },
            { "OnCalculateChannelDuration", "HookName += SpellCalculateChannelDurationFn(class::function);"                                             },
            { "OnInterrupt",                "HookName += SpellInterruptFn(class::function);"                                                            },
            { "OnCheckInterrupt",           "HookName += SpellCheckInterruptFn(class::function);"                                                       },
            { "OnPrepare",                  "HookName += SpellOnPrepareFn(class::function);"                                                            },
            { "OnEffectLaunch",             "HookName += SpellEffectFn(class::function, EffectIndexSpecifier, EffectNameSpecifier);"                    },
            { "OnEffectLaunchTarget",       "HookName += SpellEffectFn(class::function, EffectIndexSpecifier, EffectNameSpecifier);"                    },
            { "OnEffectHit",                "HookName += SpellEffectFn(class::function, EffectIndexSpecifier, EffectNameSpecifier);"                    },
            { "OnEffectHitTarget",          "HookName += SpellEffectFn(class::function, EffectIndexSpecifier, EffectNameSpecifier);"                    },
            { "BeforeHit",                  "HookName += SpellHitFn(class::function);"                                                                  },
            { "OnHit",                      "HookName += SpellHitFn(class::function);"                                                                  },
            { "AfterHit",                   "HookName += SpellHitFn(class::function);"                                                                  },
            { "OnObjectAreaTargetSelect",   "HookName += SpellObjectAreaTargetSelectFn(class::function, EffectIndexSpecifier, TargetsNameSpecifier);"   },
            { "OnObjectTargetSelect",       "HookName += SpellObjectTargetSelectFn(class::function, EffectIndexSpecifier, TargetsNameSpecifier);"       },
            { "OnDispelAura",               "HookName += SpellOnDispelAuraFnType(class::function);"                                                     },
            { "OnDestinationTargetSelect",  "HookName += SpellDestinationTargetSelectFn(class::function, EffectIndexSpecifier, TargetsNameSpecifier);"  },

            { "DoCheckAreaTarget",          "OnEffectApply += AuraEffectApplyFn(class::function);"                                                                      },
            { "OnDispel",                   "HookName += AuraDispelFn(class::function);"                                                                                },
            { "AfterDispel",                "HookName += AuraDispelFn(class::function);"                                                                                },
            { "OnEffectApply",              "HookName += AuraEffectApplyFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier, AuraEffectHandleModes);"     },
            { "AfterEffectApply",           "HookName += AuraEffectApplyFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier, AuraEffectHandleModes);"     },
            { "OnEffectRemove",             "HookName += AuraEffectRemoveFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier, AuraEffectHandleModes);"    },
            { "AfterEffectRemove",          "HookName += AuraEffectRemoveFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier, AuraEffectHandleModes);"    },
            { "OnEffectPeriodic",           "HookName += AuraEffectPeriodicFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                         },
            { "AfterEffectPeriodic",        "HookName += AuraEffectPeriodicFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                         },
            { "OnAuraUpdate",               "HookName += AuraUpdateFn(class::function);"                                                                                },
            { "OnEffectUpdate",             "HookName += AuraEffectUpdateFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                           },
            { "OnEffectUpdatePeriodic",     "HookName += AuraEffectUpdatePeriodicFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                   },
            { "DoEffectCalcAmount",         "HookName += AuraEffectCalcAmounFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                        },
            { "DoEffectCalcFloatAmount",    "HookName += AuraEffectCalcFloatAmountFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                  },
            { "DoCalcMaxDuration",          "HookName += AuraCalcMaxDurationFn(class::function);"                                                                       },
            { "DoEffectCalcPeriodic",       "HookName += AuraEffectCalcPeriodicFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                     },
            { "DoEffectCalcSpellMod",       "HookName += AuraEffectCalcSpellModFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                     },
            { "OnEffectAbsorb",             "HookName += AuraEffectAbsorbFn(class::function, EffectIndexSpecifier);"                                                    },
            { "AfterEffectAbsorb",          "HookName += AuraEffectAbsorbFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                           },
            { "OnEffectManaShield",         "HookName += AuraEffectAbsorbFn(class::function, EffectIndexSpecifier);"                                                    },
            { "AfterEffectManaShield",      "HookName += AuraEffectAbsorbFn(class::function, EffectIndexSpecifier);"                                                    },
            { "OnEffectSplitDamage",        "HookName += AuraEffectSplitDamageFn(class::function, EffectIndexSpecifier);"                                               },
            { "DoCheckProc",                "HookName += AuraCheckProcFn(class::function);"                                                                             },
            { "DoCheckEffectProc",          "HookName += AuraCheckEffectProcFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                        },
            { "DoCalcPPM",                  "HookName += AuraProcPerMinuteFn(class::function);"                                                                         },
            { "DoCalcProcChance",           "HookName += AuraCheckEffectProcFn(class::function);"                                                                       },
            { "DoPrepareProc",              "HookName += AuraProcFn(class::function);"                                                                                  },
            { "OnProc",                     "HookName += AuraProcFn(class::function);"                                                                                  },
            { "AfterProc",                  "HookName += AuraProcFn(class::function);"                                                                                  },
            { "OnEffectProc",               "HookName += AuraEffectProcFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                             },
            { "AfterEffectProc",            "HookName += AuraEffectProcFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                             },
            { "CanRefreshProc",             "HookName += AuraCanRefreshProcFn(class::function);"                                                                        },
            { "OnEffectKeyboundOverride",   "HookName += AuraEffectKeyboundOverrideFn(class::function, EffectIndexSpecifier);"                                          },
            { "DoCheckSpellProcEvent",      "HookName += AuraCheckProcEventFnType(class::function);"                                                                    },
            { "OnEffectExpireTick",         "HookName += AuraEffectExpireTickFn(class::function, EffectIndexSpecifier, EffectAuraNameSpecifier);"                       }
        };

        public static void CreateTemplate(uint objectEntry, ListBox hooksListBox, TreeView hookBodiesTreeView, ref string preview)
        {
            if (!DB2.Db2.IsLoaded())
                DB2.Db2.Load();

            string scriptBody = "";
            string defaultName = "";
            string scriptName = "";

            defaultName = Spell.GetSpellName(objectEntry);
            if (defaultName == "")
                return;

            if (IsSpellScript(hooksListBox) && IsAuraScript(hooksListBox))
            {
                scriptName = "spell_" + NormilizeScriptName(defaultName) + "_" + objectEntry;
                scriptBody = "/// " + defaultName + " - " + objectEntry + "\r\n";
                scriptBody += "class " + scriptName + " : public " + "SpellScript" + "\r\n";
                scriptBody += "{" + "\r\n";
                scriptBody += Utils.AddSpacesCount(4) + "PrepareSpellScript(" + scriptName + ");";
                scriptBody += GetHooksBody(hooksListBox, hookBodiesTreeView);
                scriptBody += "\r\n" + Utils.AddSpacesCount(4) + "void Register() override" + "\r\n";
                scriptBody += Utils.AddSpacesCount(4) + "{" + "\r\n";
                scriptBody += GetRegisterBody(scriptName, hooksListBox);
                scriptBody += "\r\n\r\n" + Utils.AddSpacesCount(4) + "}" + "\r\n";
                scriptBody += "};\r\n\r\n";

                scriptName = "spell_" + NormilizeScriptName(defaultName) + "_aura_" + objectEntry;
                scriptBody += "/// " + defaultName + " - " + objectEntry + "\r\n";
                scriptBody += "class " + scriptName + " : public " + "AuraScript" + "\r\n";
                scriptBody += "{" + "\r\n";
                scriptBody += Utils.AddSpacesCount(4) + "PrepareAuraScript(" + scriptName + ");" + "\r\n";
                scriptBody += GetHooksBody(hooksListBox, hookBodiesTreeView, true);
                scriptBody += "\r\n\r\n" + Utils.AddSpacesCount(4) + "void Register() override" + "\r\n";
                scriptBody += Utils.AddSpacesCount(4) + "{" + "\r\n";
                scriptBody += GetRegisterBody(scriptName, hooksListBox, true);
                scriptBody += "\r\n" + Utils.AddSpacesCount(4) + "}" + "\r\n";
                scriptBody += "};";
            }
            else
            {
                scriptName = "spell_" + NormilizeScriptName(defaultName) + (IsAuraScript(hooksListBox) ? "_aura_" : "_") + objectEntry;
                scriptBody = "/// " + defaultName + " - " + objectEntry + "\r\n";
                scriptBody += "class " + scriptName + " : public " + (IsAuraScript(hooksListBox) ? "AuraScript" : "SpellScript") + "\r\n";
                scriptBody += "{" + "\r\n";
                scriptBody += Utils.AddSpacesCount(4) + (IsAuraScript(hooksListBox) ? "PrepareAuraScript(" : "PrepareSpellScript(") + scriptName + ");";
                scriptBody += GetHooksBody(hooksListBox, hookBodiesTreeView, IsAuraScript(hooksListBox));
                scriptBody += "\r\n\r\n" + Utils.AddSpacesCount(4) + "void Register() override" + "\r\n";
                scriptBody += Utils.AddSpacesCount(4) + "{" + "\r\n";
                scriptBody += GetRegisterBody(scriptName, hooksListBox, IsAuraScript(hooksListBox));
                scriptBody += "\r\n" + Utils.AddSpacesCount(4) + "}" + "\r\n";
                scriptBody += "};";
            }

            if (preview == "true")
                preview = scriptBody;
            else
            {
                Clipboard.SetText(scriptBody);
                MessageBox.Show("Template has been successfully builded and copied on your clipboard!");
            }
        }

        public static string NormilizeScriptName(string line)
        {
            Regex nonWordRegex = new Regex(@"\W+");
            string normilizedString = line;

            normilizedString = normilizedString.Replace(" ", "_");
            normilizedString = normilizedString.Replace("-", "_");

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

        private static bool IsSpellScript(ListBox listBox, string checkString = "")
        {
            List<string> spellList = new List<string>() {
            "BeforeCast",
            "OnCast",
            "AfterCast",
            "OnCheckCast",
            "OnTakePowers",
            "OnCalculateThreat",
            "OnCalculateChannelDuration",
            "OnInterrupt",
            "OnCheckInterrupt",
            "OnPrepare",
            "OnEffectLaunch",
            "OnEffectLaunchTarget",
            "OnEffectHit",
            "OnEffectHitTarget",
            "BeforeHit",
            "OnHit",
            "AfterHit",
            "OnObjectAreaTargetSelect",
            "OnObjectTargetSelect",
            "OnDispelAura",
            "OnDestinationTargetSelect" };

            if (checkString != "")
                return spellList.Contains(checkString);

            return listBox.SelectedItems.Cast<object>().Any(item => spellList.Contains(item.ToString()));
        }

        private static bool IsAuraScript(ListBox listBox, string checkString = "")
        {
            List<string> auraList = new List<string>() {
            "DoCheckAreaTarget",
            "OnDispel",
            "AfterDispel",
            "OnEffectApply",
            "AfterEffectApply",
            "OnEffectRemove",
            "AfterEffectRemove",
            "OnEffectPeriodic",
            "AfterEffectPeriodic",
            "OnAuraUpdate",
            "OnEffectUpdate",
            "OnEffectUpdatePeriodic",
            "DoEffectCalcAmount",
            "DoEffectCalcFloatAmount",
            "DoCalcMaxDuration",
            "DoEffectCalcPeriodic",
            "DoEffectCalcSpellMod",
            "OnEffectAbsorb",
            "AfterEffectAbsorb",
            "OnEffectManaShield",
            "AfterEffectManaShield",
            "OnEffectSplitDamage",
            "DoCheckProc",
            "DoCheckEffectProc",
            "DoCalcPPM",
            "DoCalcProcChance",
            "DoPrepareProc",
            "OnProc",
            "AfterProc",
            "OnEffectProc",
            "AfterEffectProc",
            "CanRefreshProc",
            "OnEffectKeyboundOverride",
            "DoCheckSpellProcEvent",
            "OnEffectExpireTick", };

            if (checkString != "")
                return auraList.Contains(checkString);

            return listBox.SelectedItems.Cast<object>().Any(item => auraList.Contains(item.ToString()));
        }

        private static bool IsHookBodiesContainItem(string itemName, TreeView hookBodiesTreeView)
        {
            return hookBodiesTreeView.Nodes.Cast<TreeNode>().Any(parentNode => parentNode.Nodes.Cast<TreeNode>().Any(childNode => childNode.Checked && childNode.Text == itemName));
        }

        private static string GetHooksBody(ListBox hooksListBox, TreeView hookBodiesTreeView, bool auraScript = false)
        {
            string body = "";

            foreach (var hook in hooksListBox.SelectedItems)
            {
                if (auraScript && !IsAuraScript(null, hook.ToString()))
                    continue;

                if (!auraScript && !IsSpellScript(null, hook.ToString()))
                    continue;

                string function = hooksDictionary[hook.ToString()];
                function = function.Replace(" (", "(");
                body += "\r\n\r\n" + Utils.AddSpacesCount(4) + function;
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

                            string l_AddBody = "";

                            if (auraScript)
                                l_AddBody += hooksAuraChecksDictionary[childNode.Text];
                            else
                                l_AddBody += hooksSpellChecksDictionary[childNode.Text];

                            if (function.Contains("bool H"))
                                body += l_AddBody.Replace("return;", "return false;");
                            else if (function.Contains("SpellCastResult"))
                                body += l_AddBody.Replace("return;", "return SPELL_FAILED_DONT_REPORT;");
                            else
                                body += l_AddBody;

                            if (!moreThanOne)
                            {
                                moreThanOne = true;
                            }
                        }
                    }
                }

                if (function.Contains("bool H"))
                    body += "\r\n" + Utils.AddSpacesCount(8) + "return true;";

                if (function.Contains("SpellCastResult"))
                    body += "\r\n" + Utils.AddSpacesCount(8) + "return SPELL_CAST_OK;";

                body += "\r\n" + Utils.AddSpacesCount(4) + "}";
            }

            return body;
        }

        private static string GetRegisterBody(string scriptName, ListBox hooksListBox, bool auraScript = false)
        {
            string body = "";

            bool first = true;
            foreach (var hook in hooksListBox.SelectedItems)
            {
                if (auraScript && !IsAuraScript(null, hook.ToString()))
                    continue;

                if (!auraScript && !IsSpellScript(null, hook.ToString()))
                    continue;

                string registerLine = hooksRegisterDictionary[hook.ToString()];
                string function = hooksDictionary[hook.ToString()];
                string[] splitFinction = function.Split(' ');

                if (registerLine.Contains("HookName"))
                    registerLine = registerLine.Replace("HookName", hook.ToString());

                registerLine = registerLine.Replace("class", scriptName);
                registerLine = registerLine.Replace("function", splitFinction[1]);

                if (first)
                    body += Utils.AddSpacesCount(8) + registerLine;
                else
                    body += "\r\n" + Utils.AddSpacesCount(8) + registerLine;

                first = false;
            }

            return body;
        }
    }
}
