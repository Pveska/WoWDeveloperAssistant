using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WoWDeveloperAssistant.DBC.Structures;

namespace WoWDeveloperAssistant.Achievements
{
    public static class ModifierTreesHandler
    {
        public static void ShowModifierTreeRequirements(MainForm mainForm)
        {
            if (!DBC.DBC.IsLoaded())
            {
                DBC.DBC.Load();
            }

            DBC.DBC.ModifierTree.TryGetValue(int.Parse(mainForm.textBox_ModifierTrees_ModifierTreeId.Text), out var modifierTree);
            if (modifierTree == null)
                return;

            mainForm.label_ModifierTrees_Type.Text = $"Type: { (AchievementEnums.CriteriaAdditionalCondition)modifierTree.Type }";
            mainForm.label_ModifierTrees_Amount.Text = $"Amount: { modifierTree.Amount }";
            mainForm.label_ModifierTrees_Operator.Text = $"Operator: { (AchievementEnums.ModifierTreeOperator)modifierTree.Operator }";
            mainForm.label_ModifierTrees_Type.Text = $"Asset: { modifierTree.Asset }";
            mainForm.label_ModifierTrees_Type.Text = $"Secondary Asset: { modifierTree.SecondaryAsset }";
            mainForm.label_ModifierTrees_Type.Text = $"Tertiary Asset: { modifierTree.TertiaryAsset }";
            Achievements.AchievementsHandler.FillTreeWithModifiersChildNodes(modifierTree.ID, mainForm.treeView_ModifierTrees_ModifierTrees);
        }
    }
}
