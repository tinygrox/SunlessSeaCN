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
    [HarmonyPatch(typeof(StatusLockedWithoutLevelRequirementIcon))]
    public class SS_StatusLockedWithoutLevelRequirementIcon
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(BaseQRequirement), typeof(IPossessesQualities)])]
        public static IEnumerable<CodeInstruction> SSPatch_StatusLockedWithoutLevelRequirementIcon(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Unlocked when "] = "当",
                    [" is:\n"] = "满足下列条件时解锁：\n"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
