using FailBetter.Core;
using FailBetter.Core.Result.QualityChangeMessages;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(QualityExplicitlySetMessage))]
    public class SS_QualityExplicitlySetMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality), typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_QualityExplicitlySetMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["You now have "] = "你现在拥有",
                    [" of this: '"] = "的这个：'",

                    ["You have a new Accomplishment..."] = "你拥有了一段新的历往…",
                    ["An occurrence! Your '"] = "事件！你的'",
                    ["' Quality is now "] = "'特质现在为",
                    ["' has been reset: a conclusion, or a new beginning?"] = "'已经被重置：是结局，亦或新的开始？",
                    ["You no longer have any of this: '"] = "你不再拥有：'",
                    ["Your '"] = "你的'",
                    ["' Quality has gone!"] = "'特质已经没有了！",
                    ["[This is a metaquality! It will appear on your user profile, and may unlock new starting options in other worlds.]"] = "[这是一个元特质！它将出现在您的用户个人资料中，并可能在其他世界中解锁新的开始选项。]",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
