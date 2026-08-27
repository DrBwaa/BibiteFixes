using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ManagementScripts;
using SettingScripts;
using SimulationScripts.BibiteScripts;
using UnityEngine;

namespace BibiteFixes {

    [BepInPlugin("bibites.bibitefixes", "BibiteFixes", "1.0.0")]
    public class Plugin : BaseUnityPlugin {
        public static Plugin Instance { get; private set; }
        
        internal static ManualLogSource Log;

        private Harmony harmony;

        private ConfigEntry<bool> useStaticPheroSenseTimer;
        public bool UseStaticPheroSenseTimer => useStaticPheroSenseTimer.Value;

        private ConfigEntry<float> pheroTimerOverrideSeconds;
        public float PheroTimerOverrideSeconds => pheroTimerOverrideSeconds.Value;

        public void Awake() {
            Instance = this;
            Log = Logger;

            InitConfig();
            DoPatches();
        }

        private void DoPatches() {
            if (harmony == null) {
                harmony = new Harmony("bibites.slimpheros");
            }

            foreach (Type type in typeof(Plugin).Assembly.GetTypes()) {
                if (type.GetCustomAttribute<HarmonyPatch>() == null) continue;
                try {
                    // TODO: Allow filtering specific patches based on config
                    harmony.CreateClassProcessor(type).Patch();
                    Log.LogInfo($"Patching '{type.Name}'...");
                }
                catch (Exception e) {
                    Log.LogError($"'{type.Name}' could not be patched: {e}");
                }
            }
        }

        private void InitConfig() {
            useStaticPheroSenseTimer = Config.Bind(
                "Performance.Pheros",
                "UseStaticPheroSenseTimer",
                false,
                "If `true`, overrides the default pherosense timer fix with the constant value of `PheroTimerOverrideSeconds` instead. If false, pherosensing will occur on each brain tick."
            );
                                         
            pheroTimerOverrideSeconds = Config.Bind(
                "Performance.Pheros",
                "PheroTimerOverrideSeconds",
                0.5f,
                "The time, in seconds, to wait between each pheromone recalculation. This setting only applies if `UseStaticPheroSenseTimer` is set to `true`. Higher values improve performance, but may negatively impact bibites evolved without this setting. The creator has stated that 0.5s was the intended behavior."
            );
            
        }
    }
}
