using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.UI.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(CombatItemIcon))]
    public class SS_CombatItemIcon
    {
        [HarmonyTranspiler]
        [HarmonyPatch("GetInteractions", [typeof(Quality), typeof(bool)])]
        public static IEnumerable<CodeInstruction> SSPatch_GetInteractions(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Assay"] = "估值",
                    ["Use"] = "使用"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
