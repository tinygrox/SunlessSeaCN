using FailBetter.Core;
using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(QualityCapMessage))]
    public class SS_QualityCapMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality), typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_QualityCapMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" has not increased: this quality cannot currently increase past  "] = "未能增加：此特质目前无法超过"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }
    }
}
