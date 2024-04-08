using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(StoryletErrorMessage))]
    public class SS_StoryletErrorMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor)]
        public static IEnumerable<CodeInstruction> SSPatch_classname(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Something went wrong!..but everything's good. Click 'Done' to continue."] = "有地方出错了！…但目前一切正常。点击“完成”继续游戏"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }
    }
}
