using HarmonyLib;
using Sunless.Game.Phenomena.ShipEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Witherweed))]
    public class SS_Witherweed
    {
        [HarmonyTranspiler]
        [HarmonyPatch("OnTriggerStay2D", new[] { typeof(Collider2D) })]
        public static IEnumerable<CodeInstruction> SSPatch_OnTriggerStay2D(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Your engines slow in the Witherweed! Engines labour: zailors cry out, and fall.", "你的引擎被凋零草减速了！引擎在艰难运转：水手们惊恐万分，陷入绝望。");
            return instructions;
        }
    }
}
