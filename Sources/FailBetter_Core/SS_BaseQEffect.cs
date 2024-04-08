using FailBetter.Core.Interfaces;
using FailBetter.Core.QAssoc.BaseClasses;
using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(BaseQEffect))]
    public class SS_BaseQEffect
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(BaseQEffect.PossessorSatisfiesIfNoMoreThan), [typeof(IPossessesQualities), typeof(CoreResultInfo)], [ArgumentType.Normal, ArgumentType.Ref])]
        public static IEnumerable<CodeInstruction> SSPatch_PossessorSatisfiesIfNoMoreThan(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" hasn't changed, because it's higher than "] = "已发生改变，因为它高于"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(BaseQEffect.PossessorSatisfiesOnlyIfAtLeast), [typeof(IPossessesQualities), typeof(CoreResultInfo)], [ArgumentType.Normal, ArgumentType.Ref])]
        public static IEnumerable<CodeInstruction> SSPatch_PossessorSatisfiesOnlyIfAtLeast(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" hasn't changed, because it's lower than "] = "已发生改变，因为它低于"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }
    }
}
