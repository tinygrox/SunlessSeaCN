using FailBetter.Core;
using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(AttackSuccessMessage))]
    public class SS_AttackSuccessMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Character)])]
        static IEnumerable<CodeInstruction> SSPatch_AttackSuccessMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["You have murdered "] = "你已经谋杀了",
                    [" with style and distinction. Well done."] = "，做的既有范又与众不同。干得好。",

                    [" spotted you at the last second, but too late!"] = "在最后的关头发现了你，但已经为时已晚！",

                    ["You stepped from the shadows and did something appalling to "] = "你从阴影中现身，对",
                    [". Well done."] = "做了一些可怕的事。干得好。",

                    ["Your attack on "] = "你针对",
                    [" succeeded. There are some distressing screams, but only briefly."] = "发动的攻击成功了。现场发出了几声痛苦的尖叫，但时间很短。",

                    ["There was no hiding place for "] = "",
                    ["! Your attack was inescapable!"] = "已经无处可躲！你的攻击无法被闪避！",

                    [" evaded your knife. So you shot "] = "躲过了你的刀。所以你对",
                    ["! And the peanut gallery can stop complaining. Strictly speaking, it's within the rules."] = "开枪了！花生画廊可以停止抱怨了。严格来说，这符合规则。",

                    [" tripped and fell on your knife. Silly thing."] = "被你的刀绊倒了。真是个愚蠢的家伙。",

                    [" scurried like a rat! But you were everywhere! You have defeated "] = "被逼得抱头鼠窜！但你无处不在！你轻而易举的打败了",
                    [" handily."] = "。",

                    ["Murder most foul. If also most elegant. At "] = "最为肮脏的谋杀，但也最为优雅。以",
                    ["'s expense"] = "为代价。",

                    ["Let us drink a toast to the memory of "] = "让我们举杯缅怀",
                    [", who knew no better than to tangle with you."] = "，其根本不知道与你为敌有多么愚蠢。",

                    ["You are cunning, and ferocious. Well done."] = "你狡猾而凶险。干得好。",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
