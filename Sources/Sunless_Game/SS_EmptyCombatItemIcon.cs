using HarmonyLib;
using Sunless.Game.UI.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(EmptyCombatItemIcon))]
    public class SS_EmptyCombatItemIcon
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_EmptyCombatItemIcon(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Weapon slot ", "武器槽位 ");
            });
            return instructions;
        }
    }
}
