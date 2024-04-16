using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using System.Collections.Generic;

namespace SSTranslator
{
    [HarmonyPatch(typeof(JournalProvider))]
    public class SS_JournalProvider
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(JournalProvider.RenderAccomplishments))]
        public static IEnumerable<CodeInstruction> SSPatch_RenderAccomplishments(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["'s Accomplishments"] = "的历往",
                ["You don't have any accomplishments."] = "你没有任何历往"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
    }

    //[HarmonyPostfix]
    //[HarmonyPatch(nameof(JournalProvider.Display))]
    //static void

}
