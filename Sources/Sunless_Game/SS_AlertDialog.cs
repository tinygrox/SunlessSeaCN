using HarmonyLib;
using Sunless.Game.UI.Components;
using System;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(AlertDialog))]
    public class SS_AlertDialog
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(Action), typeof(string), typeof(string)])]
        public static void SSPatch_AlertDialog(GameObject parent)
        {
            SS_Utility.UpdateGameObjectText(parent, "AlertDialog(Clone)/Continue/Text", "Continue", "继续");
        }
    }
}
