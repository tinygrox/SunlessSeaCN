using FailBetter.Core.QAssoc.BaseClasses;
using HarmonyLib;
using Sunless.Game.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(StatusQualityFormatter))]
    public class SS_StatusQualityFormatter
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(StatusQualityFormatter.GetDescription), [typeof(BaseQPossession)])]
        public static IEnumerable<CodeInstruction> SSPatch_GetDescription(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Right click to use this item", "右键单击使用此物品");
            });
            return instructions;
        }
    }
}
