using FailBetter.Core;
using FailBetter.Core.Result;
using HarmonyLib;
using Sunless.Game.Data.SNRepositories;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(DifficultyRollSuccessMessage))]
    public class SS_DifficultyRollSuccessMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality), typeof(int), typeof(int), typeof(Character)])]
        public static IEnumerable<CodeInstruction> SSPatch_DifficultyRollSuccessMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["(Risky challenges mean you learn more.)"] = "(高风险的挑战意味着你能获得更多经验)",
                    ["(Simple challenges mean you don't learn so much.)"] = "(简单的挑战意味着你能获得的经验不多)",
                    ["(But this was a second chance, so you'd already learnt from it.)"] = "(但这是第二次了，你已经得到过经验了)",
                    ["You succeeded in a {0} challenge! {1} {2}"] = "你在{0}的挑战成功了！{1} {2}",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality), typeof(int), typeof(int), typeof(Character)])]
        public static void SSPatch_DifficultyRollSuccessMessage(Quality q, ref string ____message)
        {
            // 干的就是 "Luck"
            if (q.Name == QualityRepository.Instance.GetById(432).Name)
            {
                // "You were fortunate!"
                ____message = "你很幸运！";
            }
        }
    }

}
