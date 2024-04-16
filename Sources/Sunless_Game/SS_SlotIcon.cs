using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.UI.Icons;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(SlotIcon))]
    public class SS_SlotIcon
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(SlotIcon.UnequipItem))]
        public static IEnumerable<CodeInstruction> SSPatch_UnequipItem(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "We must dock before we change the ship's equipment.", "我们必须先停泊才能更换舰船装备。");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("GetInteractions", [typeof(Quality), typeof(bool)])]
        public static IEnumerable<CodeInstruction> SSPatch_GetInteractions(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Unequip"] = "卸下",
                    ["Use"] = "使用"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }

        //    [HarmonyPrefix]
        //    [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(CharacterQPossession), typeof(Action)])]
        //    public static void SSPatch_SlotIcon(ref GameObject parent, CharacterQPossession qassoc)
        //    {
        //        if (parent == null)
        //        {
        //            Debug.Log($"[SlotIcon] qassoc.AssociatedQuality {qassoc.AssociatedQuality.Name}'s parent is null");
        //            var kv = new Dictionary<Quality, string>()
        //            {
        //                [WellKnownQualityProvider.Aft] = "Aft",
        //                [WellKnownQualityProvider.Auxiliary] = "Auxiliary",
        //                [WellKnownQualityProvider.Bridge] = "Bridge",
        //                [WellKnownQualityProvider.Deck] = "Deck",
        //                [WellKnownQualityProvider.Engines] = "Engines",
        //                [WellKnownQualityProvider.Forward] = "Forward",
        //            };
        //            if (kv.ContainsKey(qassoc.AssociatedQuality))
        //            {
        //                Debug.Log($"[SlotIcon] qassoc.AssociatedQuality's parent should be {kv[qassoc.AssociatedQuality]}");
        //                parent = NavigationProvider.Instance.Anchors.BR.transform?.Find($"Gazeteer(Clone)/InnerOffset/Pages/LeftPage/Scroll View/Viewport/SlotsOnBoat(Clone)/{kv[qassoc.AssociatedQuality]}")?.gameObject;
        //                if (parent == null)
        //                {
        //                    Debug.LogError($"[SlotIcon] parent is null, still");
        //                }

        //            }
        //        }
        //        else
        //            Debug.Log($"[SlotIcon] parent not null and its name = {parent.name}, and has {parent.transform.childCount} Children");
        //    }
    }

    // 不需要 但这是一个很好的例子展示了如何 Patch 带泛型的 class -> 直接指定特定类型
    //[HarmonyPatch(typeof(BaseIcon<CharacterQPossession>))]
    //public class SS_BaseUI
    //{
    //    [HarmonyPrefix]
    //    [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(string), typeof(CharacterQPossession), typeof(string)])]
    //    public static void SSPatch_BaseUI(CharacterQPossession qassoc)
    //    {
    //        var kv = new Dictionary<Quality, string>()
    //        {
    //            [WellKnownQualityProvider.Aft] = "Aft",
    //            [WellKnownQualityProvider.Auxiliary] = "Auxiliary",
    //            [WellKnownQualityProvider.Bridge] = "Bridge",
    //            [WellKnownQualityProvider.Deck] = "Deck",
    //            [WellKnownQualityProvider.Engines] = "Engines",
    //            [WellKnownQualityProvider.Forward] = "Forward",
    //        };
    //        if (kv.ContainsKey(qassoc.AssociatedQuality))
    //        {
    //            Debug.LogWarning($"[BaseIcon] - {qassoc.AssociatedQuality.Name}");
    //            qassoc.AssociatedQuality.Name = kv[qassoc.AssociatedQuality];
    //        }
    //    }
    //}
}
