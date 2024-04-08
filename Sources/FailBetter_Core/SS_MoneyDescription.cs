using FailBetter.Core.Utility;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(MoneyDescription))]
    public class SS_MoneyDescription
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MoneyDescription.LongFormatAsEchoes), [typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_MoneyDescription(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" penny"] = " 便士",
                    [" pence"] = " 便士",
                    ["1 Echo"] = "1 回声",
                    [" Echoes"] = " 回声"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
