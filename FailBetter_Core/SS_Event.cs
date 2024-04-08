using FailBetter.Core;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(Event))]
    public class SS_Event
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Event.ApplyOtherEffects), [typeof(Character)])]
        public static IEnumerable<CodeInstruction> SSPatch_ApplyOtherEffects(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["Your inventory has grown!"] = "你的库存容量增加了",
                ["You have an opportunity to change your face! Use it from the Me page."] = "你现在有机会更换你的面孔！请在“我”页面使用它。"
            };
            return instructions;
        }
    }
}
