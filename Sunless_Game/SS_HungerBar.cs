using HarmonyLib;
using Sunless.Game.UI.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(HungerBar))]
    public class SS_HungerBar
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(HungerBar.GetSuppliesTooltip))]
        public static IEnumerable<CodeInstruction> SSPatch_GetSuppliesTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Supplies: ", "物资：");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(HungerBar.GetHungerTooltip))]
        public static IEnumerable<CodeInstruction> SSPatch_GetHungerTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Hunger: ", "饥饿：");
            });
            return instructions;
        }
    }
}
