using HarmonyLib;
using Sunless.Game.UI.Combat;
using Sunless.Game.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(HealthBar))]
    public class SS_HealthBar
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(HealthBar.Update), new[] { typeof(Vector3), typeof(int), typeof(int), typeof(BehaviourState), typeof(bool) })]
        public static IEnumerable<CodeInstruction> SSPatch_Update(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, " (Crew: ", "(船员：");
            });
            return instructions;
        }
    }
}
