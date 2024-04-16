using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using System.Collections.Generic;

namespace SSTranslator
{
    [HarmonyPatch]
    public class SS_ExchangeProvider
    {
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Sunless.Game.ApplicationProviders.ExchangeProvider), nameof(ExchangeProvider.DisplayExchange))]
        public static IEnumerable<CodeInstruction> SSPatch_DisplayExchange(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["No shops available"] = "无可用商店",
                ["There is nothing available for you here. Perhaps once, but not now, and not for you."] = "这里没有对你有用的东西。也许之前有，但现在没有了，有也不是给你的。"
            };
            SS_Utility.ILReplacer(instructions, trans);

            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Sunless.Game.ApplicationProviders.ExchangeProvider), nameof(ExchangeProvider.Sell))]
        public static IEnumerable<CodeInstruction> SSPatch_Sell(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                //["Stack Item"] = SS_Utility.KeyScheme["Stack Item"],
                ["You don't have enough of these"] = "你没有那么多这些"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Sunless.Game.ApplicationProviders.ExchangeProvider), nameof(ExchangeProvider.Buy))]
        public static IEnumerable<CodeInstruction> SSPatch_Buy(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                //["Stack Item"] = SS_Utility.KeyScheme["Stack Item"],
                ["You can't afford that!"] = "你买不起那个！"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
    }
}
