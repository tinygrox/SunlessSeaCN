using HarmonyLib;
using Sunless.Game.Entities.Weather;
using System.Collections.Generic;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Waxwind))]
    public class SS_Waxwind
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Waxwind.OnEnter), new[] { typeof(Collider2D) })]
        public static IEnumerable<CodeInstruction> SSPatch_OnEnter(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "The killing Wax-Wind blows from the south! Engines labour: zailors cry out, and fall.", "致命的蜡风从南方吹来！引擎在艰难运转：水手们惊恐万分，陷入绝望。");
            return instructions;
        }
    }
}
