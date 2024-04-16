using FailBetter.Core;
using FailBetter.Core.Result.QualityChangeMessages;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(StandardQualityChangeMessage))]
    public class SS_StandardQualityChangeMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality), typeof(int), typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_StandardQualityChangeMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["{0} remains unchanged, at {1}"] = "{0}保持不变，仍为{1}",
                    ["{0} +0 (now {1})"] = "{0} +0 (现在有{1})",
                    ["gained "] = "获得了",
                    ["lost "] = "失去了",
                    ["You've {0} x {1} (new total {2})."] = "你{0} x {1} (现在有{2})。",
                    ["I've {0} x {1} (new total {2})."] = "我{0} x {1} (现在有{2}).",
                    ["You now have "] = "你现在有",
                    ["I now have "] = "我现在有",
                    ["{0} {1} (now {2})"] = "{0}{1} (现在有{2})",
                    ["[This is a metaquality! It will appear on your user profile, and may unlock new starting options in other worlds.]"] = "[这是一个元特质！它将出现在您的用户个人资料中，并可能在其他世界中解锁新的开始选项。]",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
