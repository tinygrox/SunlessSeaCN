using HarmonyLib;
using Sunless.Game.UI.Menus;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(MainMenuButton))]
    public class SS_MainMenuButton
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(string), typeof(string), typeof(Action)])]
        public static void SSPatch_MainMenu_Button_Localized(ref Text ____label)
        {
            Dictionary<string, string> trans = new()
            {
                ["Save"] = "保存",
                ["Load"] = "载入",
                ["Back"] = "返回"
            };
            if (trans.ContainsKey(____label.text))
            {
                ____label.text = trans[____label.text];
            }
        }
    }
}
