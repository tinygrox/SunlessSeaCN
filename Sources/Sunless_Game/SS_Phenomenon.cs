using HarmonyLib;
using Sunless.Game.Entities.Geography;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Phenomenon))]
    public class SS_Phenomenon
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Phenomenon.GetDescription))]
        public static IEnumerable<CodeInstruction> SSPatch_GetDescription(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "discovered ", "发现了");
            return instructions;
        }
    }
}
