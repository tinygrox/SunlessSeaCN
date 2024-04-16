using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sunless.Game.UI.Menus;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(MainMenu))]
    public class SS_MainMenu
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(string), typeof(List<MainMenu.ButtonData>), typeof(bool)])]
        public static IEnumerable<CodeInstruction> SSPatch_MainMenu(IEnumerable<CodeInstruction> instructions)
        {
            Dictionary<string, string> trans = new()
            {
                ["Exit to Desktop"] = "退出到桌面",
                ["New Game"] = "新游戏",
                ["Options"] = "设置",
                ["Load Game"] = "载入游戏",
                ["Credits"] = "制作人员",
                ["Quit to Title Screen"] = "回到标题界面",
                ["Only available while docked."] = "仅在停泊时可用。"
            };
            SS_Utility.ILReplacer(instructions, trans);

            return instructions;
        }
        // 我把你妈妈杀，搞了一晚上结果问题不是这个，嗯，就当Debug了
        //[HarmonyPostfix]
        //[HarmonyPatch(MethodType.Constructor, new[] { typeof(GameObject), typeof(string), typeof(List<MainMenu.ButtonData>), typeof(bool) })]
        //public static void SSPatch_MainMenu(ref List<MainMenu.ButtonData> buttons, ref InputField ____uiInput, ref GameObject ____saveScreen, MainMenu __instance)
        //{
        //    Debug.LogWarning($"[SSPatch_MainMenu] Postfix");
        //    if (buttons.Any((MainMenu.ButtonData x) => x.Text == "手动存档"))
        //    {
        //        ____uiInput = ____saveScreen.GetComponentInChildren<InputField>();
        //        ____uiInput.text = GameProvider.Instance.CurrentCharacter.Name;
        //        var method = AccessTools.Method(typeof(MainMenu), "SaveGame");
        //        if (method != null)
        //        {
        //            //Action saveGameAction = (Action)Delegate.CreateDelegate(typeof(Action), null, method);
        //            Debug.LogWarning($"[SSPatch_MainMenu] Save Action pre   ---");
        //            //UnityComponentHelper.OnClick(UnityHelper.FindComponentInDescendant<Button>(____saveScreen, "Save"), new Action(saveGameAction), null);
        //            UnityComponentHelper.OnClick(UnityHelper.FindComponentInDescendant<Button>(____saveScreen, "Back"), new Action(__instance.HideSaveOrLoadScreen), null);
        //            Debug.LogWarning($"[SSPatch_MainMenu] Back Action after ---");
        //        }
        //        var SetupSaveButton = AccessTools.Method(typeof(MainMenu), "SetupSaveButton", [typeof(MainMenu.ButtonData)]);
        //        if (SetupSaveButton != null)
        //        {
        //            var btn = buttons.FirstOrDefault(x => x.Text == "手动存档");
        //            if (btn != null)
        //            {
        //                Debug.LogWarning($"[SSPatch_MainMenu] Postfix --- {btn} {btn.Text}");
        //                SetupSaveButton.Invoke(__instance, [btn]);
        //            }
        //            else
        //                Debug.LogWarning($"[SSPatch_MainMenu] Postfix NONONONONO!!!!!");
        //        }
        //    }
        //}

        [HarmonyTranspiler]
        [HarmonyPatch("SetupSaveButton", new[] { typeof(MainMenu.ButtonData) })]
        public static IEnumerable<CodeInstruction> SSPatch_SetupSaveButton(IEnumerable<CodeInstruction> instructions)
        {
            //Debug.LogWarning($"[SSPatch_MainMenu] Call SetupSaveGame");
            SS_Utility.ILReplacer(ref instructions, "Manual Save", "手动存档");
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("SetupLoadButton", new[] { typeof(MainMenuButton) })]
        public static IEnumerable<CodeInstruction> SSPatch_SetupLoadButton(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Switch to Merciful Mode to load manual saves", "切换到仁慈模式再加载手动存档");
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MainMenu.ConfirmFallenLondon))]
        public static IEnumerable<CodeInstruction> SSPatch_ConfirmFallenLondon(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Yes please"] = "是，请开始",
                    ["No, thank you"] = "否，谢谢你",
                    ["Play Fallen London"] = "游玩坠落沦敦",
                    ["Explore Fallen London more deeply in the original browser game? It's free to play, but civilised free-to-play."] = "在原初的网页游戏中更加深入的探索沦敦？这是一款免费游戏，文明的免费的游戏。"
                };
                //SS_Utility.ILReplacer(ref instructions, "Yes please", "是，请开始");
                //SS_Utility.ILReplacer(ref instructions, "No, thank you", "否，谢谢你");
                //SS_Utility.ILReplacer(ref instructions, "Play Fallen London", "游玩沦敦");
                //SS_Utility.ILReplacer(ref instructions, "Explore Fallen London more deeply in the original browser game? It's free to play, but civilised free-to-play.", "在原初的网页游戏中更加深入的探索沦敦？这是一款免费游戏，文明的免费的游戏。");
                SS_Utility.ILReplacer(instructions, trans);
            });

            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MainMenu.WarnAboutMercifulMode))]
        public static IEnumerable<CodeInstruction> SSPatch_WarnAboutMercifulMode(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var tans = new Dictionary<string, string>()
                {
                    ["Merciful Mode"] = "仁慈模式",
                    ["In Unforgiving Mode, Sunless Sea autosaves whenever you dock, and whenever you die. You may switch to Merciful Mode and save manually. This will remove your character's Invictus Token."] = "在非仁慈模式下，游戏将在玩家停靠和死亡时自动保存.您可以切换到仁慈模式来手动保存,但您将会失去此存档的不可战胜之印章。"
                };
                SS_Utility.ILReplacer(instructions, tans);
            });
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MainMenu.ExitToMainMenu))]
        public static IEnumerable<CodeInstruction> SSPatch_ExitToMainMenu(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Quit to main menu"] = "退回到主菜单",
                    ["Are you sure you want to quit?"] = "你确定你要退出吗？"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });

            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MainMenu.ConfirmNewgame))]
        public static IEnumerable<CodeInstruction> SSPatch_ConfirmNewgame(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Start new game"] = "开始新游戏",
                    ["Are you sure?"] = "你确定吗？"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("ShowActivateDialog", new[] { typeof(string) })]
        public static IEnumerable<CodeInstruction> SSPatch_ShowActivateDialog(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Activation code"] = "激活码",
                    ["Use this activation code, unless you have another."] = "使用此激活码，或输入其他的。",
                    ["Activate"] = "激活"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("UpdateContent")]
        public static IEnumerable<CodeInstruction> SSPatch_UpdateContent(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Importing..", "正在导入…");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("LoadGame")]
        public static IEnumerable<CodeInstruction> SSPatch_LoadGame(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "You must choose a game to load", "你必须选择游戏存档才能加载");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("SaveGame")]
        public static IEnumerable<CodeInstruction> SSPatch_SaveGame(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Invalid save name"] = "存档名非法",
                    ["Overwrite save"] = "覆盖存档",
                    ["Are you sure you want to overwrite "] = "你确定要覆盖 "
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("ExitGame")]
        public static IEnumerable<CodeInstruction> SSPatch_ExitGame(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Exit game"] = "退出游戏",
                    ["Are you sure you want to quit?"] = "你确定要退出吗？"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MainMenu.SetUpdateButtonState))]
        public static IEnumerable<CodeInstruction> SSPatch_SetUpdateButtonState(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            SS_Utility.PatchHelper(() =>
            {
                CodeInstruction call = CodeInstruction.Call(typeof(SS_MainMenu), nameof(SS_MainMenu.UpdateButtonValues));
                codes.Insert(0, call);
            });
            //return instructions;
            return codes.AsEnumerable();
        }

        static void UpdateButtonValues()
        {
            var innerclass = AccessTools.Inner(typeof(MainMenu), "UpdateButtonValues");
            if (innerclass != null)
            {
                Traverse.Create(innerclass).Field(nameof(MainMenu.UpdateButtonValues.NoConnection)).SetValue("(无法连接)");
                Traverse.Create(innerclass).Field(nameof(MainMenu.UpdateButtonValues.NoNewContent)).SetValue("故事已是最新！");
                Traverse.Create(innerclass).Field(nameof(MainMenu.UpdateButtonValues.GetContent)).SetValue("新可用故事！");
                Traverse.Create(innerclass).Field(nameof(MainMenu.UpdateButtonValues.Checking)).SetValue("正在检查更新……");
                Traverse.Create(innerclass).Field(nameof(MainMenu.UpdateButtonValues.OldSoftware)).SetValue("需要游戏版本为最新");
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch("ShowLoadScreen")]
        static void SSPatch_ShowLoadScreen(ref GameObject ____loadScreen)
        {
            //Debug.Log($"ShowLoadScreen GameObject.Name = {____loadScreen.name}");
            //var Title = ____loadScreen.transform.Find("Title");
            //if (Title != null)
            //{
            //    var TitleText = Title.GetComponent<Text>();
            //    if (TitleText != null && TitleText.text == "Load Game")
            //    {
            //        TitleText.text = "载入游戏";
            //    }
            //}
            SS_Utility.UpdateGameObjectText(____loadScreen, "Title", "Load Game", "载入游戏");

            //var LoadButton = ____loadScreen.transform.Find("FooterButtons/Load/Text");
            //if (LoadButton != null)
            //{
            //    var LoadButtonText = LoadButton.GetComponent<Text>();
            //    if (LoadButtonText != null && LoadButtonText.text == "Load")
            //    {
            //        LoadButtonText.text = "载入";
            //    }
            //}
            SS_Utility.UpdateGameObjectText(____loadScreen, "FooterButtons/Load/Text", "Load", "载入");

            //var BackButton = ____loadScreen.transform.Find("FooterButtons/Back/Text");
            //if (BackButton != null)
            //{
            //    var BackButtonText = BackButton.GetComponent<Text>();
            //    if (BackButtonText != null && BackButtonText.text == "Back")
            //    {
            //        BackButtonText.text = "返回";
            //    }
            //}
            SS_Utility.UpdateGameObjectText(____loadScreen, "FooterButtons/Back/Text", "Back", "返回");
        }
        [HarmonyPostfix]
        [HarmonyPatch("ShowSaveScreen")]
        static void SSPatch_ShowSaveScreen(ref GameObject ____saveScreen)
        {
            SS_Utility.UpdateGameObjectText(____saveScreen, "Title", "Save Game", "保存游戏");
            SS_Utility.UpdateGameObjectText(____saveScreen, "FooterButtons/Save/Text", "Save", "保存");
            SS_Utility.UpdateGameObjectText(____saveScreen, "FooterButtons/Back/Text", "Back", "返回");
        }
    }
}
