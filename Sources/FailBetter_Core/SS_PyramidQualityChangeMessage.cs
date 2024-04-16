using FailBetter.Core;
using FailBetter.Core.Enums;
using FailBetter.Core.Result.QualityChangeMessages;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(PyramidQualityChangeMessage))]
    public class SS_PyramidQualityChangeMessage
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Quality), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_PyramidQualityChangeMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["</strong> has increased to "] = "</strong>已经增长至",
                    ["My "] = "我的",
                    [" has increased to "] = "已经增长至",
                    ["</strong> has dropped to "] = "</strong>已经下降至",
                    [" has dropped to "] = "已经下降至",
                    ["</strong> is increasing..."] = "</strong> 正在增长…",
                    [" is increasing..."] = "正在增长…",
                    [" is dropping..."] = "正在下降…",
                    [" change "] = "变动",
                    [", "] = "，还需",
                    [" more needed to reach level "] = "点以上才能达到等级",
                    ["[This is a metaquality! It will appear on your user profile, and may unlock new starting options in other worlds.]"] = "[这是一个元特质！它将出现在您的用户个人资料中，并可能在其他世界中解锁新的开始选项。]",
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch("AcquisitionMessageForQualityAndCategory", [typeof(Quality), typeof(int), typeof(Category)])]
        public static IEnumerable<CodeInstruction> SSPatch_AcquisitionMessageForQualityAndCategory(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["A twist in your tale! You are now <strong>{0}</strong>."] = "你的故事发生了一个转折! 你现在是<strong>{0}</strong>。",
                    ["A twist in my tale! I am now {0}."] = "我的故事中发生了一个转折，我现在是{0}了。",
                    ["You've made a friend, or at least a contact: <strong>{0}</strong>."] = "你结识了一个朋友，或者至少是打过照面：<strong>{0}</strong>。",
                    ["I've made a friend, or at least a contact: {0}."] = "我结识了一个朋友，或者至少是打过照面：{0}。",
                    ["An Accomplishment! You are now <strong>{0}</strong>."] = "一段历往！你现在是<strong>{0}</strong>。",
                    ["An Accomplishment! I am now {0}."] = "一段历往！我现在是<strong>{0}</strong>。",
                    ["Begun a new venture! <strong>{0}</strong>"] = "开始了一段新的冒险！<strong>{0}</strong>",
                    ["I have begun a new venture!{0}"] = "我已经开始了一个新的冒险！{0}",
                    ["<strong>{0}</strong> shows your progress in the venture."] = "<strong>{0}</strong>显示你在冒险中的进度。",
                    ["{0} shows my progress in the venture."] = "{0}会显示我在冒险中的进度。",
                    ["Boldly done! You have chosen an Ambition! <strong>{0}</strong>"] = "非常有勇气！你已选择了志向！<strong>{0}</strong>",
                    ["I have chosen an Ambition! {0}"] = "我已选择志向！{0}",
                    ["You've learnt a new route: <strong>{0}</strong>"] = "你获解到一条新航线：<strong>{0}</strong>",
                    ["I've learnt a new route: {0}"] = "我获解到一条新航线：{0}",
                    ["You've gained a new quality: <strong>{0}</strong> at {1}"] = "你获得了一项新特质：<strong>{0}</strong> {1}",
                    ["I've gained a new quality: {0} at {1}"] = "我获得了一项新特质：{0} {1}"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("LossMessageForQualityAndCategory", [typeof(Quality), typeof(int), typeof(Category), typeof(int)])]
        public static IEnumerable<CodeInstruction> SSPatch_LossMessageForQualityAndCategory(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["A twist in your tale! You are no longer <strong>{0}</strong>."] = "你的故事发生了一个转折! 你不再是<strong>{0}</strong>。",
                    ["A twist in my tale! I am no longer {0}."] = "我的故事中发生了一个转折，我不再是{0}了。",
                    ["You've lost a quality: <strong>{0}</strong>."] = "你失去了特质：<strong>{0}</strong>。",
                    ["I've lost a quality: {0}."] = "我失去了一个特质：{0}。",
                    ["You've lost a quality: <strong>{0}</strong>. (but your equipped items still give you an effective level of "] = "你失去了特质：<strong>{0}</strong>。(但你的装备物品仍然为你提供了",
                    [")."] = "的有效级别)",
                    ["I've lost a quality: {0}. (but my equipped items still give me an effective level of "] = "我失去了特质：{0}。(但我的装备物品仍然为我提供了"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
