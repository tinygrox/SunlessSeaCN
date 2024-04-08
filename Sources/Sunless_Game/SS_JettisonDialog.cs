using HarmonyLib;
using Sunless.Game.Entities;
using Sunless.Game.UI.Gazetteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(JettisonDialog))]
    public class SS_JettisonDialog
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(GameObject), typeof(SunlessCharacter), typeof(Action) })]
        public static IEnumerable<CodeInstruction> SSPatch_JettisonDialog(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, " + Click for stacks of 10)", "+ 左键 一次性丢弃 10)");
            });
            return instructions;
        }
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(GameObject), typeof(SunlessCharacter), typeof(Action) })]
        public static void SSPatch_JettisonDialog(GameObject parent) // Anchor_CENTER
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.UpdateGameObjectText(parent, "JettisonWindow(Clone)/JettisonContainer/ButtonContainer/AcceptButton/Text", "DONE", "完成");
                SS_Utility.UpdateGameObjectText(parent, "JettisonWindow(Clone)/JettisonContainer/Overboard/Title", "Overboard", "抛下物品");
            });
        }
        [HarmonyTranspiler]
        [HarmonyPatch("UpdateJettisonPanels")]
        public static IEnumerable<CodeInstruction> SSPatch_UpdateJettisonPanels(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Hold", "货舱");
            });
            return instructions;
        }


    }
}
