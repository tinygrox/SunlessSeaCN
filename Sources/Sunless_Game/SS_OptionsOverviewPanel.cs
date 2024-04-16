using HarmonyLib;
using Sunless.Game.UI.Menus.Options;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(OptionsOverviewPanel))]
    public class SS_OptionsOverviewPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch("CreateOptions")]
        public static IEnumerable<CodeInstruction> SSPatch_CreateOptions(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Account Management", "账号管理");
                SS_Utility.ILReplacer(ref instructions, "Audio Settings", "声音设置");
                SS_Utility.ILReplacer(ref instructions, "Video Settings", "视频设置");
                SS_Utility.ILReplacer(ref instructions, "Edit Keybindings", "按键绑定");
                SS_Utility.ILReplacer(ref instructions, "Reset tutorial", "重置教程");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("ConfirmResetTutorials")]
        public static IEnumerable<CodeInstruction> SSPatch_ConfirmResetTutorials(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Tutorial reset", "教程重置");
                SS_Utility.ILReplacer(ref instructions, "All tutorials have been restored", "所有教学课程都已重置");
            });
            return instructions;
        }

        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(bool)])]
        public static void SSPatch_OptionOverviewPanel(ref GameObject ____inheritedParent)
        {
            // 实际上传过来的 GameObject parent 即赋值给 _inheritedParent 的 GameObject 就是 GameObject.FInd("Anchor_CENTER")
            var Title = ____inheritedParent.transform.Find("OptionsPanel(Clone)/Title");
            if (Title != null)
            {
                var TitleText = Title.GetComponent<Text>();
                if (TitleText != null && TitleText.text == "Options")
                    TitleText.text = "设置";
            }

            var BackButton = ____inheritedParent.transform.Find("OptionsPanel(Clone)/BackButton/Text");
            if (BackButton != null)
            {
                var BackButtonText = BackButton.GetComponent<Text>();
                if (BackButtonText != null && BackButtonText.text == "Back")
                {
                    BackButtonText.text = "返回";
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(OptionsOverviewPanel.OpenAccountOptions))]
        public static void SSPatch_OpenAccountOptions(ref GameObject ____inheritedParent)
        {

            //var Anchor_CENTER = GameObject.Find("Anchor_CENTER");
            //if (Anchor_CENTER != null)
            //{
            //int optionIndex = -1;
            //Debug.Log("[Anchor_CENTER] not null!!");
            //for (int i = 0; i < Anchor_CENTER.transform.childCount; i++)
            //{
            /// 沃日，还真的跟UnityExplore显示一毛一样，
            /// [Info   : Unity Log] [Anchor_CENTER] find in [TitlePanel]
            // [Info: Unity Log][Anchor_CENTER] find in [Main Menu(Clone)]
            // [Info: Unity Log][Anchor_CENTER] find in [OptionsPanel(Clone)]
            // [Info: Unity Log][Anchor_CENTER] find in [AccountOptionsPanel(Clone)]
            //Debug.Log($"[Anchor_CENTER] find in [{Anchor_CENTER.transform.GetChild(i).name}]");
            //if (Anchor_CENTER.transform.GetChild(i).name == "AccountOptionsPanel(Clone)")
            //    optionIndex = i;
            //}

            //var AccountPanel = Anchor_CENTER.transform.Find("AccountOptionsPanel(Clone)");
            // Find 找不到，就很怪
            //var AccountPanel = Anchor_CENTER.transform.FindChild("AccountOptionsPanel(Clone)");
            //var AccountPanel = Anchor_CENTER.transform.GetChild(optionIndex);

            //if (AccountPanel != null)
            //{
            //    var TitlePanel = AccountPanel.transform.Find("Title");
            //    if (TitlePanel != null)
            //    {
            //        var titletext = TitlePanel.transform.GetComponent<Text>();
            //        if (titletext != null && titletext.text == "Account Management")
            //        {
            //            titletext.text = "账户管理";
            //        }
            //    }
            //}
            //}
            // 经历了上面的实验，现在只需一边看着 Unity Explorer 一遍写即可
            // 你们说 XUnity.AutoTranslator 的原理会不会也是这样呢？遍历游戏中的所有的GameObject 然后记录所有的 Component<Text>，再配合机器翻译

            //var AccountPanel = GameObject.Find("AccountOptionsPanel(Clone)"); // 相同，但性能更差

            var AccountPanel = ____inheritedParent.transform.Find("AccountOptionsPanel(Clone)");
            if (AccountPanel != null)
            {
                // 标题
                var title = AccountPanel.transform.Find("Title");
                if (title != null) // 防将来的某一天的游戏更新导致的异常
                {
                    var getText = title.GetComponent<Text>();
                    if (getText != null && getText.text == "Account Management")
                    {
                        getText.text = "账户管理";
                    }
                }
                else
                {
                    Debug.LogError("[AccountPanel] title not find!");
                }

                // 内容
                var content = AccountPanel.transform.Find("Content/Notes");
                if (content != null)
                {
                    var content_text = content.GetComponent<Text>();
                    if (content_text != null && content_text.text == "Authenticate at any time to recieve your latest condiments.\n\n<i>Condiments - Tasty accompaniments found in Fallen London, or purchased as DLC.</i>")
                    {
                        content_text.text = "随时都可进行身份验证授权来获取你的最新调味品\n\n<i><b>调味品</b>——可在伦敦坠落找到的调味品，或当做DLC进行购买</i>";
                    }
                }
                else
                {
                    Debug.LogError("[AccountPanel] Content/Notes not find!");
                }

                // 按钮
                var button1 = AccountPanel.transform.Find("FooterButtons/ChangeButton");
                if (button1 != null)
                {
                    var btn = button1.transform.Find("Text").GetComponent<Text>();
                    if (btn != null && btn.text == "Authenticate")
                    {
                        btn.text = "验证授权";
                    }
                }
                else
                {
                    Debug.LogError("[AccountPanel] FooterButtons/ChangeButton not find!");
                }
                var button2 = AccountPanel.transform.Find("FooterButtons/BackButton");
                if (button2 != null)
                {
                    var btn = button2.transform.Find("Text").GetComponent<Text>();
                    if (btn != null && btn.text == "Back")
                    {
                        btn.text = "返回";
                    }
                }
                else
                {
                    Debug.LogError("[AccountPanel] FooterButtons/BackButton not find!");
                }
            }
        }


    }
}
