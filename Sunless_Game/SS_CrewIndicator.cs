using HarmonyLib;
using Sunless.Game.UI.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(CrewIndicator))]
    public class SS_CrewIndicator
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CrewIndicator.GetTooltip))]
        public static IEnumerable<CodeInstruction> SSPatch_GetTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["If less than half your crew remains, you may not move at full speed."] = "如果你的船员人数不足一半，你将无法全速前进。",
                    ["If less than a quarter of your crew remains, Terror increases faster."] = "如果你的船员人数不足四分之一，恐惧会增加得更快。"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
