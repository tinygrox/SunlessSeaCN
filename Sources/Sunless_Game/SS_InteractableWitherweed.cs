using HarmonyLib;
using Sunless.Game.Phenomena.Physics;
using System.Collections.Generic;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(InteractableWitherweed))]
    public class SS_InteractableWitherweed
    {
        [HarmonyTranspiler]
        [HarmonyPatch("OnTriggerEnter2D", new[] { typeof(Collider2D) })]
        public static IEnumerable<CodeInstruction> SSPatch_OnTriggerEnter2D(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "The Witherweed damages our hull!", "凋零草损坏了我们的船体!");
            });
            return instructions;
        }
    }
}
