using HarmonyLib;
using Sunless.Game.UI.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(LegacyQualitySelect))]
    public class SS_LegacyQualitySelect
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LegacyQualitySelect.Disable))]
        public static void SSPatch_LegacyQualitySelect(ref string ____notAvailableMessage)
        {
            switch (____notAvailableMessage)
            {
                case "<i>None (requires Shipmate legacy)</i>":
                    ____notAvailableMessage = "<i>无 (需要同行船员)</i>";
                    break;
                case "<i>None (requires Rival legacy)</i>":
                    ____notAvailableMessage = "<i>无 (需要仇敌)</i>";
                    break;
            }
        }
    }
}
