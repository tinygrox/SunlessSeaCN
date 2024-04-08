using FailBetter.Core.Interfaces;
using FailBetter.Core.QAssoc.BaseClasses;
using HarmonyLib;
using Sunless.Game.Formatters.RequirementIcons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(QualityUnlockedWithLevelRequirementIcon))]
    public class SS_QualityUnlockedWithLevelRequirementIcon
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(BaseQRequirement), typeof(IPossessesQualities)])]
        public static IEnumerable<CodeInstruction> SSPatch_QualityUnlockedWithLevelRequirementIcon(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                Dictionary<string, string> trans = new()
                {
                    ["Unlocked with "] = "需要",
                    [" (you have "] = " (你拥有"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
