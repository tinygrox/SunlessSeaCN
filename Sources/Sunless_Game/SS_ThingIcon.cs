using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.UI.Icons;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(ThingIcon))]
    public class SS_ThingIcon
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
