using HarmonyLib;
using Sunless.Game.UI.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(AttackButton))]
    public class SS_AttackButton
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(AttackButton.LockedSlotStatus), typeof(AttackButton.SlotType)])]
        public static IEnumerable<CodeInstruction> SSPatch_AttackButton(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Deck"] = "甲板",
                    ["Forward"] = "舰艏",
                    ["Aft"] = "舰艉",
                    [" weapon ("] = "武器(",
                    ["cannot be equipped on this ship"] = "无法在此船安装",
                    ["none equipped!"] = "未安装！"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(AttackButton.ToggleTooltip), new[] { typeof(bool) })]
        public static IEnumerable<CodeInstruction> SSPatch_ToggleTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, " weapon ", "武器");
            });
            return instructions;
        }
    }
}
