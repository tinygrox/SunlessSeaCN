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
    [HarmonyPatch(typeof(QualityUnlockedWithoutLevelRequirementIcon))]
    public class SS_QualityUnlockedWithoutLevelRequirementIcon
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(BaseQRequirement), typeof(IPossessesQualities)])]
        public static IEnumerable<CodeInstruction> SSPatch_QualityUnlockedWithoutLevelRequirementIcon(IEnumerable<CodeInstruction> instructions)
        {
            Dictionary<string, string> trans = new()
            {
                ["Unlocked when "] = "当",
                [" is:\n"] = "满足下列条件时解锁：\n"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
    }
}
