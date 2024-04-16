using HarmonyLib;
using Sunless.Game.UI.HUD;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(TerrorBar))]
    public class SS_TerrorBar
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(TerrorBar.Update), new[] { typeof(int), typeof(float), typeof(int) })]
        public static IEnumerable<CodeInstruction> SSPatch_Update(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Terror: ", "恐惧：");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("GetGloomBarColour", new[] { typeof(int) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetGloomBarColour(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "You are far from light or hope. Terror is increasing rapidly.", "你远离光明与希望，恐惧正如潮水般涌来。");
                SS_Utility.ILReplacer(ref instructions, "The zee is wide and dark. Terror is increasing.", "大海宽阔而幽暗，恐惧正悄然滋生。");
                SS_Utility.ILReplacer(ref instructions, "The terror of the zee is less here.", "海域此处，恐惧少些。");
            });
            return instructions;
        }
    }
}
