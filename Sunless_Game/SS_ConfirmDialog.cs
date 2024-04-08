using HarmonyLib;
using Sunless.Game.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(ConfirmDialog))]
    public class SS_ConfirmDialog
    {
        [HarmonyPrefix]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(GameObject), typeof(string), typeof(Action), typeof(string), typeof(Action), typeof(string), typeof(string) })]
        public static void SSPatch_ConfirmDialog(ref string yesText, ref string noText)
        {
            //Debug.Log($"Dialog - yes:'{yesText}' no: '{noText}'");
            if (yesText.ToLower().Trim() == "yes")
            {
                yesText = "是";
            }
            if (noText.ToLower().Trim() == "no")
            {
                noText = "否";
            }
        }
        // 我也不知道为什么readonly字段可以用Postfix进行patch，明明执行在构造函数之后，此时应该已经成形了才对，除非Harmony的Postfix是在函数的末尾调用一次patch，否则说不通。
        //[HarmonyTranspiler]
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(GameObject), typeof(Action), typeof(Action), typeof(string), typeof(string) })]
        static void Postfix(ref Button ____confirmButton, ref Button ____cancelButton)
        {
            //Debug.Log($"Dialog - yes:'{____confirmButton.GetComponentInChildren<Text>().text}' no: '{____cancelButton.GetComponentInChildren<Text>().text}'");
            if (____confirmButton.GetComponentInChildren<Text>().text.ToLower().Trim() == "yes")
            {
                ____confirmButton.GetComponentInChildren<Text>().text = "是";
            }
            else
            {
                Debug.LogError($"是 按钮的文本：{____confirmButton.GetComponentInChildren<Text>().text}");
            }
            if (____cancelButton.GetComponentInChildren<Text>().text.ToLower().Trim() == "no")
            {
                ____cancelButton.GetComponentInChildren<Text>().text = "否";
            }
            else
            {
                Debug.LogError($"否 按钮的文本：{____cancelButton.GetComponentInChildren<Text>().text}");
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(GameObject), typeof(Action), typeof(Action), typeof(string), typeof(string) })]
        static void SSPatch_ConfirmDialogPrefix(ref string title, ref string description)
        {
            switch (title)
            {
                case "Exit to Title Screen":
                    title = "退出到标题界面";
                    break;
                case "Accept Legacy":
                    title = "接收遗留之物";
                    break;
                case "Proceed Without a Legacy":
                    title = "拒绝遗留之物继续";
                    break;
            }

            switch (description)
            {
                case "Are you sure you want to exit to Title Screen?":
                    description = "你确定要退出到标题界面吗？";
                    break;
                case "Are you sure you want to accept this legacy?":
                    description = "你确定要接收此项遗留之物吗？";
                    break;
                case "Are you sure you want to proceed without accepting a legacy?":
                    description = "你确定要拒绝遗留之物然后继续吗？";
                    break;
            }
        }
    }
}
