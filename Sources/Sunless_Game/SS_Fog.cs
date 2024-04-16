using HarmonyLib;
using Sunless.Game.Entities.Weather;
using System.Collections.Generic;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Fog))]
    public class SS_Fog
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Fog.OnEnter), new[] { typeof(Collider2D) })]
        public static IEnumerable<CodeInstruction> SSPatch_OnEnter(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "We have entered a fog-bank! Our gunners struggle to see our foe.", "我们已经驶入了雾堤！我们的炮手将难以探敌。");
            return instructions;
        }
    }
}
