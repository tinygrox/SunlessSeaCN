using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(NotEnoughActionsMessage))]
    public class SS_NotEnoughActionsMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor)]
        public static IEnumerable<CodeInstruction> SSPatch_classname(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["<p>You don't have enough actions for that! Your options:\r\n<br/>&nbsp;&nbsp;- Wait patiently for your action candle to refresh.\r\n<br/>&nbsp;&nbsp;- [fatelink] to use\r\n<br/>&nbsp;&nbsp;- [sharelink]\r\n\r\n</p>"] = "<p>你没有足够的行动点数！你的选择：\r\n<br/>&nbsp;&nbsp;- 耐心等待你的行动蜡烛刷新。\r\n<br/>&nbsp;&nbsp;- 使用[fatelink]\r\n<br/>&nbsp;&nbsp;- [sharelink]\r\n\r\n</p>",
                    ["You need more actions!"] = "你需要更多的行动点数！"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }
    }
}
