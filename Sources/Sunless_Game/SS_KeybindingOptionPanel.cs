using HarmonyLib;
using Sunless.Game.UI.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(KeybindingOptionPanel))]
    public class SS_KeybindingOptionPanel
    {
        [HarmonyTargetMethods]
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(KeybindingOptionPanel), nameof(KeybindingOptionPanel.ListenForKey));
            yield return AccessTools.Method(typeof(KeybindingOptionPanel), nameof(KeybindingOptionPanel.ListenForAlt));
        }
        //[HarmonyPatch(nameof(KeybindingOptionPanel.ListenForKey))]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> SSPatch_ListenForKey(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Bind key"] = "绑定按键",
                    ["Press a new key for "] = "绑定新按键给 ",
                    ["Cancel"] = "取消"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
