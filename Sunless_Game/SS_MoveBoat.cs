using HarmonyLib;
using Sunless.Game.Scripts.AI;
using Sunless.Game.Scripts.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(MoveBoat))]
    public class SS_MoveBoat
    {
        [HarmonyTranspiler]
        [HarmonyPatch("MessageWithFragments", new[] { typeof(string), typeof(int) })]
        public static IEnumerable<CodeInstruction> SSPatch_MessageWithFragments(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["<b>("] = " <b>(获得了",
                    [" fragments gained)</b>"] = "碎片)</b>"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("DiscoverBeastie", new[] { typeof(CombatCollision) })]
        public static IEnumerable<CodeInstruction> SSPatch_DiscoverBeastie(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Sighted ", "发现");
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("CheckFragments")]
        public static IEnumerable<CodeInstruction> SSPatch_CheckFragments(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["You have gained a "] = "你获得了",
                    ["! Speak to your officers to improve your abilities."] = "！跟你的麾下军官交谈来提升能力。"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });

            return instructions;
        }
    }
}
