using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.UI.Icons;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(OfficerIcon))]
    public class SS_OfficerIcon
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(OfficerIcon.EquipItem))]
        public static IEnumerable<CodeInstruction> SSPatch_EquipItem(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "We must dock before we change the ship's equipment.", "我们必须先停泊才能更换舰船装备。");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(OfficerIcon.UnequipItem))]
        public static IEnumerable<CodeInstruction> SSPatch_UnequipItem(IEnumerable<CodeInstruction> instructions)
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
                    ["Unassign"] = "卸任",
                    ["Assign"] = "分配",
                    ["Speak to"] = "交谈"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
