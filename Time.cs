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
}
