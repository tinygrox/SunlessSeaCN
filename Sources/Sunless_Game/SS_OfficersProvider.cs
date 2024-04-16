using FailBetter.Core;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Sunless.Game.ApplicationProviders.OfficersProvider))]
    public class SS_OfficersProvider
    {
        [HarmonyTranspiler]
        [HarmonyPatch("CreateOfficerPanel", new[] { typeof(IEnumerable<Quality>) })]
        public static IEnumerable<CodeInstruction> SSPatch_CreateOfficerPanel(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Available Officers", "可用军官");
            SS_Utility.ILReplacer(ref instructions, "You do not have any officers!", "你没有任何军官！");
            return instructions;
        }
    }
}
