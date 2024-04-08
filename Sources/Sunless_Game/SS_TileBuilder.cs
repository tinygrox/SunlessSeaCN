using HarmonyLib;
using Sunless.Game.Editor.MonoBehaviours;
using Sunless.Game.Entities.Geography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(TileBuilder))]
    public class SS_TileBuilder
    {
        [HarmonyTranspiler]
        [HarmonyPatch("RenderUI", new[] { typeof(Tile[]) })]
        public static IEnumerable<CodeInstruction> SSPatch_RenderUI(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Select a tile:", "选择地块：");
            SS_Utility.ILReplacer(ref instructions, "Reveal all terrain", "显露所有地形");
            SS_Utility.ILReplacer(ref instructions, "Reveal all labels", "显露所有地点");
            return instructions;
        }
    }
}
