using FailBetter.Core.QAssoc;
using HarmonyLib;
using Sunless.Game.Entities;
using Sunless.Game.UI.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Officers))]
    public class SS_Officers
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(GameObject), typeof(SunlessCharacter), typeof(IOrderedEnumerable<CharacterQPossession>) })]
        public static IEnumerable<CodeInstruction> SSPatch_Officers(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Show Officers", "显示军官");
                SS_Utility.ILReplacer(ref instructions, "Hide Officers", "隐藏军官");
            });
            return instructions;
        }

        // 不需要，也没用
        //[HarmonyPostfix]
        //[HarmonyPatch(nameof(Officers.ToggleOfficersDisplay))]
        //public static void SSPatch_Officers(ref string[] ___OfficerSlots)
        //{
        //    //var officerSlotsField = AccessTools.Field(typeof(Officers), "OfficerSlots");
        //    //if (officerSlotsField != null)
        //    //{
        //    //    Debug.Log($"[OFFICERS!!!!!!!!!!!!]");
        //    //    string[] newOfficerSlots = ["首席工程师", "厨师", "大副", "吉祥物", "枪械官", "医生"];
        //    //    officerSlotsField.SetValue(null, newOfficerSlots);
        //    //}
        //    foreach (var slot in ___OfficerSlots)
        //    {
        //        Debug.Log("BEFORE" + slot);
        //    }
        //    ___OfficerSlots = ["首席工程师", "厨师", "大副", "吉祥物", "枪械官", "医生"];
        //    foreach (var slot in ___OfficerSlots)
        //    {
        //        Debug.Log("AFTER:" + slot);
        //    }
        //}

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Officers.OpenOfficers))]
        public static IEnumerable<CodeInstruction> SSPatch_OpenOfficers(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Hide Officers", "隐藏军官");
            });
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Officers.CloseOfficers))]
        public static IEnumerable<CodeInstruction> SSPatch_CloseOfficers(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Show Officers", "显示军官");
            });
            return instructions;
        }


    }
}
