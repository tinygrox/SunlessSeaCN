using HarmonyLib;
using Sunless.Game.Scripts.UI.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(DragDropCombatItem))]
    public class SS_DragDropCombatItem
    {
        [HarmonyTranspiler]
        [HarmonyPatch("AssignSlot", new[] { typeof(int) })]
        public static IEnumerable<CodeInstruction> SSPatch_AssignSlot(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "You cannot switch item slot during combat!", "处于战斗中无法切换物品栏！");
            return instructions;
        }
    }
}
