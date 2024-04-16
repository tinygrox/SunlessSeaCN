using HarmonyLib;
using Sunless.Game.Entities;
using Sunless.Game.UI.Gazetteer;
using Sunless.Game.UI.Hold;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(ShipLayoutPanel))]
    public class SS_ShipLayoutPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(Gazetteer), typeof(SunlessCharacter), typeof(Action) })]
        public static IEnumerable<CodeInstruction> SSPatch_ShipLayoutPanel(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Unnamed Ship", "未命名的舰船");
                SS_Utility.ILReplacer(ref instructions, "Click to rename your ship", "点此重命名你的舰船");
            });
            return instructions;
        }
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(Gazetteer), typeof(SunlessCharacter), typeof(Action) })]
        static void SSPatch_ShipLayoutPanel(ref RawImage ____shipImage, ref GameObject ____weaponTable)
        {
            //Debug.Log($"Current ShipImage.texture.name = {____shipImage.texture.name}");
            switch (____shipImage.texture.name)
            {
                case "Boat_Basic":
                    ____shipImage.texture = SS_Utility.GetImageTexture2D("Boat_Basic.png");
                    break;
                case "Caligo":
                    ____shipImage.texture = SS_Utility.GetImageTexture2D("Caligo.png");
                    break;
                case "SteamLaunch":
                    ____shipImage.texture = SS_Utility.GetImageTexture2D("SteamLaunch.png");
                    break;
                case "CladeryHeart":
                    ____shipImage.texture = SS_Utility.GetImageTexture2D("CladeryHeart.png");
                    break;
                case "Corvette":
                    ____shipImage.texture = SS_Utility.GetImageTexture2D("Corvette.png");
                    break;
                case "Eschatologue":
                    ____shipImage.texture = SS_Utility.GetImageTexture2D("Eschatologue.png");
                    break;
                case "Lampad":
                    ____shipImage.texture = SS_Utility.GetImageTexture2D("Lampad.png");
                    break;
                case "Leucothea":
                    ____shipImage.texture = SS_Utility.GetImageTexture2D("Leucothea.png");
                    break;
                case "Maenad":
                    ____shipImage.texture = SS_Utility.GetImageTexture2D("Maenad.png");
                    break;
            }

            //Debug.Log($"Current WeaponTable.name = {____weaponTable.name}, expect 'WeaponStats'");
            SS_Utility.UpdateGameObjectText(____weaponTable, "Header/Text", "Weapon Statistics", "武器数据");
            SS_Utility.UpdateGameObjectText(____weaponTable, "Deck/SlotName", "Deck", "甲板");
            SS_Utility.UpdateGameObjectText(____weaponTable, "Forward/SlotName", "Forward", "船艏");
            SS_Utility.UpdateGameObjectText(____weaponTable, "Aft/SlotName", "Aft", "船艉");
        }
    }
}
