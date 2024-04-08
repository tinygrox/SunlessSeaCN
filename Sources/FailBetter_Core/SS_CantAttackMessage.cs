using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(CantAttackMessage))]
    public class SS_CantAttackMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(string)])]
        public static IEnumerable<CodeInstruction> SSPatch_CantAttackMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>() { ["You can't attack this target because "] = "你无法攻击此目标，因为" };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }
    }
}
