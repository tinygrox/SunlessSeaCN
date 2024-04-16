using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Sunless.Game.Entities.Beasties.CompanionController))]
    public class SS_CompanionController
    {
        [HarmonyTranspiler]
        [HarmonyPatch("SpawnSonar")]
        public static IEnumerable<CodeInstruction> SSPatch_SpawnSonar(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Something has been following us. It is patient.", "有什么东西在跟着我们。且耐心十足。");
            return instructions;
        }
    }
}
