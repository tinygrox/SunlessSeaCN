using FailBetter.Core;
using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(SecondChanceSpentMessage))]
    public class SS_SecondChanceSpentMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality)])]
        public static IEnumerable<CodeInstruction> SSPatch_SecondChanceSpentMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["You succeeded! ...but used up {0} {1} anyway"] = "你成功了！…但还是用掉了{0}{1}"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }
    }
}
