using FailBetter.Core.QAssoc.BaseClasses;
using HarmonyLib;
using Sunless.Game.Entities.Combat;
using Sunless.Game.Formatters.QIcons;
using System.Collections.Generic;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(QPossessionTooltipFormatter))]
    public class SS_QPossessionTooltipFormatter
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(QPossessionTooltipFormatter.GetQualityPossessionTooltip), [typeof(BaseQPossession), typeof(CombatItem)])]
        public static IEnumerable<CodeInstruction> SSPatch_GetQualityPossessionTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Usable in combat: "] = "战斗时可用：",
                    ["Cooldown: "] = "冷却时间：",
                    [" seconds"] = "秒",
                };
                SS_Utility.ILReplacer(instructions, trans);

            });
            return instructions;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(QPossessionTooltipFormatter.GetQualityPossessionTooltip), [typeof(BaseQPossession), typeof(CombatAttack)])]
        static void SSPatch_GetQualityPossessionTooltip(ref string __result)
        {
            if (__result.Contains("Aft weapon".ToUpper()))
            {
                __result = __result.Replace("Aft weapon".ToUpper(), "船艉武器");
            }
            else if (__result.Contains("Auxiliary".ToUpper()))
            {
                Debug.Log($"GetQualityPossessionTooltip - result [Auxiliary]:{__result}");
                __result = __result.Replace("Auxiliary weapon".ToUpper(), "辅助武器");
            }
            else if (__result.Contains("deck weapon".ToUpper()))
            {
                __result = __result.Replace("Deck weapon".ToUpper(), "甲板武器");
            }
            else if (__result.Contains("Bridge".ToUpper()))
            {
                Debug.Log($"GetQualityPossessionTooltip - result [Bridge]:{__result}");
                __result = __result.Replace("Bridge weapon".ToUpper(), "驾驶室武器");
            }
            else if (__result.Contains("Current Ship".ToUpper()))
            {
                Debug.Log($"GetQualityPossessionTooltip - result [Current Ship]:{__result}");
                __result = __result.Replace("Current Ship weapon".ToUpper(), "当前舰船武器");
            }
            else if (__result.Contains("Forward weapon".ToUpper()))
            {
                __result = __result.Replace("Forward weapon".ToUpper(), "船艏武器");
            }
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(QPossessionTooltipFormatter.GetQualityPossessionTooltip), [typeof(BaseQPossession), typeof(bool)])]
        public static IEnumerable<CodeInstruction> SSPatch_GetQualityPossessionTooltip2(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Equips to: "] = "装备至：",
                    ["Right click to speak to this officer"] = "右键点击与此军官谈话",
                    ["Right click to use this item"] = "右键点击使用此物品"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(QPossessionTooltipFormatter.DescribeCombatStats), [typeof(CombatAttack)])]
        public static IEnumerable<CodeInstruction> SSPatch_DescribeCombatStats(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Hull Damage: +"] = "船体伤害：+",
                    ["Life Damage: +"] = "生命值伤害：+",
                    ["Crew Damage: "] = "船员伤害：",
                    ["Warmup Time: "] = "预热时间：",
                    ["Stagger Amount: "] = "震荡：",
                    [" seconds"] = "秒",

                    // 完整句子是 "Costs one {combatAttack.QualityCost.Name} per use"
                    ["Costs one "] = "每次使用花费",
                    [" per use"] = ""
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }

    }
}
