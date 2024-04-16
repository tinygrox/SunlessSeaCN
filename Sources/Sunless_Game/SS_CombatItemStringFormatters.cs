using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.Entities.Combat;
using Sunless.Game.Formatters.Tooltips;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(CombatItemStringFormatters))]
    public class SS_CombatItemStringFormatters
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CombatItemStringFormatters.GetTooltip), new[] { typeof(CombatItem), typeof(string) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Cooldown: "] = "冷却：",
                    [" second"] = "秒",
                    [" seconds"] = "秒",
                    ["Using this item will not consume it"] = "此物品使用不会被消耗"
                };

                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CombatItemStringFormatters.GetHotkey), new[] { typeof(int) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetHotkey(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                // 本质是将 Combat Item xxx 修改为 战斗物品 xx
                SS_Utility.ILReplacer(instructions, SS_Utility.KeyScheme);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CombatItemStringFormatters.GetHullTooltip), new[] { typeof(CombatAttack), typeof(Character) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetHullTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Hull Damage"] = "船体伤害",
                    [" damage "] = "伤害",
                    [" bonus from your Iron stat)"] = " 增益，根据你的钢铁值)"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CombatItemStringFormatters.GetLifeTooltip), new[] { typeof(CombatAttack), typeof(Character) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetLifeTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Life Damage"] = "生命伤害",
                    [" damage "] = "伤害",
                    [" bonus from your Iron stat)"] = " 增益，根据你的钢铁值)"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CombatItemStringFormatters.GetCrewTooltip), new[] { typeof(CombatAttack), typeof(Character) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetCrewTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Crew Damage"] = "船员伤害",
                    ["Does not damage crew"] = "不会造成船员伤害",
                    [" crew lost each hit if opponent is below 50% hull"] = " 每次命中对方减员数，仅当对方船体值低于50%",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CombatItemStringFormatters.GetTimeTooltip), new[] { typeof(CombatAttack), typeof(Character) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetTimeTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Warmup time"] = "预热时间",
                    [" seconds "] = "秒",
                    [" seconds bonus from your Mirrors stat)"] = " 秒增益，根据你的镜子值)",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CombatItemStringFormatters.GetStaggerTooltip), new[] { typeof(CombatAttack), typeof(Character) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetStaggerTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Stagger Amount"] = "震荡数值",
                    ["Does not stagger opponent"] = "未能对敌方造成震荡",
                    [" seconds knocked off your opponents firing solutions for each hit"] = " 秒每次命中敌方射击时间延长",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
