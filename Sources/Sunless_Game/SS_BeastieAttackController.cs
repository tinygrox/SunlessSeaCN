using HarmonyLib;
using Sunless.Game.Combat;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(BeastieAttackController))]
    public class SS_BeastieAttackController
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(BeastieAttackController.CollidedWithPlayer))]
        public static IEnumerable<CodeInstruction> SSPatch_CollidedWithPlayer(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Suffered "] = "受到了",
                    [" damage!"] = "点伤害！",
                    ["Staggered! Firing solutions have been set back."] = "受到震荡！武器发射时间延后。"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("AttackSuccess", new[] { typeof(AttackData) })]
        public static IEnumerable<CodeInstruction> SSPatch_AttackSuccess(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Suffered "] = "受到了",
                    [" damage!"] = "点伤害！",
                    ["Staggered! Firing solutions have been set back."] = "受到震荡！武器发射时间延后。"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
