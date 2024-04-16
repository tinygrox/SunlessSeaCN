using HarmonyLib;
using Sunless.Game.Combat;
using Sunless.Game.UI.Combat;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(AttackBar))]
    public class SS_AttackBar
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(AttackBar.UpdateContent), new[] { typeof(List<AttackData>), typeof(List<CombatItemData>) })]
        public static IEnumerable<CodeInstruction> SSPatch_UpdateContent(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Deck"] = "甲板",
                    ["Forward"] = "舰艏",
                    ["Aft"] = "舰艉"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
