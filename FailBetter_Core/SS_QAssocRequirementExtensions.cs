using FailBetter.Core.QAssocExtensions;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(QAssocRequirementExtensions))]
    public class SS_QAssocRequirementExtensions
    {
        //[HarmonyTranspiler]
        //[HarmonyPatch(nameof(QAssocRequirementExtensions.GetType))]
        //public static IEnumerable<CodeInstruction> SSPatch_classname(IEnumerable<CodeInstruction> instructions)
        //{
        //    return instructions;
        //}
    }
}
