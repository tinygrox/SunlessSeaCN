using FailBetter.Core.Result;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(FatePointChangeMessage))]
    public class SS_FatePointChangeMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_FatePointChangeMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["You have gained "] = "你获得了",
                    [" Fate! "] = "<b>命运</b>！",
                    ["Fate can be used to give you more Actions, Opportunities and other lovely things. Explore the Nex tab above."] = "<b>命运</b>可以用来给你更多的行动、机会和其他美好的事物。探索上面的“Nex”选项卡。",

                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
