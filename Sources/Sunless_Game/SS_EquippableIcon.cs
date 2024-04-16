using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.UI.Icons;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(EquippableIcon))]
    public class SS_EquippableIcon
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(EquippableIcon.EquipItem))]
        public static IEnumerable<CodeInstruction> SSPatch_EquipItem(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "We must dock before we change the ship's equipment.", "我们必须先停泊才能更换舰船装备。");
            });
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch("GetInteractions", [typeof(Quality), typeof(bool)])]
        public static IEnumerable<CodeInstruction> SSPatch_GetInteractions(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Assay"] = "估值",
                    ["Fit to Slot"] = "自动装备",
                    ["Use"] = "使用"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }

    }
}
