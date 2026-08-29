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
    [HarmonyPatch(typeof(TimeController), "UpdateTimeScale")]
    public static class TPSFix {
        private static readonly FieldInfo BaseFixedDeltaTimeField = AccessTools.Field(typeof(TimeController), "baseFixedDeltaTime");

        static bool Prefix(float val) {
            Time.timeScale = val;
            TimeController.paused = Mathf.Approximately(val, 0f);
            Time.fixedDeltaTime = (float)BaseFixedDeltaTimeField.GetValue(null);
            Time.maximumDeltaTime = Time.fixedDeltaTime;
            return false;
        }
    }
    [HarmonyPatch(typeof(SaveController), "ReloadAfterAutoSave")]
    public static class SaveSpeedOnReload
    {
        public static float speedAtReload = 0.0f; 
        [HarmonyPrefix]
        static void Prefix()
        {
            speedAtReload = TimeController.targetTimeScale.GetValue();
        }
    }

    [HarmonyPatch(typeof(SimulationManager), "InitializeScene")]
    public static class SetSpeedOnReload
    {
        [HarmonyPrefix]
        public static void Prefix()
        {
            if (SimulationManager.fromAutosaveReload)
            {
                TimeController.targetTimeScale.SetValue(SaveSpeedOnReload.speedAtReload);
            }
        }
    }
}
