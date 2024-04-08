using HarmonyLib;
using Sunless.Game.Entities.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Snow))]
    public class SS_Snow
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Snow.OnEnter), new[] { typeof(Collider2D) })]
        public static IEnumerable<CodeInstruction> SSPatch_OnEnter(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Snow sweeps down from the north! The ship's engines labour against it.", "大雪从北方袭来！船舶引擎在轰鸣着与之对抗！");
            return instructions;
        }
    }
}
