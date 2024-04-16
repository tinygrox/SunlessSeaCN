using HarmonyLib;
using Sunless.Game.UI.HUD;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(RepairIndication))]
    public class SS_RepairIndication
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(RepairIndication.UpdateQuantities), new[] { typeof(int), typeof(int), typeof(int) })]
        public static IEnumerable<CodeInstruction> SSPatch_UpdateQuantities(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "<b>Hull:</b> ", "<b>船体值：</b>");
                SS_Utility.ILReplacer(ref instructions, "<b>Supplies:</b> ", "<b>物资：</b>");
            });
            return instructions;
        }
    }
}
