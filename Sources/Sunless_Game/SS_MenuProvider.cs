using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(MenuProvider))]
    public class SS_MenuProvider
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MenuProvider.Render))]
        public static IEnumerable<CodeInstruction> SSPatch_Render(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Continue", "继续");
            //SS_Utility.ILReplacer(ref instructions, "Manual Save", "手动存档"); // 会影响慈悲模式，傻逼东西，nm四了
            return instructions;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MenuProvider.WarningErrorMessage), [typeof(string)])]
        public static void WarningErrorMessage(ref string errorText)
        {
            Debug.Log($"[MenuProvider.WarningErrorMessage] errorText: '{errorText}'");
            if (errorText == "The import failed, continue with your saved game or try again later.")
            {
                errorText = "导入失败，可继续游玩已存档或稍后再试一遍。";
            }
        }
        //[HarmonyTranspiler]
        //[HarmonyPatch(nameof(MenuProvider.UpdateLocalData))]
        //public static IEnumerable<CodeInstruction> SSPatch_UpdateLocalData(IEnumerable<CodeInstruction> instructions)
        //{
        //    SS_Utility.ILReplacer(ref instructions, "The import failed, continue with your saved game or try again later.", "导入失败，可使用存档继续游戏或再试一遍。");
        //    return instructions;
        //}
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MenuProvider.DeleteSave))]
        public static IEnumerable<CodeInstruction> SSPatch_DeleteSave(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["Confirm delete"] = "删除确认",
                ["Are you sure you want to delete this game?"] = "你确定想要删除此游玩进度吗？"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
    }
}
