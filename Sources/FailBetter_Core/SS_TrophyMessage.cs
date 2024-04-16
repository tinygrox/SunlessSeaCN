using FailBetter.Core;
using FailBetter.Core.Result;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(TrophyMessage))]
    public class SS_TrophyMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality), typeof(Quality)])]
        public static IEnumerable<CodeInstruction> SSPatch_classname(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Your "] = "你的",
                    [" has provided you with 1 x "] = "为你提供了 1 x",
                    [" from your successful kill."] = "来自于你的成功击杀。",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
