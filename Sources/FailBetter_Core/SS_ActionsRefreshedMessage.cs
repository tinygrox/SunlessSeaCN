using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(ActionsRefreshedMessage))]
    public class SS_ActionsRefreshedMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(int), typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_ActionsRefreshedMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                Dictionary<string, string> trans = new()
                {
                    ["Your actions have been refreshed!"] = "你的行动已经被刷新！"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }
    }
}
