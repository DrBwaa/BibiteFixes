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

namespace BibiteFixes
{
    [HarmonyPatch(typeof(BibiteMouth), "UpdateOrgan")] // credits to Wrightshoe / rogerwrightshoe
    public static class FullMouthFix {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            List<CodeInstruction> instr = new List<CodeInstruction>(instructions);
            bool found = false;
            for (int i = 0; i < instr.Count - 2; i++) {
                bool match = instr[i].opcode == OpCodes.Ldarg_0
                    && instr[i + 1].opcode == OpCodes.Ldc_I4_0
                    && instr[i + 2].opcode == OpCodes.Stfld
                    && instr[i + 2].operand.ToString().Contains("nInMouth");
                if (match) {
                    FieldInfo objectsInMouthField = AccessTools.Field(typeof(BibiteMouth), "objectsInMouth");
                    List<CodeInstruction> replacement = new List<CodeInstruction> {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, objectsInMouthField),
                    new CodeInstruction(OpCodes.Ldlen),
                    new CodeInstruction(OpCodes.Conv_I4)
                };
                    instr.RemoveAt(i + 1);
                    instr.InsertRange(i + 1, replacement);
                    found = true;
                    Plugin.Log.LogInfo("FullMouthFix: target pattern found; patched successfully.");
                    break;
                }
            }
            if (!found) {
                Plugin.Log.LogWarning("FullMouthFix: target pattern not found, patch not applied.");
            }
            return instr.AsEnumerable();
        }
    }
}
