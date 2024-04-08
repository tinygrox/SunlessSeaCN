using HarmonyLib;
using Sunless.Game.Appearance;
using Sunless.Game.Dictionaries;
using Sunless.Game.UI.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(HotkeyButton))]
    public class SS_HotkeyButton
    {
        static Dictionary<string, string> trans = new Dictionary<string, string>()
        {
            ["Transform Ship "] = "切换船型",
            ["Zee-bat "] = "海蝙蝠",
            ["Toggle Sonar Pulse (automatic) "] = "开关声呐 (自动)",
            ["Repair Ship (available if your Hull is at 50% or better) "] = "维修船只 (船体值高于50时可用) ",
            ["Toggle Lights "] = "切换照明",
            ["Full power to the engines! "] = "全功率发动引擎！",
            ["Pause "] = "暂停",
            ["Resume "] = "恢复",
            ["Gazetteer "] = "日志",
            ["Chart "] = "海图",
            ["<b>Main Menu <color=#0d5919ff>[Esc]</color></b>"] = "<b>主菜单 <color=#0d5919ff>[Esc]</color></b>",
        };
        [HarmonyPrefix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(HotkeysDictionary.HotkeyAttributes)])]
        static void SSPatch_HotkeyBar(ref HotkeysDictionary.HotkeyAttributes attributes)
        {
            if (HotkeysDictionary.Resume.Tooltip != trans["Resume "])
            {
                HotkeysDictionary.Resume.Tooltip = trans["Resume "];
            }
            if (HotkeysDictionary.SonarPulse.Tooltip != trans["Toggle Sonar Pulse (automatic) "])
                HotkeysDictionary.SonarPulse.Tooltip = trans["Toggle Sonar Pulse (automatic) "];
            if (trans.ContainsKey(attributes.Tooltip))
            {
#if DEBUG
                Debug.Log($"[SSTranslator] - 翻译下方按钮提示 |{attributes.Tooltip}|");
#endif
                attributes.Tooltip = trans[attributes.Tooltip];
            }
        }
    }
}
