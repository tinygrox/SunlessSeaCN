using FailBetter.Core;
using FailBetter.Core.Result;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(SecondChanceUsedMessage))]
    public class SS_SecondChanceUsedMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality)])]
        public static IEnumerable<CodeInstruction> SSPatch_SecondChanceUsedMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["...but you used up "] = "…但你用掉了",
                    [" to get a second chance!"] = "来换取第二次机会！",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
