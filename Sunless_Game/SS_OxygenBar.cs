using HarmonyLib;
using Sunless.Game.UI.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(OxygenBar))]
    public class SS_OxygenBar
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(OxygenBar.GetOxygenTooltip))]
        public static IEnumerable<CodeInstruction> SSPatch_GetOxygenTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Oxygen: ", "氧气：");
                SS_Utility.ILReplacer(ref instructions, "\n\nSurfacing will refill your Oxygen tanks", "\n\n上浮后会重新补满氧气罐");

            });
            return instructions;
        }
    }
}
