using HarmonyLib;
using Sunless.Game.UI.Legacy;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(LegacyPanel))]
    public class SS_LegacyPanel
    {
        //[HarmonyTranspiler]
        //[HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(string), typeof(bool)])]
        //public static IEnumerable<CodeInstruction> SSPatch_LegacyPanel(IEnumerable<CodeInstruction> instructions)
        //{

        //    return instructions;
        //}
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(string), typeof(bool)])]
        public static void SSPatch_LegacyPanel(GameObject parent, ref Text ____legacyTitle)
        {
            if (parent == null || parent.name != "Anchor_CENTER") return; // 铁是 Anchor_CENTER

            if (____legacyTitle != null)
            {
                ____legacyTitle.text = ____legacyTitle.text.Replace("You were ", "你曾是");
                ____legacyTitle.text = ____legacyTitle.text.Replace("'s:", "的：");
            }
            SS_Utility.PatchHelper(() =>
            {
                // HeaderJoy
                var HeaderJoyImage = parent.transform.Find("Legacy(Clone)/Column1/DeathCertificate/HeaderJoy");
                if (HeaderJoyImage != null)
                {
                    var oldSprite = HeaderJoyImage.GetComponent<Image>();
                    if (oldSprite != null)
                    {
                        oldSprite.sprite = SS_Utility.GetSprite(HeaderJoyImage, "legacy_header_joy.png");
                    }
                }

                //In Memoriam
                //var myHeaderMemoriamTexture = SS_Utility.GetImageTexture2D("legacy_header_memoriam.png");
                var HeaderMemoriamImage = parent.transform.Find("Legacy(Clone)/Column1/DeathCertificate/HeaderMemoriam");
                if (HeaderMemoriamImage != null)
                {
                    var oldSprite = HeaderMemoriamImage.GetComponent<Image>();
                    //HeaderMemoriamImage.sprite = SS_Utility.GetSprite(HeaderMemoriamImage, "legacy_header_memoriam.png");
                    oldSprite.sprite = SS_Utility.GetSprite(HeaderMemoriamImage, "legacy_header_memoriam.png");
                }

                // 左侧页面的下方按钮MAIN MENU
                SS_Utility.UpdateGameObjectText(parent, "Legacy(Clone)/Column1/MainMenu", "main menu", "主菜单");

                // 左侧页面的下方小标题
                SS_Utility.UpdateGameObjectText(parent, "Legacy(Clone)/Column1/DeathCertificate/Scroll View/Viewport/Content/QualitiesUnlocked", "Legacy Qualities Unlocked:", "职业特质解锁：");

                // 右侧页面的下方按钮
                SS_Utility.UpdateGameObjectText(parent, "Legacy(Clone)/Column2/TitleScreen", "quit to title screen", "退出到标题界面");

                // 右侧页面标题文本
                //SS_Utility.UpdateGameObjectText(parent, "Legacy(Clone)/Column2/Redemption/PaperHeader", "", "");
                var WoRImage = parent.transform.Find("Legacy(Clone)/Column2/Redemption/PaperHeader");
                if (WoRImage != null)
                {
                    var oldSprite = WoRImage.GetComponent<Image>();
                    oldSprite.sprite = SS_Utility.GetSprite(WoRImage, "legacy_header_redemption.png");
                }

                SS_Utility.UpdateGameObjectText(parent, "Legacy(Clone)/Column2/Redemption/Scroll View/Viewport/Content/ConfirmTitle", "Please confirm the following information:", "请确认以下信息：");

                // 右侧页面应用按钮
                SS_Utility.UpdateGameObjectText(parent, "Legacy(Clone)/Column2/Redemption/ButtonContainer/AcceptButton/Text", "ACCEPT LEGACY", "应用遗留之物");

                // 右侧页面 0 1
                SS_Utility.UpdateGameObjectText(parent, "Legacy(Clone)/Column2/Redemption/Scroll View/Viewport/Content/H_Layout/Col1/Header/Text", "Starting combat stats", "起始战斗属性");

                SS_Utility.UpdateGameObjectText(parent, "Legacy(Clone)/Column2/Redemption/Scroll View/Viewport/Content/H_Layout/Col2/Header/Text", "Starting wealth", "起始财富");

                // 4 - 3
                var ChartHeader = parent.transform.Find("Legacy(Clone)/Column2/Redemption/Scroll View/Viewport/Content")?.GetChild(4)?.Find("Col2")?.GetChild(3).gameObject;
                SS_Utility.UpdateGameObjectText(ChartHeader, "Text", "Starting chart", "起始海图");

                // 6 -  
                var Inherited = parent.transform.Find("Legacy(Clone)/Column2/Redemption/Scroll View/Viewport/Content")?.GetChild(6)?.gameObject;
                SS_Utility.UpdateGameObjectText(Inherited, "Col1/Header/Text", "Inherited officer", "继承军官");
                SS_Utility.UpdateGameObjectText(Inherited, "Col2/Header/Text", "Inherited Weapon", "继承武器");

            });
        }

        [HarmonyPostfix]
        [HarmonyPatch("GetFormattedDeathDescription", [typeof(string)])]
        public static void SSPatch_GetFormattedDeathDescription(ref string __result)
        {
            __result = __result.Replace("Retired ", "退休于 ");
            __result = __result.Replace("Died ", "死于 ");
        }
        [HarmonyPostfix]
        [HarmonyPatch(nameof(LegacyPanel.UpdateStats))]
        static void UpdateStats(ref Text ____ironStat, ref Text ____mirrorsStat, ref Text ____veilsStat, ref Text ____pagesStat, ref Text ____heartsStat, ref Text ____chartLabel)
        {
            if (____ironStat.text.Contains("<b>Iron:</b> "))
                ____ironStat.text = ____ironStat.text.Replace("<b>Iron:</b> ", "<b>钢铁值：</b> ");
            if (____mirrorsStat.text.Contains("<b>Mirrors:</b> "))
                ____mirrorsStat.text = ____mirrorsStat.text.Replace("<b>Mirrors:</b> ", "<b>镜子值：</b> ");
            if (____veilsStat.text.Contains("<b>Veils:</b> "))
                ____veilsStat.text = ____veilsStat.text.Replace("<b>Veils:</b> ", "<b>面纱值：</b> ");
            if (____heartsStat.text.Contains("<b>Hearts:</b> "))
                ____heartsStat.text = ____heartsStat.text.Replace("<b>Hearts:</b> ", "<b>红心值：</b> ");
            if (____pagesStat.text.Contains("<b>Pages:</b> "))
                ____pagesStat.text = ____pagesStat.text.Replace("<b>Pages:</b> ", "<b>书页值：</b> ");
            if (____chartLabel.text.Contains("'s Chart"))
                ____chartLabel.text = ____chartLabel.text.Replace("'s Chart", "的海图");
            if (____chartLabel.text.Contains("<i>None (requires Correspondent legacy)</i>"))
                ____chartLabel.text = ____chartLabel.text.Replace("<i>None (requires Correspondent legacy)</i>", "<i>无 (需要新闻通讯员)</i>");


        }
    }
}
