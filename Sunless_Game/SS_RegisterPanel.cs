using HarmonyLib;
using Sunless.Game.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(RegisterPanel))]
    public class SS_RegisterPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(RegisterPanel.IsValid), MethodType.Getter)]
        public static IEnumerable<CodeInstruction> SSPatch_IsValid(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                //SS_Utility.ILReplacer(ref instructions, "enter password", "输入密码");
                SS_Utility.ILReplacer(ref instructions, "invalid email", "邮箱地址无效");
            });
            return instructions;
        }
    }
}
