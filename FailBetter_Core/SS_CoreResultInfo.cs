using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(CoreResultInfo))]
    public class SS_CoreResultInfo
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CoreResultInfo.GetCombatMessages))]
        public static IEnumerable<CodeInstruction> SSPatch_GetCombatMessages(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                Dictionary<string, string> trans = new()
                {
                    [" and "] = "和"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }
    }
}
