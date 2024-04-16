using HarmonyLib;
using Sunless.Game.Entities.Beasties;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(SpawnedEntity))]
    public class SS_SpawnedEntity
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(SpawnedEntity.GetDescription))]
        public static IEnumerable<CodeInstruction> SSPatch_GetDescription(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Discovered a ", "发现了");
            SS_Utility.ILReplacer(ref instructions, " from ", "，来自于");
            return instructions;
        }
    }
}
