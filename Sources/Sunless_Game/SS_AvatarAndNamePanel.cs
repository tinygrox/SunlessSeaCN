using HarmonyLib;
using Sunless.Game.UI.Officers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(AvatarAndNamePanel))]
    public class SS_AvatarAndNamePanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(AvatarAndNamePanel.Accept))]
        public static IEnumerable<CodeInstruction> SSPatch_Accept(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["You must enter a name"] = "你需有名",
                    ["Only the gods of the deep zee are nameless."] = "唯深海之神方可无名"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject)])]
        public static void SSPatch_AvatarAndNamePanel(GameObject parent)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.UpdateGameObjectText(parent, "NameAndAvatarSelection(Clone)/V_Layout/H_Layout/NameInput/Placeholder", "Choose your name", "选个名字");
                SS_Utility.UpdateGameObjectText(parent, "NameAndAvatarSelection(Clone)/V_Layout/H_Layout/AcceptBtn/Text", "ACCEPT", "接受");
            });
        }
    }
}
