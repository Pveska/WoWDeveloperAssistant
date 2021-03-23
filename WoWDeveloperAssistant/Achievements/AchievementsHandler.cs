using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WoWDeveloperAssistant.DBC.Structures;

namespace WoWDeveloperAssistant.Achievements
{
    public static class AchievementsHandler
    {
        public static void ShowAchievementRequirements(MainForm mainForm)
        {
            if (!DBC.DBC.IsLoaded())
            {
                DBC.DBC.Load();
            }

            DBC.DBC.Achievement.TryGetValue(int.Parse(mainForm.textBox_Achievements_AchievementId.Text), out var achievement);
            if (achievement == null)
                return;

            DBC.DBC.CriteriaTree.TryGetValue((int)achievement.CriteriaTree, out var criteriaTree);
            if (criteriaTree == null)
                return;

            mainForm.label_Achievements_AchievementName.Text = "Achievement Name: " + achievement.Description;
            mainForm.label_Achievements_AchievementFaction.Text = "Achievement Faction: " + (AchievementEnums.AchievementFaction) achievement.Faction;
            mainForm.label_Achievements_AchievementFlags.Text = "Achievement Flags: " + GetAchievementFlagNames(achievement.Flags);

            mainForm.label_Achievements_CriteriaTreeId.Text = "CriteriaTree Id: " + achievement.CriteriaTree;
            mainForm.label_Achievements_CriteriaTreeName.Text = "CriteriaTree Name: " + criteriaTree.Description;
            mainForm.label_Achievements_CriteriaTreeAmount.Text = "CriteriaTree Amount: " + criteriaTree.Amount;
            mainForm.label_Achievement_CriteriaTreeOperator.Text = "CriteriaTree Operator: " + (AchievementEnums.CriteriaTreeOperator)criteriaTree.Operator;
            FillTreeWithCriteriaTreeChildNodes(achievement.CriteriaTree, mainForm.treeView_Achievements_ChildNodes);
        }

        private static string GetAchievementFlagNames(int flags)
        {
            if (flags == 0)
                return "0";

            string flagNames = "";

            foreach (uint flag in Enum.GetValues(typeof(AchievementEnums.AchievementFlags)))
            {
                if ((flags & flag) != 0)
                {
                    if (flagNames == "")
                    {
                        flagNames += (AchievementEnums.AchievementFlags)flag;
                    }
                    else
                    {
                        flagNames += ", " + (AchievementEnums.AchievementFlags)flag;
                    }
                }
            }

            return flagNames;
        }

        private static IEnumerable<KeyValuePair<int, CriteriaTreeEntry>> GetCriteriaTreeChildNodes(uint criteriaTreeId)
        {
            return DBC.DBC.CriteriaTree.Where(x => x.Value.Parent == criteriaTreeId);
        }

        private static void FillTreeWithCriteriaTreeChildNodes(uint criteriaTreeId, TreeView treeWiev)
        {
            treeWiev.Nodes.Clear();

            foreach (var node in GetCriteriaTreeChildNodes(criteriaTreeId))
            {
                TreeNode treeNode = new TreeNode(node.Key.ToString());
                treeNode.Nodes.Add("Amount: " + node.Value.Amount);
                treeNode.Nodes.Add("Operator: " + (AchievementEnums.CriteriaTreeOperator)node.Value.Operator);
                treeNode.Nodes.Add("Flags: " + GetCriteriaTreeFlagNames(node.Value.Flags));
                FillTreeWithCriteriaTreeChildNodes((uint)node.Key, treeNode);
                treeWiev.Nodes.Add(treeNode);
            }
        }

        private static void FillTreeWithCriteriaTreeChildNodes(uint criteriaTreeId, TreeNode treeNode)
        {
            foreach (var nodeIter in GetCriteriaTreeChildNodes(criteriaTreeId))
            {
                TreeNode node = new TreeNode(nodeIter.Key.ToString());
                node.Nodes.Add("Amount: " + nodeIter.Value.Amount);
                node.Nodes.Add("Operator: " + (AchievementEnums.CriteriaTreeOperator)nodeIter.Value.Operator);
                node.Nodes.Add("Flags: " + GetCriteriaTreeFlagNames(nodeIter.Value.Flags));
                FillTreeWithCriteriaTreeChildNodes((uint)nodeIter.Key, node);
                treeNode.Nodes.Add(node);
            }
        }

        public static void FillTreeWithCriterias(uint criteriaTreeId, TreeView treeWiev, bool clearBeforeAdd)
        {
            DBC.DBC.Criteria.TryGetValue((int)GetCriteriaIdFromCriteriaTree(criteriaTreeId), out var criteria);
            if (criteria == null)
                return;

            if (clearBeforeAdd)
            {
                treeWiev.Nodes.Clear();
            }

            TreeNode treeNode = new TreeNode(GetCriteriaIdFromCriteriaTree(criteriaTreeId).ToString());
            treeNode.Nodes.Add("Type: " + (AchievementEnums.CriteriaTypes)criteria.Type);
            treeNode.Nodes.Add("Asset: " + criteria.Asset);
            treeNode.Nodes.Add("Start Event: " + (AchievementEnums.CriteriaTimedTypes)criteria.StartEvent);
            treeNode.Nodes.Add("Start Asset: " + criteria.StartAsset);
            treeNode.Nodes.Add("Start Timer: " + criteria.StartTimer);
            treeNode.Nodes.Add("Fail Event: " + (AchievementEnums.CriteriaTimedTypes)criteria.FailEvent);
            treeNode.Nodes.Add("Fail Asset: " + criteria.FailAsset);
            treeNode.Nodes.Add("Flags: " + GetCriteriaFlagNames(criteria.Flags));
            treeWiev.Nodes.Add(treeNode);
        }

