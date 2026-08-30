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

        public Settings Settings { get; private set; }
        
        private Dictionary<string, string> patchNameToModName;
        
        internal static ManualLogSource Log;

        private Harmony harmony;

        public void Awake() {
            Instance = this;
            Log = Logger;

            Settings = new Settings(Config);
            InitSettingMap();

            DoPatches();
        }

        // Internal lookup from each Patch to the "mod name" it's connected to.
        // Used to determine whether each patch is enabled.
        private void InitSettingMap() {
            patchNameToModName = new Dictionary<string, string>() {
                { "DisableReaper", "ReaperFix" },
                { "Phero3HeadingEvolvable", "Phero3HeadingFix" },
                { "FullMouthFix", "FullMouthFix" },
                { "PherosenseTick", "PheroSenseFix" },
                { "PheromoneCost", "PheroCostFix" },
                { "TPSFix", "ConstantTPS" },
                { "SaveSpeedOnReload", "AutosaveSpeedFix" },
                { "SetSpeedOnReload", "AutosaveSpeedFix" }
            };
        }

        private void DoPatches() {
            if (harmony == null) {
                harmony = new Harmony("bibites.bibitefixes");
            }

            foreach (Type type in typeof(Plugin).Assembly.GetTypes()) {
                if (type.GetCustomAttribute<HarmonyPatch>() == null) continue;
                if (ShouldLoadPatch(type.Name)) {
                    try {
                        harmony.CreateClassProcessor(type).Patch();
                        Log.LogInfo($"Patching '{type.Name}'...");
                    }
                    catch (Exception e) {
                        Log.LogError($"'{type.Name}' could not be patched: {e}");
                    }
                } else {
                    Log.LogInfo($"Not patching '{type.Name}': not enabled.");
                }
            }
        }

        private bool ShouldLoadPatch(string patchName) {
            return patchNameToModName.ContainsKey(patchName) && Settings.IsModEnabled(patchNameToModName[patchName]);
        }
    }

    public class Settings {

        private ConfigEntry<bool> reaperFixEnabled;
        public bool ReaperFixEnabled => reaperFixEnabled.Value;
        
        private ConfigEntry<bool> phero3HeadingFixEnabled;
        public bool Phero3HeadingFixEnabled => phero3HeadingFixEnabled.Value;
        
        private ConfigEntry<bool> fullMouthFixEnabled;
        public bool FullMouthFixEnabled => fullMouthFixEnabled.Value;
        
        private ConfigEntry<bool> pheroSenseFixEnabled;
        public bool PheroSenseFixEnabled => pheroSenseFixEnabled.Value;
        
        private ConfigEntry<bool> pheroCostFixEnabled;
        public bool PheroCostFixEnabled => pheroCostFixEnabled.Value;
        
        private ConfigEntry<bool> constantTPSEnabled;
        public bool ConstantTPSEnabled => constantTPSEnabled.Value;
        
        private ConfigEntry<bool> autosaveSpeedFixEnabled;
        public bool AutosaveSpeedFixEnabled => autosaveSpeedFixEnabled.Value;

        private ConfigEntry<bool> useStaticPheroSenseTimer;
        public bool UseStaticPheroSenseTimer => useStaticPheroSenseTimer.Value;

        private ConfigEntry<float> pheroTimerOverrideSeconds;
        public float PheroTimerOverrideSeconds => pheroTimerOverrideSeconds.Value;

        private Dictionary<string, bool> modNameToEnabledVal;

        private ConfigFile config;

        public Settings(ConfigFile config) {
            this.config = config;
            
            BindConfig();
            InitLookup();
        }

        private void BindConfig() {
            // ReaperFix
            reaperFixEnabled = config.Bind(
                "ReaperFix",
                "Enabled",
                true,
                "Forces the Reaper trait to respect the Easter-egg setting (enabled/disabled)."
            );

            // Phero3HeadingFix
            phero3HeadingFixEnabled = config.Bind(
                "Phero3HeadingFix",
                "Enabled",
                true,
                "Makes Phero3Heading evolvable."
            );

            // FullMouthFix
            fullMouthFixEnabled = config.Bind(
                "FullMouthFix",
                "Enabled",
                true,
                "Fixes a bug where holding ten objects at once would prevent eating (ever again)."
            );

            // PheroCostFix
            pheroCostFixEnabled = config.Bind(
                "PheroCostFix",
                "Enabled",
                true,
                "Fixes a bug where the pheromone cost would show a stale value in the UI."
            );

            // PheroSenseFix
            pheroSenseFixEnabled = config.Bind(
                "PheroSenseFix",
                "Enabled",
                true,
                "Fixes a bug where pheromone senses were being processed every game tick, even when the brain wouldn't update."
            );
            
            useStaticPheroSenseTimer = config.Bind(
                "PheroSenseFix",
                "UseStaticPheroSenseTimer",
                false,
                "If false, pherosensing will occur on each brain tick.\nIf `true`, overrides the default pherosense timer fix with the constant value of `PheroTimerOverrideSeconds` instead."
            );
                                         
            pheroTimerOverrideSeconds = config.Bind(
                "PheroSenseFix",
                "PheroTimerOverrideSeconds",
                0.5f,
                "The time, in seconds, to wait between each pheromone recalculation. This setting only applies if `UseStaticPheroSenseTimer` is set to `true`.\nHigher values improve performance, but may negatively impact bibites evolved without this setting.\nThe creator has stated that 0.5s was the intended behavior."
            );

            // ConstantTPS
            constantTPSEnabled = config.Bind(
                "ConstantTPS",
                "Enabled",
                true,
                "Prevents the game from inserting extra sim ticks below 1x simulation speed."
            );

            // AutosaveSpeedFix
            autosaveSpeedFixEnabled = config.Bind(
                "AutosaveSpeedFix",
                "Enabled",
                true,
                "Preserves the target simulation speed setting when auto-reloading after an autosave."
            );
        }

        private void InitLookup() {
            modNameToEnabledVal = new Dictionary<string, bool>() {
                { "ReaperFix", ReaperFixEnabled },
                { "Phero3HeadingFix", Phero3HeadingFixEnabled },
                { "FullMouthFix", FullMouthFixEnabled },
                { "PheroSenseFix", PheroSenseFixEnabled },
                { "PheroCostFix", PheroCostFixEnabled },
                { "ConstantTPS", ConstantTPSEnabled },
                { "AutosaveSpeedFix", AutosaveSpeedFixEnabled }
            };
        }

        public bool IsModEnabled(string name) {
            return modNameToEnabledVal.ContainsKey(name) && modNameToEnabledVal[name];
        }
    }
}
