using HarmonyLib;
using Sunless.Game.UI.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(KeymappingPanel))]
    public class SS_KeymappingPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(KeymappingPanel.WarnAndRestoreDefaults))]
        public static IEnumerable<CodeInstruction> SSPatch_WarnAndRestoreDefaults(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Restore defaults", "恢复为默认");
                SS_Utility.ILReplacer(ref instructions, "Are you sure you want to restore the default control scheme?", "你确定要恢复为默认的控制方案吗？");
            });
            return instructions;
        }

        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject)])]
        public static void SSPatch_KeymappingPanel(GameObject parent)
        {
            SS_Utility.UpdateGameObjectText(parent, "KeymappingPanel(Clone)/Title", "Rebind keys", "按键绑定");
            SS_Utility.UpdateGameObjectText(parent, "KeymappingPanel(Clone)/FooterButtons/DefaultsButton/Text", "Reset defaults", "重设");
            SS_Utility.UpdateGameObjectText(parent, "KeymappingPanel(Clone)/FooterButtons/AcceptButton/Text", "Accept", "应用");
            SS_Utility.UpdateGameObjectText(parent, "KeymappingPanel(Clone)/FooterButtons/CancelButton/Text", "Cancel", "取消");
        }
    }
}