        private static uint GetCriteriaIdFromCriteriaTree(uint criteriaTreeId)
        {
            if (DBC.DBC.CriteriaTree.ContainsKey((int)criteriaTreeId))
                return DBC.DBC.CriteriaTree[(int)criteriaTreeId].CriteriaID;

            return 0;
        }

        private static string GetCriteriaTreeFlagNames(int flags)
        {
            if (flags == 0)
                return "0";

            string flagNames = "";

            foreach (uint flag in Enum.GetValues(typeof(AchievementEnums.CriteriaTreeFlags)))
            {
                if ((flags & flag) != 0)
                {
                    if (flagNames == "")
                    {
                        flagNames += (AchievementEnums.CriteriaTreeFlags)flag;
                    }
                    else
                    {
                        flagNames += ", " + (AchievementEnums.CriteriaTreeFlags)flag;
                    }
                }
            }

            return flagNames;
        }

        private static string GetCriteriaFlagNames(int flags)
        {
            if (flags == 0)
                return "0";

            string flagNames = "";

            foreach (uint flag in Enum.GetValues(typeof(AchievementEnums.CriteriaFlags)))
            {
                if ((flags & flag) != 0)
                {
                    if (flagNames == "")
                    {
                        flagNames += (AchievementEnums.CriteriaFlags)flag;
                    }
                    else
                    {
                        flagNames += ", " + (AchievementEnums.CriteriaFlags)flag;
                    }
                }
            }

            return flagNames;
        }

        public static void FillTreeWithModifiers(uint criteriaTreeId, TreeView treeWiev, bool clearBeforeAdd)
        {
            DBC.DBC.ModifierTree.TryGetValue((int)GetModifierTreeForCriteria(criteriaTreeId), out var modifierTree);
            if (modifierTree == null)
                return;

            if (clearBeforeAdd)
            {
                treeWiev.Nodes.Clear();
            }

            TreeNode treeNode = new TreeNode(GetModifierTreeForCriteria(criteriaTreeId).ToString());
            treeNode.Nodes.Add("Operator: " + (AchievementEnums.ModifierTreeOperator)modifierTree.Operator);
            treeNode.Nodes.Add("Amount: " + modifierTree.Amount);
            treeNode.Nodes.Add("Type: " + (AchievementEnums.CriteriaAdditionalCondition)modifierTree.Type);
            treeNode.Nodes.Add("Asset: " + modifierTree.Asset);
            treeNode.Nodes.Add("SecondaryAsset: " + modifierTree.SecondaryAsset);
            treeNode.Nodes.Add("TertiaryAsset: " + modifierTree.TertiaryAsset);
            treeWiev.Nodes.Add(treeNode);
        }

        private static uint GetModifierTreeForCriteria(uint criteriaId)
        {
            if (DBC.DBC.Criteria.ContainsKey((int)criteriaId))
                return DBC.DBC.Criteria[(int)criteriaId].ModifierTreeID;

            return 0;
        }

        public static void FillTreeWithModifiersChildNodes(uint modifierTreeId, TreeView treeWiev)
        {
            foreach (var node in GetModifierTreeChildNodes(modifierTreeId))
            {
                TreeNode treeNode = new TreeNode(node.Key.ToString());
                treeNode.Nodes.Add("Operator: " + (AchievementEnums.ModifierTreeOperator)node.Value.Operator);
                treeNode.Nodes.Add("Amount: " + node.Value.Amount);
                treeNode.Nodes.Add("Type: " + (AchievementEnums.CriteriaAdditionalCondition)node.Value.Type);
                treeNode.Nodes.Add("Asset: " + node.Value.Asset);
                treeNode.Nodes.Add("SecondaryAsset: " + node.Value.SecondaryAsset);
                treeNode.Nodes.Add("TertiaryAsset: " + node.Value.TertiaryAsset);
                FillTreeWithModifiersChildNodes((uint)node.Key, treeNode);
                treeWiev.Nodes.Add(treeNode);
            }
        }

        public static void FillTreeWithModifiersChildNodes(uint modifierTreeId, TreeNode treeNode)
        {
            foreach (var nodeIter in GetModifierTreeChildNodes(modifierTreeId))
            {
                TreeNode node = new TreeNode(nodeIter.Key.ToString());
                node.Nodes.Add("Operator: " + (AchievementEnums.ModifierTreeOperator)nodeIter.Value.Operator);
                node.Nodes.Add("Amount: " + nodeIter.Value.Amount);
                node.Nodes.Add("Type: " + (AchievementEnums.CriteriaAdditionalCondition)nodeIter.Value.Type);
                node.Nodes.Add("Asset: " + nodeIter.Value.Asset);
                node.Nodes.Add("SecondaryAsset: " + nodeIter.Value.SecondaryAsset);
                node.Nodes.Add("TertiaryAsset: " + nodeIter.Value.TertiaryAsset);
                FillTreeWithModifiersChildNodes((uint)nodeIter.Key, node);
                treeNode.Nodes.Add(node);
            }
        }

        private static IEnumerable<KeyValuePair<int, ModifierTreeEntry>> GetModifierTreeChildNodes(uint modifierTreeId)
        {
            return DBC.DBC.ModifierTree.Where(x => x.Value.Parent == modifierTreeId);
        }
    }
}
