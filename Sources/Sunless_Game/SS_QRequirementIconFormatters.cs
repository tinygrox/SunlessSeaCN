using FailBetter.Core.Interfaces;
using FailBetter.Core.QAssoc.BaseClasses;
using HarmonyLib;
using Sunless.Game.Formatters;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(QRequirementIconFormatters))]
    public class SS_QRequirementIconFormatters
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(QRequirementIconFormatters.RequirementTextFor), [typeof(BaseQRequirement), typeof(bool), typeof(int?), typeof(IPossessesQualities)])]
        public static IEnumerable<CodeInstruction> SSPatch_RequirementTextFor(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" and "] = "且",
                    [" no more than "] = "不高于"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("GetRequirementObjectText", [typeof(BaseQRequirement)])]
        public static IEnumerable<CodeInstruction> SSPatch_GetRequirementObjectText(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" (them)"] = "(他们)",
                    [" (you)"] = "(你)"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("QualityRequirementWithoutLevelAsRange", [typeof(BaseQRequirement), typeof(int?), typeof(IPossessesQualities)])]
        public static IEnumerable<CodeInstruction> SSPatch_QualityRequirementWithoutLevelAsRange(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "(you have this)", "(你已拥有)");
            });
            return instructions;
        }
    }
}
