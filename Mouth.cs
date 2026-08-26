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
    [HarmonyPatch(typeof(BibiteMouth), "UpdateOrgan")] // credits to Wrightshoe / rogerwrightshoe
    public static class MouthFix
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            bool found = false;
            for (int i = 0; i < list.Count - 2; i++)
            {
                bool match = list[i].opcode == OpCodes.Ldarg_0
                    && list[i + 1].opcode == OpCodes.Ldc_I4_0
                    && list[i + 2].opcode == OpCodes.Stfld
                    && list[i + 2].operand.ToString().Contains("nInMouth");
                if (match)
                {
                    FieldInfo objectsInMouthField = AccessTools.Field(typeof(BibiteMouth), "objectsInMouth");
                    List<CodeInstruction> replacement = new List<CodeInstruction>
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, objectsInMouthField),
                    new CodeInstruction(OpCodes.Ldlen),
                    new CodeInstruction(OpCodes.Conv_I4)
                };
                    list.RemoveAt(i + 1);
                    list.InsertRange(i + 1, replacement);
                    found = true;
                    Plugin.Log.LogInfo("MouthFix: replaced 'nInMouth = 0' fallback successfully.");
                    break;
                }
            }
            if (!found)
            {
                Plugin.Log.LogWarning("MouthFix: target pattern not found, patch not applied.");
            }
            return list.AsEnumerable();
        }
    }
}
