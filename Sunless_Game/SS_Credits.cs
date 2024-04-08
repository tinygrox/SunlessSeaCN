using HarmonyLib;
using Sunless.Game.ExtraContent;
using Sunless.Game.MonoBehaviours.UI.Credits;
using Sunless.Game.UI.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Credits))]
    public class SS_Credits
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Credits.Content), MethodType.Getter)]
        public static void SSPatch_ContentGetter(ref List<CreditGroup> __result)
        {
            try
            {
                for (int i = 0; i < __result.Count; i++)
                {
                    var cc = __result[i];
                    //Debug.Log($"[SSTranslator] [Credits Screen] - {i} - {cc.Title}");
                    switch (cc.Title)
                    {
                        case "SUNLESS SEA & ZUBMARINER":
                            cc.Title = "无光之海 & ZUBMARINER";
                            cc.Names = "由 Failbetter Games 开发";
                            continue;
                        case "Production, Lead Design & Lead Writing":
                            cc.Title = "产品、首席设计和首席编剧";
                            continue;
                        case "Art":
                            cc.Title = "美术";
                            continue;
                        case "Programming":
                            cc.Title = "编程";
                            continue;
                        case "Writing":
                            cc.Title = "编剧";
                            continue;
                        case "Guest Writing":
                            cc.Title = "特邀编剧";
                            continue;
                        case "Editing, Content Production and Additional Writing":
                            cc.Title = "编辑、内容和辅助编剧";
                            continue;
                        case "Original Music":
                            cc.Title = "游戏音乐";
                            continue;
                        case "Additional Music":
                            cc.Title = "辅助配乐";
                            continue;
                        case "Audio":
                            cc.Title = "音频";
                            continue;
                        case "Animation":
                            cc.Title = "动画";
                            continue;
                        case "Quality Analysis":
                            cc.Title = "质量测试";
                            continue;
                        case "QA & Playtesting":
                            cc.Title = "QA & 游玩测试";
                            cc.Names = "Adam Myers, Chris Gardiner, Kevin O’Connor, Marcus Starling, Caolain Porter, James Long, 所有无光之海的玩家";
                            continue;
                        case "Production":
                            cc.Title = "产品";
                            continue;
                        case "PR & Marketing":
                            cc.Title = "公关 & 营销";
                            continue;
                        case "Additonal Art":
                            cc.Title = "美术补充";
                            continue;
                        case "Failbetter Interns":
                            cc.Title = "Failbetter 实习生";
                            continue;
                        case "Plugins Used":
                            cc.Title = "使用插件";
                            continue;
                        case "Special Thanks":
                            cc.Title = "特别鸣谢";
                            continue;
                        default:
                            break;
                    }
                    //Debug.Log($"[SSTranslator] [Credits Screen] - {i} - AFTER {cc.Title}");
                    //Debug.Log($"[SSTranslator] [Credits Screen] - {i} - AFTER:{Credits.Content[i].Title}");
                }
                __result.Insert(19, new CreditGroup
                {
                    Title = "无光之海界面中文翻译",
                    Names = "tinygrox",
                    Gutter = 50
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
