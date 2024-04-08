using HarmonyLib;
using Sunless.Game.UI.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(SavingIndicator))]
    public class SS_SavingIndicator
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject)])]
        public static void SSPatch_SavingIndicator(ref GameObject parent)
        {
            //Debug.Log($"[SavingIndicator]{parent.name}");
            if (parent != null && parent.transform.name == "BL_Table")
            {
                SS_Utility.UpdateGameObjectText(parent, "SavingIndicator(Clone)/Label", "Saving game", "正在保存");
            }
        }
    }
}
