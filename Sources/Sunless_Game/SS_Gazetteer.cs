using HarmonyLib;
using Sunless.Game.UI.Gazetteer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Gazetteer))]
    public class SS_Gazetteer
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject)])]
        public static IEnumerable<CodeInstruction> SSPatch_Gazetteer(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Story"] = "轶事",
                    ["Hold"] = "货舱",
                    ["Journal"] = "日志",
                    ["Officers"] = "军官",
                    ["Shops"] = "商店",
                    ["Shipyard"] = "船坞"

                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Gazetteer.SetRightTitle), new[] { typeof(string), typeof(Action) })]
        public static IEnumerable<CodeInstruction> SSPatch_SetRightTitle(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "<b>Click to jettison cargo</b>", "<b>点击抛弃货物</b>");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Gazetteer.UpdateJettisonWidget))]
        public static IEnumerable<CodeInstruction> SSPatch_UpdateJettisonWidget(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "<b>Cargo:</b> ", "<b>货物：</b>");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Gazetteer.GetEchoTooltip))]
        public static IEnumerable<CodeInstruction> SSPatch_GetEchoTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" Echoes"] = " 回声",
                    ["Echoes are the currency of Fallen London"] = "回声是沦敦的货币"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
