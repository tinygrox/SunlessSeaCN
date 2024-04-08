using HarmonyLib;
using Sunless.Game.UI.Components;
using Sunless.Game.UI.Menus.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(AccountOptionsPanel))]
    public class SS_AccountOptionsPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(ILogin)])]
        public static IEnumerable<CodeInstruction> SSPatch_AccountOptionsPanel(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["<b>Username:</b> (authentication required)"] = "<b>用户名：</b> (需要身份验证授权)",
                    ["Quit to the Title Screen before attempting to Authenticate."] = "进行身份验证授权前请先退回到主菜单界面。",
                    ["<b>Username:</b> "] = "<b>用户名：</b>"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(AccountOptionsPanel.ReAuthenticate))]
        public static IEnumerable<CodeInstruction> SSPatch_ReAuthenticate(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Currently Playing"] = "当前在游戏中",
                    ["Quit to the Title Screen before attempting to Authenticate."] = "进行身份验证授权前请先退回到主菜单界面。",
                    ["Outdated Version"] = "版本过时",
                    ["Please update to the latest version of the game."] = "请更新游戏到最新版本。"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
