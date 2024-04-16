using HarmonyLib;
using Sunless.Game.Scripts.InputControls;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(InputProvider))]
    public class SS_InputProvider
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(InputProvider.ToggleRepair))]
        public static IEnumerable<CodeInstruction> SSPatch_ToggleRepair(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "We've suffered heavy damage. We need to return to London to make repairs.", "我们遭受了严重的损坏，需要返回伦敦进行维修。");
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("BatSiginalUpdate")]
        public static IEnumerable<CodeInstruction> SSPatch_BatSiginalUpdate(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Clicking this will add an icon to your chart.", "单击此处将会在你的海图上生成一个标志图标。");
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(InputProvider.GetSubmergedCityIndication))]
        public static IEnumerable<CodeInstruction> SSPatch_GetSubmergedCityIndication(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["The sonar has picked up an underwater settlement"] = "声呐探测到水下定居点",
                    [", it is "] = "，位于",
                    [" to the "] = "，我们的"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });

            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(InputProvider.GetBatSignal))]
        public static IEnumerable<CodeInstruction> SSPatch_GetBatSignal(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["There is something underwater"] = "有东西在水下",
                    [" is "] = "位于",
                    [" to the "] = "，我们的",
                    ["(Click to mark on your chart)"] = "(点击生成标记到你的海图上)",
                    ["There are no islands within the zee-bat's range."] = "海蝙蝠侦察范围内未搜寻到岛屿。"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });

            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("GetFuzzyDistance", new[] { typeof(float) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetFuzzyDistance(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["close by"] = "附近(200m内)",
                    ["some distance"] = "一定距离(800m内)",
                    ["a long way"] = "很远处(800m外)"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });

            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("GetCompassPoint", new[] { typeof(float) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetCompassPoint(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["West"] = "西方",
                    ["Southwest"] = "西南方",
                    ["South"] = "南方",
                    ["Southeast"] = "东南方",
                    ["East"] = "东方",
                    ["Northeast"] = "东北方",
                    ["North"] = "北方",
                    ["Northwest"] = "西北方"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
