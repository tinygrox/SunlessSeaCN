using FailBetter.Core.ExtensionMethods.EnumExtensionMethods;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(GenreExtensionMethods))]
    public class SS_GenreExtensionMethods
    {
        // 好像完全没有必要去翻译
        [HarmonyPostfix]
        [HarmonyPatch(nameof(GenreExtensionMethods.DisplayText))]
        public static void SSPatch_classname(ref string __result)
        {
            var trans = new Dictionary<string, string>()
            {
                ["Not Yet Specified"] = "尚未指明",
                ["Science Fiction"] = "科幻小说",
                ["Post-Apocalyptic"] = "后启示录",
                ["You Can't Label Me"] = "你无法定义我",
                ["Experimental"] = "实验性",
                ["Drama"] = "戏剧",
                ["Romance"] = "浪漫",
                ["Fantasy"] = "奇幻",
                ["Historical"] = "历史",
                ["Educational"] = "教育",
                ["Comedy"] = "喜剧",
                ["Crime"] = "犯罪",
                ["Horror"] = "恐怖"
            };
            if (trans.ContainsKey(__result))
            {
                __result = trans[__result];
            }
        }
    }
}
