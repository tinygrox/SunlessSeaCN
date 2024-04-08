using FailBetter.Core.QAssoc;
using HarmonyLib;
using Sunless.Game.Data.SNRepositories;
using Sunless.Game.UI.Storylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(ChallengeIcon))]
    public class SS_ChallengeIcon
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(ChallengeIcon.ChallengeTooltip), [typeof(BranchQRequirement)])]
        public static IEnumerable<CodeInstruction> SSPatch_ChallengeTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Your "] = "你的",
                    [" quality gives you a "] = "特质为你提供了",
                    ["% chance of success"] = "%的成功率"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyPostfix]
        [HarmonyPatch(nameof(ChallengeIcon.ChallengeTextFor), [typeof(Text), typeof(BranchQRequirement)])]
        public static void SSPatch_ChallengeTextFor(Text challengeLabel, BranchQRequirement baseQRequirement)
        {
            SS_Utility.PatchHelper(() =>
            {
                // 简单记录一下Patch逻辑，因为源代码很傻逼的使用 Name 作为条件判断，所以当 Json 文件中将 Luck 翻译了后，条件就恒为 false 了，需要改掉这个傻逼逻辑
                // Patch 逻辑是先确定 baseQRequirement.AssociatedQuality.Name == "Luck"=> QualityRepository.Instance.GetById(432) 一致
                // 一轮判断下来，可知在汉化的情况下，恒为假，且文本恒为 text 的内容，为了找回真正的 text2 内容，需要建立索引，让原来的 text 变回对应的 text2
                var textTotext2 = new Dictionary<string, string>()
                {
                    ["An almost impossible challenge"] = "A matter of luck: a long shot...but you might win.",
                    ["A high-risk challenge"] = "A matter of luck: the odds are strongly against you here.",
                    ["A tough challenge"] = "A matter of luck: the odds are against you here.",
                    ["A very chancy challenge"] = "A matter of luck: it could go either way.",
                    ["A chancy challenge"] = "A matter of luck: it could go either way.",
                    ["A modest challenge"] = "A matter of luck: pretty good odds.",
                    ["A very modest challenge"] = "A matter of luck: pretty good odds.",
                    ["A low-risk challenge"] = "A matter of luck: how can you fail?",
                    ["A straightforward challenge"] = "A matter of luck: a sure thing. Or is it?",
                };
                var trans = new Dictionary<string, string>()
                {
                    ["An almost impossible challenge"] = "几乎不可能的挑战",
                    ["A high-risk challenge"] = "高风险的挑战",
                    ["A tough challenge"] = "艰难的挑战",
                    ["A very chancy challenge"] = "非常冒险的挑战",
                    ["A chancy challenge"] = "冒险的挑战",
                    ["A modest challenge"] = "温和的挑战",
                    ["A very modest challenge"] = "非常温和的挑战",
                    ["A low-risk challenge"] = "低风险的挑战",
                    ["A straightforward challenge"] = "简单的挑战",
                    ["A matter of luck: a long shot...but you might win."] = "运气相关：胜算十分渺茫…但你也有极小概率成功",
                    ["A matter of luck: the odds are strongly against you here."] = "运气相关：成功没打算站在你这边",
                    ["A matter of luck: the odds are against you here."] = "运气相关：成功率对你不太有利",
                    ["A matter of luck: it could go either way."] = "运气相关：一半一半，可好可坏",
                    ["A matter of luck: pretty good odds."] = "运气相关：赢面很大",
                    ["A matter of luck: how can you fail?"] = "运气相关：你怎么可能失败？",
                    ["A matter of luck: a sure thing. Or is it?"] = "运气相关：十拿十稳，不是吗？",
                };
                if (baseQRequirement.AssociatedQuality == QualityRepository.Instance.GetById(432))
                {
                    Debug.Log($"看来应该是幸运的");
                    if (textTotext2.ContainsKey(challengeLabel.text))
                        challengeLabel.text = trans[textTotext2[challengeLabel.text]];
                }
                else
                    challengeLabel.text = trans[challengeLabel.text];
            });
        }
    }
}
