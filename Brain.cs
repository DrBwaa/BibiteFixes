using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ManagementScripts;
using SettingScripts;
using SimulationScripts.BibiteScripts;
using UnityEngine;

namespace CommonFixes
{
    [HarmonyPatch(typeof(NEATBrain), "GetInputIndex")]
    public static class Phero3HeadingEvolvable {
        static bool Prefix(NEATBrain.Inputs? input, ref int __result) {
            if (input.HasValue)  {
                return true;
            }
            __result = UnityEngine.Random.Range(0, NEATBrain.NInputs);
            return false;
        }
    }
}
