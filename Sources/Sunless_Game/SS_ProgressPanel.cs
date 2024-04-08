using HarmonyLib;
using Sunless.Game.UI.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(ProgressPanel))]
    public class SS_ProgressPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(GameObject) })]
        public static IEnumerable<CodeInstruction> SSPatch_ProgressPanel(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Something awaits you in port", "港口有些什么在等着你");
            });
            return instructions;
        }
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(GameObject) })]
        public static void SSPatch_ProgressPanel(GameObject parent)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.UpdateGameObjectText(parent, "ProgressPanel(Clone)/Title", "LOG BOOK", "航海日志");
            });
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(ProgressPanel.GetFragmentsTooltip))]
        public static IEnumerable<CodeInstruction> SSPatch_GetFragmentsTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Your Pages score is <b>"] = "你的书页分数达到了<b> ",
                    ["</b>...so glean another <b>"] = " </b>…再收集 <b>",
                    ["</b> Fragments to gain a Secret"] = "</b> 碎片即可获得一个秘密"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
