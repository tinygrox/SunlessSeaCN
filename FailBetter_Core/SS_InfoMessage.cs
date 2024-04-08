using FailBetter.Core.Result;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(InfoMessage))]
    public class SS_InfoMessage
    {
        [HarmonyPrefix]
        [HarmonyPatch(MethodType.Constructor, [typeof(string), typeof(string), typeof(string), typeof(string)])]
        static void SSPatch_InfoMessage(ref string imageTooltip, string headline)
        {
            Debug.Log($"[FailBetter.Core] headline:{headline} imageTooltip: {imageTooltip}");

            if (imageTooltip == "A thing has happened!")
            {
                imageTooltip = "某件事情发生了！";
            }
        }
    }
}
