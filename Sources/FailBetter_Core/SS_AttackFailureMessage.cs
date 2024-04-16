using FailBetter.Core;
using FailBetter.Core.Result;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(AttackFailureMessage))]
    public class SS_AttackFailureMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Character)])]
        public static IEnumerable<CodeInstruction> SSPatch_AttackFailureMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                Dictionary<string, string> trans = new()
                {
                    ["Lamentably, your attack on "] = "很遗憾，你针对",
                    [" failed."] = "发动的攻击失败了。",
                    ["Better luck another time. Have you tried using the Hunt option?"] = "下次好运。你试过使用“狩猎”选项了吗？"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
