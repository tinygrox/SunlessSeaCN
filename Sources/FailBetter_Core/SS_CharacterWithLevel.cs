using FailBetter.Core.ResultModel;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(CharacterWithLevel))]
    public class SS_CharacterWithLevel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CharacterWithLevel.LevelDesc), MethodType.Getter)]
        public static IEnumerable<CodeInstruction> SSPatch_classname(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Unknown"] = "籍籍无名",
                    ["Noticed"] = "初露头角",
                    ["Known"] = "声名鹊起",
                    ["Recognised"] = "崭露头角",
                    ["Liked"] = "颇受欢迎",
                    ["Respected"] = "德高望重",
                    ["Well-regarded"] = "有口皆碑",
                    ["Distinguished"] = "声名显赫",
                    ["Celebrated"] = "誉满天下",
                    ["Esteemed"] = "备受推崇",
                    ["Honoured"] = "功勋卓越",
                    ["Adored"] = "爱戴有加",
                    ["Idolized"] = "奉若神明",
                    ["Worshipped"] = "顶礼膜拜",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
