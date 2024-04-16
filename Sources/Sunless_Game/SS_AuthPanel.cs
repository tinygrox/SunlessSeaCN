using HarmonyLib;
using Sunless.Game.UI.Components;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(AuthPanel))]
    public class SS_AuthPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(ILogin)])]
        public static IEnumerable<CodeInstruction> SSPatch_AuthPanel(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "If you have forgotten your password, enter your email address and click here to begin the password reset process", "如果您忘记了密码，请输入您的电子邮件地址，然后单击此处开始密码重置。");
                //SS_Utility.ILReplacer(ref instructions, "You have been sent an email to begin the password reset process", "我们已向您发送了一封用于重置密码的电子邮件。"); // 去看 BaseProvider
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(AuthPanel.SetLoginButtonState), new[] { typeof(bool) })]
        public static IEnumerable<CodeInstruction> SSPatch_SetLoginButtonState(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Login"] = "登录",
                    ["Connecting..."] = "正在连接..."
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(AuthPanel.IsValid), MethodType.Getter)]
        public static IEnumerable<CodeInstruction> SSPatch_IsValid(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["enter password"] = "输入密码",
                    ["enter a valid email address"] = "输入正确的电子邮件地址"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }

        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(ILogin)])]
        static void SSPatch_AuthPanel()
        {
            SS_Utility.PatchHelper(() =>
            {
                var AuthPanel = GameObject.Find("AuthPanel(Clone)");
                if (AuthPanel != null)
                {
                    // 标题
                    var Title = AuthPanel.transform.Find("Title");
                    if (Title != null)
                    {
                        var TitleText = Title.GetComponent<Text>();
                        if (TitleText != null && TitleText.text == "Login to Fallen London")
                        {
                            TitleText.text = "登录到伦敦坠落";
                        }
                    }

                    // 邮箱地址输入框的缺省值
                    var UserName_PlaceHolder = AuthPanel.transform.Find("Content/Username/Placeholder");
                    if (UserName_PlaceHolder != null)
                    {
                        var PlaceHolder = UserName_PlaceHolder.GetComponent<Text>();
                        if (PlaceHolder != null && PlaceHolder.text == "Email address")
                        {
                            PlaceHolder.text = "电子邮箱地址";
                        }
                    }

                    // 密码输入框缺省值
                    var Password_PlaceHolder = AuthPanel.transform.Find("Content/Password/Placeholder");
                    if (Password_PlaceHolder != null)
                    {
                        var PlaceHolder = Password_PlaceHolder.GetComponent<Text>();
                        if (PlaceHolder != null && PlaceHolder.text == "Password")
                        {
                            PlaceHolder.text = "密码";
                        }
                    }

                    // 登录
                    var LoginButton = AuthPanel.transform.Find("Content/LoginOptions/LoginButton/Text");
                    if (LoginButton != null)
                    {
                        var LoginButtonText = LoginButton.GetComponent<Text>();
                        if (LoginButtonText != null && LoginButtonText.text == "Login")
                        {
                            LoginButtonText.text = "登录";
                        }
                    }

                    // 忘记密码
                    var ResetButton = AuthPanel.transform.Find("Content/LoginOptions/ResetPassword");
                    if (ResetButton != null)
                    {
                        var ResetButtonText = ResetButton.GetComponent<Text>();
                        if (ResetButtonText != null && ResetButtonText.text == "Forgot password")
                        {
                            ResetButtonText.text = "忘记密码";
                        }
                    }

                    // 注释
                    var Notes = AuthPanel.transform.Find("Content/Notes");
                    if (Notes != null) { }
                    {
                        var NotesText = Notes.GetComponent<Text>();
                        if (NotesText != null && NotesText.text == "Or if you don't have an account:")
                        {
                            NotesText.text = "或如果你还没有账号";
                        }
                    }

                    // 注册按钮
                    var RegisterButton = AuthPanel.transform.Find("Content/RegisterButton/Text");
                    if (RegisterButton != null)
                    {
                        var RegisterButtonText = RegisterButton.GetComponent<Text>();
                        if (RegisterButtonText != null && RegisterButtonText.text == "Register")
                        {
                            RegisterButtonText.text = "注册";
                        }
                    }

                    // 取消按钮
                    var CancelButton = AuthPanel.transform.Find("CancelButton/Text");
                    if (CancelButton != null)
                    {
                        var CancelButtonText = CancelButton.GetComponent<Text>();
                        if (CancelButtonText != null && CancelButtonText.text == "Cancel")
                        {
                            CancelButtonText.text = "取消";
                        }
                    }
                }
            });
        }
    }
}
