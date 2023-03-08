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
            None              = 0x00000000,
            Gossip            = 0x00000001,
            QuestGiver        = 0x00000002,
            Unknown1          = 0x00000004,
            Unknown2          = 0x00000008,
            Trainer           = 0x00000010,
            ClassTrainer      = 0x00000020,
            ProfessionTrainer = 0x00000040,
            Vendor            = 0x00000080,
            AmmoVendor        = 0x00000100,
            FoodVendor        = 0x00000200,
            PoisonVendor      = 0x00000400,
            ReagentVendor     = 0x00000800,
            Repair            = 0x00001000,
            FlightMaster      = 0x00002000,
            SpiritHealer      = 0x00004000,
            SpiritGuide       = 0x00008000,
            InnKeeper         = 0x00010000,
            Banker            = 0x00020000,
            Petitioner        = 0x00040000,
            TabardDesigner    = 0x00080000,
            BattleMaster      = 0x00100000,
            Auctioneer        = 0x00200000,
            StableMaster      = 0x00400000,
            GuildBanker       = 0x00800000,
            SpellClick        = 0x01000000,
            PlayerVehicle     = 0x02000000,
            Mailbox           = 0x04000000,
            ArtifactForge     = 0x08000000,
            Transmogrifier    = 0x10000000,
            Vaultkeeper       = 0x20000000,
            WildBattlePet     = 0x40000000,
            BlackMarket       = 0x80000000
        };

        enum NpcFlags2 : long
        {
            None                    = 0x00000,
            ItemUpgrade             = 0x00001,
            GarrisonArchitect       = 0x00002,
            AIObstacle              = 0x00004,
            Steering                = 0x00008,
            GarrisonShipmentCrafter = 0x00010,
            GarrisonMissionNPC      = 0x00020,
            TradeskillNPC           = 0x00040,
            BlackMarket             = 0x00080,
            Unk100                  = 0x00100,
            GarrisonTalentNPC       = 0x00200,
            ContributionCollector   = 0x00400,
            Unk800                  = 0x00800,
            Unk1000                 = 0x01000,
            PlayerAiImitation       = 0x02000,
            AzeriteReforge          = 0x04000,
            ExpeditionsMap          = 0x08000,
            Unk10000                = 0x10000,
            Unk20000                = 0x20000,
            Barber                  = 0x40000 // Show icon on minimap
        };

        public enum UnitFlags : long
        {
            None                       = 0,
            NotClientControlled        = 0x1,
            Spawning                   = 0x2,
            RemoveClientControl        = 0x4,
            PlayerControlled           = 0x8,
            Unk4                       = 0x10,
            Preparation                = 0x20,
            Unk6                       = 0x40,
            NoAttack                   = 0x80,
            ImmunePC                   = 0x100,
            ImmuneNPC                  = 0x200,
            Looting                    = 0x400,
            PetIsAttackingTarget       = 0x800,
            PVP                        = 0x1000,
            Silenced                   = 0x2000,
            CannotSwim                 = 0x4000,
            CanSwim                    = 0x8000,
            NoAttack2                  = 0x10000,
            Pacified                   = 0x20000,
            Stunned                    = 0x40000,
            AffectingCombat            = 0x80000,
            OnTaxi                     = 0x100000,
            Disarmed                   = 0x200000,
            Confused                   = 0x400000,
            Feared                     = 0x800000,
            PossessedByPlayer          = 0x1000000,
            Uninteractible             = 0x2000000,
            Skinnable                  = 0x4000000,
            Mount                      = 0x8000000,
            PreventKneelingWhenLooting = 0x10000000,
            PreventEmotes              = 0x20000000,
            Sheath                     = 0x40000000,
            Immune                     = 0x80000000,
        };

        enum UnitFlags2 : long
        {
            FeignDeath                                = 0x00000001, ///<
            HideBody                                  = 0x00000002, ///< Hide unit model (show only player equip)
            IgnoreReputation                          = 0x00000004, ///<
            ComprehendLang                            = 0x00000008, ///<
            MirrorImage                               = 0x00000010, ///<
            InstantlyDontFadeIn                       = 0x00000020, ///< Unit model instantly appears when summoned (does not fade in)
            ForceMovement                             = 0x00000040, ///<
            DisarmOffhand                             = 0x00000080, ///<
            DisablePredStats                          = 0x00000100, ///< Player has disabled predicted stats (Used by raid frames)
            AllowChangingTalents                      = 0x00000200, ///< Allows changing talents outside rest area
            DisarmRanged                              = 0x00000400, ///< this does not disable ranged weapon display (maybe additional flag needed?)
            RegeneratePower                           = 0x00000800, ///<
            RestrictPartyInteraction                  = 0x00001000, ///< Restrict interaction to party or raid
            PreventSpellClick                         = 0x00002000, ///< Prevent spellclick
            InteractWhileHostile                      = 0x00004000, ///<
            CannotTurn                                = 0x00008000, ///<
            Unk2                                      = 0x00010000, ///<
            PlayDeathAnim                             = 0x00020000, ///< Plays special death animation upon death
            AllowCheatSpells                          = 0x00040000, ///< allows casting spells with AttributesEx7 & SPELL_ATTR7_DEBUG_SPELL
            SuppressHighlightWhenTargetedOrMousedOver = 0x00080000, ///< Suppress highlight when targeted or moused over
            TreatAsRaidUnitForHelpfulSpells           = 0x00100000, ///< Treat as Raid Unit For Helpful Spells (Instances ONLY)
            LargeAoi                                  = 0x00200000, ///<
            GiganticAoi                               = 0x00400000, ///<
            NoActions                                 = 0x00800000, ///<
            AiWillOnlySwimIfTargetSwims               = 0x01000000, ///<
            DontGenerateCombatLogWhenEngagedWithNpcs  = 0x02000000, ///<
            UntargetableByClient                      = 0x04000000, ///< Untargetable By Client (even in GM mode)
            AttackerIgnoresMinimumRanges              = 0x08000000, ///<
            UninteractibleIfHostile                   = 0x10000000, ///< Cant target, hide highlight, hide name (work on faction 14/7 but not 35)
            Unk13                                     = 0x20000000, ///<
            InfiniteAoi                               = 0x40000000, ///<
            Unk15                                     = 0x80000000  ///<
        };

        enum UnitFlags3 : long
        {
            PassiveAi               = 0x00000001,
            Unconscious             = 0x00000002,
            CanFightWithoutDismount = 0x00000004,
            Unk4                    = 0x00000008,
            Unk5                    = 0x00000010,
            Unk6                    = 0x00000020,
            Unk7                    = 0x00000040,
            Unk8                    = 0x00000080,
            Unk9                    = 0x00000100,
            Unk10                   = 0x00000200,
            Unk11                   = 0x00000400,
            Unk12                   = 0x00000800,
            Unk13                   = 0x00001000,
            DisplayAsCorpse         = 0x00002000,
            Unk15                   = 0x00004000,
            Unk16                   = 0x00008000,
            Unk17                   = 0x00010000,
            AlreadyMinedOrSkinned   = 0x00020000,
            Unk19                   = 0x00040000,
            Unk20                   = 0x00080000,
            Unk21                   = 0x00100000,
            Unk22                   = 0x00200000,
            Unk23                   = 0x00400000,
            Unk24                   = 0x00800000,
            Unk25                   = 0x01000000,
            Unk26                   = 0x02000000,
            Unk27                   = 0x04000000,
            Unk28                   = 0x08000000,
            Unk29                   = 0x10000000,
            Unk30                   = 0x20000000,
            Unk31                   = 0x40000000,
            Unk32                   = 0x80000000
        };

        enum DynamicFlags : long
        {
            None                  = 0x0000,
            HideModel             = 0x0001,
            Lootable              = 0x0002,
            TrackUnit             = 0x0004,
            Tapped                = 0x0008,
            TappedByPlayer        = 0x0010,
            EmpathyInfo           = 0x0020,
            AppearDead            = 0x0040,
            ReferAFriendLinked    = 0x0080,
            TappedByAllThreatList = 0x0100
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
            None                          = 0x00000000,
            Tameable                      = 0x00000001,
            VisibleToGhosts               = 0x00000002,
            BossMob                       = 0x00000004,
            DoNotPlayWoundAnim            = 0x00000008,
            NoFactionTooltip              = 0x00000010,
            MoreAudible                   = 0x00000020, // sound related
            SpellAttackable               = 0x00000040,
            InteractWhileDead             = 0x00000080,
            SkinWithHerbalism             = 0x00000100,
            SkinWithMining                = 0x00000200,
            NoDeathMessage                = 0x00000400,
            AllowMountedCombat            = 0x00000800,
            CanAssist                     = 0x00001000,
            NoPetBar                      = 0x00002000,
            MaskUID                       = 0x00004000,
            SkinWithEngineering           = 0x00008000,
            TameableExotic                = 0x00010000,
            UseModelCollisionSize         = 0x00020000,
            AllowInteractionWhileInCombat = 0x00040000,
            CollideWithMissiles           = 0x00080000,
            NoNamePlate                   = 0x00100000,
            DoNotPlayMountedAnimations    = 0x00200000,
            LinkAll                       = 0x00400000,
            InteractOnlyWithCreator       = 0x00800000,
            DoNotPlayUnitEventSounds      = 0x01000000,
            HasNoShadowBlob               = 0x02000000,
            TreatAsRaidUnit               = 0x04000000,
            ForceGossip                   = 0x08000000,
            DoNotSheathe                  = 0x10000000,
            DoNotTargetOnInteraction      = 0x20000000,
            DoNotRenderObjectName         = 0x40000000,
            QuestBoss                     = 0x80000000 // not verified
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
            unitFlagsDs = SQLModule.WorldSelectQuery(unitFlagsSqlQuery);
            typeFlagsDs = SQLModule.WorldSelectQuery(typeFlagsSqlQuery);
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
