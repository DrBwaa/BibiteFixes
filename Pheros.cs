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

namespace BibiteFixes {
    [HarmonyPatch(typeof(Pherosense), nameof(Pherosense.PherosenseAround))]
    public static class PherosenseTick {
        // TODO: Log configuration options
        
        private static readonly AccessTools.FieldRef<Pherosense, float> ProgressRef = AccessTools.FieldRefAccess<Pherosense, float>("progress");

        static bool Prefix(Pherosense __instance) {
            float SensePeriod = NEATBrain.brainPeriod;
            if (Plugin.Instance.Settings.UseStaticPheroSenseTimer) {
                SensePeriod = Plugin.Instance.Settings.PheroTimerOverrideSeconds;
            }

            float progress = ProgressRef(__instance) + Time.fixedDeltaTime;
            if (progress < SensePeriod) {
                ProgressRef(__instance) = progress;
                return false;
            }
            ProgressRef(__instance) = progress - SensePeriod;
            return true;
        }
    }

    [HarmonyPatch(typeof(BibitePheromoneOrgan), "UpdateOrgan")]
    public static class PheromoneCost {
        private static readonly AccessTools.FieldRef<BibitePheromoneOrgan, float> PheromoneCostRef = AccessTools.FieldRefAccess<BibitePheromoneOrgan, float>("pheromoneCost");
        private static readonly AccessTools.FieldRef<BibitePheromoneOrgan, NEATBrain> BrainRef = AccessTools.FieldRefAccess<BibitePheromoneOrgan, NEATBrain>("brain");

        static void Postfix(BibitePheromoneOrgan __instance) {
            NEATBrain brain = BrainRef(__instance);
            if (brain == null) { return; }

            float r = brain.Output(NEATBrain.Outputs.PhereOut1);
            float g = brain.Output(NEATBrain.Outputs.PhereOut2);
            float b = brain.Output(NEATBrain.Outputs.PhereOut3);

            if (Mathf.Abs(r) + Mathf.Abs(g) + Mathf.Abs(b) <= 0.05f) {
                PheromoneCostRef(__instance) = 0f;
            }
        }
    }
}
