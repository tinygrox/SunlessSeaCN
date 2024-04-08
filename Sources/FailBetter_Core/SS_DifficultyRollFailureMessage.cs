using FailBetter.Core;
using FailBetter.Core.Result;
using HarmonyLib;
using Sunless.Game.Data.SNRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(DifficultyRollFailureMessage))]
    public class SS_DifficultyRollFailureMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality), typeof(int), typeof(int), typeof(Character)])]
        public static IEnumerable<CodeInstruction> SSPatch_DifficultyRollFailureMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" failed in a challenge! Try again and you may have better luck..."] = " 挑战失败!再试几次吧，没准运气就来了呢…",
                    ["(When you try a challenge that's difficult for you, you learn more even when you fail) "] = "(当你尝试相对困难的挑战时，即使失败，你也能获得经验)",
                    ["(This challenge was old territory for you - you won't learn so much.) "] = "(此番挑战对你而言简直是家常便饭 - 很难再有新的收获了) ",
                    ["(This was a second chance, so you'd already learnt from it.)"] = "(第二次挑战，你已经从中获得过了经验。)",
                    [" failed in a challenge! "] = "挑战失败！",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }

        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality), typeof(int), typeof(int), typeof(Character)])]
        public static void SSPatch_DifficultyRollFailureMessage(Quality q, ref string ____message)
        {
            // 干的就是 "Luck"
            if (q.Name == QualityRepository.Instance.GetById(432).Name)
            {
                // "You were unlucky. Better luck next time..."
                ____message = "你不走运。希望下次能好运吧…";
            }
        }
    }
}
