using HarmonyLib;
using Sunless.Game.Scripts.Phenomena.Physics;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Barrier))]
    public class SS_Barrier
    {
        [HarmonyTranspiler]
        [HarmonyPatch("OnTriggerEnter2D")]
        public static IEnumerable<CodeInstruction> SSPatch_OnTriggerEnter2D(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Go any further north, and ice or isolation would destroy you.", "若再向北，冰雪和孤寂将会把你吞噬。");
            return instructions;
        }
    }
}
