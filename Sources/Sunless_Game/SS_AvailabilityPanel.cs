using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.UI.Exchanges;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(AvailabilityPanel))]
    public class SS_AvailabilityPanel
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(Availability)])]
        static void ChangeStaticFields(ref string ___BUY_NORMAL, ref string ___BUY_BATCH, ref string ___SELL_NORMAL, ref string ___SELL_BATCH)
        {
            try
            {
                if (___BUY_NORMAL == "<b>BUY</b> (1):")
                {
                    ___BUY_NORMAL = "<b>购买</b> (1):";
                }
                if (___BUY_BATCH == "<b>BUY</b> (10):")
                {
                    ___BUY_BATCH = "<b>购买</b> (10):";
                }
                if (___SELL_NORMAL == "<b>SELL</b> (1):")
                {
                    ___SELL_NORMAL = "<b>出售</b> (1):";
                }
                if (___SELL_BATCH == "<b>SELL</b> (10):")
                {
                    ___SELL_BATCH = "<b>出售</b> (10):";
                }
            }
            catch (Exception e)
            {
                throw new Exception($"[SSTranslator]<AvailabilityPanel.ChangeStaticFields> Exception:{e}");
            }
        }

        [HarmonyTranspiler]
        [HarmonyPatch("FormatBuyButton", new[] { typeof(bool) })]
        public static IEnumerable<CodeInstruction> SSPatch_FormatBuyButton(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["This will cost "] = "这将花费",
                    [" Echoes"] = "回声",
                    ["\n\n<b>You cannot afford this!</b>"] = "\n\n<b>你买不起这个！</b>",
                    ["\n\n<b>You don't have enough room in your hold!</b>"] = "\n\n<b>你的货舱装不下了！</b>",
                    ["\n\n<i>Hold "] = "\n\n<i>按住",
                    [" to buy 10</i>"] = "可一次购买10</i>"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("FormatSellButton", new[] { typeof(bool) })]
        public static IEnumerable<CodeInstruction> SSPatch_FormatSellButton(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["This will earn you "] = "这将让你获取",
                    [" Echoes"] = "回声",
                    ["\n\n<b>You don't have enough of these!</b>"] = "\n\n<b>存货数量没有那么多！</b>",
                    ["\n\n<i>Hold "] = "\n\n<i>按住",
                    [" to sell 10</i>"] = "可一次出售10</i>"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
