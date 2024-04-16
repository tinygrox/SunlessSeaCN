using FailBetter.Core;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(FeedMessage))]
    public class SS_FeedMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(FeedMessage.Ago))]
        public static IEnumerable<CodeInstruction> SSPatch_Ago(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" days ago"] = "天前",
                    [" day ago"] = "天前",
                    [" hours ago"] = "时前",
                    [" hour ago"] = "时前",
                    [" minutes ago"] = "分前",
                    [" minute ago"] = "分前",
                    [" seconds ago"] = "秒前",
                    [" second ago"] = "秒前",
                    ["just now"] = "刚刚"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
