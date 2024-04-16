using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.Entities.Combat;
using Sunless.Game.Formatters.Tooltips;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(CombatAttackStringFormatters))]
    public class SS_CombatAttackStringFormatters
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CombatAttackStringFormatters.GetTooltip), [typeof(CombatAttack)])]
        public static IEnumerable<CodeInstruction> SSPatch_GetTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Hull Damage: "] = "船体伤害：",
                    [" • Life Damage: "] = " • 生命值伤害：",
                    ["Warmup: "] = "预热：",
                    [" seconds"] = "秒",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CombatAttackStringFormatters.GetExposedDamageMessage), [typeof(Quality), typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_GetExposedDamageMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Lost "] = "失去了 ",
                    [" crew!"] = " 名船员!",
                    [" supplies!"] = "物资!",
                    ["Terror has increased by "] = "恐惧值增加了 ",
                    [" changed by "] = "变化了",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
