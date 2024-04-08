using HarmonyLib;
using Sunless.Game.UI.Menus;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(LatestNews))]
    public class SS_LatestNews
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(LatestNews.SetPanelContents))]
        public static IEnumerable<CodeInstruction> SSPatch_SetPanelContents(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Connect to our server to retrieve latest news and check for content updates.", "连接到我们的服务器以获取最新消息并检查更新。");
            });
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(LatestNews.SetUpdateButtonState))]
        public static IEnumerable<CodeInstruction> SSPatch_SetUpdateButtonState(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            SS_Utility.PatchHelper(() =>
            {
                CodeInstruction call = CodeInstruction.Call(typeof(SS_LatestNews), nameof(SS_LatestNews.UpdateButtonValues));
                codes.Insert(0, call);
            });
            //return instructions;
            return codes.AsEnumerable();
        }

        static void UpdateButtonValues()
        {
            var innerclass = AccessTools.Inner(typeof(LatestNews), "UpdateButtonValues");
            //Debug.LogWarning("[SSTranslator] UpdateButtonValues!");
            if (innerclass != null)
            {
                //Debug.LogWarning("[SSTranslator] 【In】UpdateButtonValues!");
                Traverse.Create(innerclass).Field(nameof(LatestNews.UpdateButtonValues.NoConnection)).SetValue("(无法连接)");
                Traverse.Create(innerclass).Field(nameof(LatestNews.UpdateButtonValues.NoNewContent)).SetValue("故事已是最新！");
                Traverse.Create(innerclass).Field(nameof(LatestNews.UpdateButtonValues.GetContent)).SetValue("新可用故事！");
                Traverse.Create(innerclass).Field(nameof(LatestNews.UpdateButtonValues.Checking)).SetValue("正在检查更新……");
                Traverse.Create(innerclass).Field(nameof(LatestNews.UpdateButtonValues.OldSoftware)).SetValue("需要游戏版本为最新");
                Traverse.Create(innerclass).Field(nameof(LatestNews.UpdateButtonValues.ConnectAndCheck)).SetValue("连接到服务器");
            }
        }
    }
}