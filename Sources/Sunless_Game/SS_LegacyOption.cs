using HarmonyLib;
using Sunless.Game.UI.Legacy;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(LegacyOption))]
    public class SS_LegacyOption
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(string), typeof(Action)])]
        public static void SSPatch_LegacyOption(ref Text ____label)
        {
            switch (____label.text)
            {
                case "Rival: Retain 50% of their Iron value and one weapon.":
                    ____label.text = "仇敌：保留50%钢铁值和一件武器";
                    break;
                case "Pupil: Retain 50% of their Mirrors value and 50% of their money.":
                    ____label.text = "学徒：保留50%镜子值和50%货币";
                    break;
                case "Salvager: Retain 50% of their Veils value and 50% of their money.":
                    ____label.text = "打捞者：保留50%面纱值和50%货币";
                    break;
                case "Shipmate: Retain 50% of their Hearts value and one officer.":
                    ____label.text = "同行船员：保留50%的红心值和一名军官";
                    break;
                case "Correspondent: Retain 50% of their Pages value and their chart.":
                    ____label.text = "新闻通讯员：保留50%的书页值和海图";
                    break;
            }
        }
    }
}
