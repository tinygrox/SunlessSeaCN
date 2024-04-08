using FailBetter.Core;
using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(StolenTrophyGiverMessage))]
    public class SS_StolenTrophyGiverMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality)])]
        public static IEnumerable<CodeInstruction> SSPatch_StolenTrophyGiverMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["CONGRATULATIONS! You have stolen something special from your victim - <strong>"] = "<b>恭喜</b>！你从你的受害者 - <strong>",
                    ["</strong>!"] = "</strong>那里偷走了特殊物品。"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
