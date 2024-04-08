using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(BaseProvider))]
    public class SS_BaseProvider
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(BaseProvider.Display), [typeof(string), typeof(Vector3)])]
        public static void SSPatch_Display(ref string message)
        {
            if (message == "You have been sent an email to begin the password reset process")
            {
                message = "我们已向您发送了一封用于重置密码的电子邮件。";
            }
        }
    }
}
