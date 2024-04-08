using HarmonyLib;
using Sunless.Game.Entities.Geography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(TileLabel))]
    public class SS_TileLabel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(TileLabel.GetDescription))]
        public static IEnumerable<CodeInstruction> SSPatch_GetDescription(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Discovered  ", "发现了");
            return instructions;
        }
    }
}
