﻿using FailBetter.Core.Utility;
using HarmonyLib;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(StringExtensions))]
    public class SS_StringExtensions
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(StringExtensions.CorrectIndefiniteArticleFor), [typeof(string)])]
        public static bool SSPatch_CorrectIndefiniteArticleFor(ref string __result)
        {
            __result = "1个";
            return false;
        }
    }
}
