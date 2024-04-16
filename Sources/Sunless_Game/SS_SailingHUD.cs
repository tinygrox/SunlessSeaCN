using HarmonyLib;
using Sunless.Game.UI.HUD;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(SailingHUD))]
    public class SS_SailingHUD
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(SailingHUD.GetHoldTooltip))]
        public static IEnumerable<CodeInstruction> SSPatch_GetHoldTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Hold capacity: ", "货舱容量：");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(SailingHUD.GetHullTooltip))]
        public static IEnumerable<CodeInstruction> SSPatch_GetHullTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Hull: ", "船体值：");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("GetHullText")]
        public static IEnumerable<CodeInstruction> SSPatch_GetHullText(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Hull: ", "船体值：");
            });
            return instructions;
        }
    }
}
